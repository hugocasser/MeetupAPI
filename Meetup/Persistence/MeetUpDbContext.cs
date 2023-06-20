using Meetup.Models;
using Meetup.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Persistence.Repositories;

public class MeetUpDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public MeetUpDbContext(DbContextOptions<MeetUpDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}