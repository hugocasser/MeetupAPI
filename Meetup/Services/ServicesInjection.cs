namespace Meetup.Services;

public static class ServiceInjection
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<ISpeakerDataClient, SpeakerDataClient>();
        serviceCollection.AddHttpClient<ISpeakerDataClient, SpeakerDataClient>();
        return serviceCollection;
    }
}