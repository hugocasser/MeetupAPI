using Meetup.SpeakerService.Models;
using Meetup.SpeakerService.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Meetup.SpeakerService.Persistence;

public class SpeakerDbContext : DbContext
{
    public DbSet<Speaker> Speakers { get; set; }

    public SpeakerDbContext(DbContextOptions<SpeakerDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new SpeakerConfiguration());
    } 
}