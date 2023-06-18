using Meetup.SpeakerService.Services.RabbitMQ;

namespace Meetup.SpeakerService.Services;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IMessageBusClient, MessageBusClient>();
        return serviceCollection;
    }
}