using Meetup.Info.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meetup.Info.Persistence.Configurations;

public class EventInformationConfigurations : IEntityTypeConfiguration<EventInformation>
{
    public void Configure(EntityTypeBuilder<EventInformation> builder)
    {
        builder
            .HasIndex(events => new { events.Id, events.SpeakerId })
            .IsUnique();

        builder
            .Property(events => events.Title)
            .HasMaxLength(20);

        builder
            .HasOne(events => events.Speaker)
            .WithMany(speaker => speaker.Events)
            .HasForeignKey(book => book.SpeakerId);
    }
}