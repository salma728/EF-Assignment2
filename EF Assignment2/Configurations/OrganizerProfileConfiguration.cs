using EF_Assignment2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Assignment2.Configurations;

public sealed class OrganizerProfileConfiguration : IEntityTypeConfiguration<OrganizerProfile>
{
    public void Configure(EntityTypeBuilder<OrganizerProfile> builder)
    {
        builder.HasKey(profile => profile.OrganizerProfileId);

        builder.Property(profile => profile.Biography)
            .HasMaxLength(1000);

        builder.Property(profile => profile.WebsiteUrl)
            .HasMaxLength(500);

        builder.Property(profile => profile.LogoUrl)
            .HasMaxLength(500);

        builder.HasOne(profile => profile.Organizer)
            .WithOne(organizer => organizer.Profile)
            .HasForeignKey<OrganizerProfile>(profile => profile.OrganizerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
