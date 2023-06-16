using Meetup.Info.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meetup.Info.Persistence.Configurations;

public class SpeakerConfiguration :IEntityTypeConfiguration<Speaker>
{
    public void Configure(EntityTypeBuilder<Speaker> builder)
    {
        builder
            .HasMany(speaker => speaker.Events)
            .WithOne(events => events.Speaker)
            .HasForeignKey(events => events.SpeakerId);
    }
}