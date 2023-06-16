using Meetup.Info.Models;
using Meetup.Info.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Info.Persistence;

public class EventInformationDbContext : DbContext
{
    public DbSet<EventInformation> Events { get; set; }
    public DbSet<Speaker?> Speakers { get; set; }

    public EventInformationDbContext(DbContextOptions<EventInformationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new EventInformationConfigurations());
        modelBuilder.ApplyConfiguration(new SpeakerConfiguration());
    }  
}