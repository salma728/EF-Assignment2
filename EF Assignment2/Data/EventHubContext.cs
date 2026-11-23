using EF_Assignment2.Configurations;
using EF_Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Assignment2.Data;

public class EventHubContext : DbContext
{
    public DbSet<Organizer> Organizers => Set<Organizer>();
    public DbSet<OrganizerProfile> OrganizerProfiles => Set<OrganizerProfile>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Attendee> Attendees => Set<Attendee>();
    public DbSet<Badge> Badges => Set<Badge>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-0M38JMT;Database=EventHubDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Organizer and Attendee use data annotations. The remaining entities use Fluent API.
        modelBuilder.ApplyConfiguration(new OrganizerProfileConfiguration());
        modelBuilder.ApplyConfiguration(new BadgeConfiguration());

        // Event is configured directly in OnModelCreating (not in a configuration class).
        modelBuilder.Entity<Event>(builder =>
        {
            builder.HasKey(eventEntity => eventEntity.EventId);

            builder.Property(eventEntity => eventEntity.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(eventEntity => eventEntity.Description)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(eventEntity => eventEntity.StartDate)
                .IsRequired();

            builder.Property(eventEntity => eventEntity.MaximumAttendees)
                .IsRequired();

            builder.HasOne(eventEntity => eventEntity.Organizer)
                .WithMany(organizer => organizer.Events)
                .HasForeignKey(eventEntity => eventEntity.OrganizerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}
