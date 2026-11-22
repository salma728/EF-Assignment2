using EF_Assignment2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Assignment2.Configurations;

public sealed class BadgeConfiguration : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder.HasKey(badge => badge.BadgeId);

        builder.Property(badge => badge.CredentialNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(badge => badge.CredentialNumber)
            .IsUnique();

        builder.Property(badge => badge.IssuedDate)
            .IsRequired();

        builder.Property(badge => badge.Tier)
            .IsRequired();

        builder.HasOne(badge => badge.Attendee)
            .WithOne(attendee => attendee.Badge)
            .HasForeignKey<Badge>(badge => badge.AttendeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
