using Meetup.Info.Services.RabbitMQProcessing;
using Meetup.Info.Services.RabbitMQSubscriber;

namespace Meetup.Info.Services;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IEventProcessor, EventProcessor>();
        serviceCollection.AddHostedService<MessageBusSubscriber>();
        return serviceCollection;
    }
}