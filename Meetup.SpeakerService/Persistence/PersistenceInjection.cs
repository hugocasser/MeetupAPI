using Meetup.SpeakerService.Application.Interfaces;
using Meetup.SpeakerService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Meetup.SpeakerService.Persistence;

public static class PersistenceInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<SpeakerDbContext>(
            optionsBuilder =>
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("modsen-meetup-dev") + "Database=modsen_speakers_dev;");
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            },
            ServiceLifetime.Transient
        );
        
        serviceCollection.AddScoped<ISpeakerRepository, SpeakerRepository>();
        return serviceCollection;
    } 
}