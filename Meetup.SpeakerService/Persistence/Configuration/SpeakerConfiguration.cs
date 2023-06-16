using Meetup.SpeakerService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meetup.SpeakerService.Persistence.Configuration;


public class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
{
    public void Configure(EntityTypeBuilder<Speaker> builder)
    {
        builder.HasKey(speaker => speaker.Id);
        builder.Property(speaker => speaker.FirstName).HasMaxLength(50);
        builder.Property(speaker => speaker.LastName).HasMaxLength(50);
        builder.Property(speaker => speaker.Presentation).HasMaxLength(600);
    }
}