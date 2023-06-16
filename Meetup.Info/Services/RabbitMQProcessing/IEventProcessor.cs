namespace Meetup.Info.Services.RabbitMQProcessing;

public interface IEventProcessor
{
    public void ProcessEvent(string message);
}