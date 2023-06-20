using Meetup.Application.Interfaces;
using Meetup.Persistence.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Persistence.Repositories;

public static class PersistenceInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<MeetUpDbContext>(
            optionsBuilder =>
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("modsen-meetup-dev") + "Database=modsen_meetup_dev;");
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            },
            ServiceLifetime.Transient
        );
        
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        return serviceCollection;
    } 
}