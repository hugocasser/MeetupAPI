using System.Text;
using Meetup.Info.Services.RabbitMQProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Meetup.Info.Services.RabbitMQSubscriber;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;
    private readonly ILogger<MessageBusSubscriber> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor, ILogger<MessageBusSubscriber> logger)
    {
        _configuration = configuration;
        _eventProcessor = eventProcessor;
        _logger = logger;

        _logger.LogInformation("--> Starting injection of the Message Bus...");
        var uri = _configuration.GetServiceUri("rabbit");

        var endpoint = new AmqpTcpEndpoint(uri);
        var factory = new ConnectionFactory()
        {
            Endpoint = endpoint,
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare("trigger", ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(
            queue: _queueName,
            exchange: "trigger",
            routingKey: "");
        
        _logger.LogInformation("--> Listenting on the Message Bus...");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (moduleHandle, eventArgs) =>
        {
            _logger.LogInformation("--> Event Received!");
            var body = eventArgs.Body;
            _eventProcessor.ProcessEvent(Encoding.UTF8.GetString(body.ToArray()));
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }    
}