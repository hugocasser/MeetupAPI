using Meetup.Info.Application.Interfaces;
using Meetup.Info.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Info.Persistence;

public static class InfoInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<EventInformationDbContext>(
            optionsBuilder =>
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("modsen-meetup-dev") + "Database=modsen_info_dev;");
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            },
            ServiceLifetime.Transient
        );
        
        serviceCollection.AddScoped<IEventRepository, EventsRepository>();
        serviceCollection.AddScoped<ISpeakerRepository, SpeakersRepository>();

        return serviceCollection;
    } 
}
