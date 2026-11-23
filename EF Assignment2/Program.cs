
using EF_Assignment2.Data;
using EF_Assignment2.Models;

namespace EF_Assignment2;

internal static class Program
{
    private static void Main()
    {
        using var db = new EventHubContext();

        // Check if the organizer already exists
        var organizer = db.Organizers
            .FirstOrDefault(o => o.Name == "EventHub Team");

        if (organizer == null)
        {
            organizer = new Organizer
            {
                Name = "EventHub Team",
                CompanyName = "EventHub",
                IsVerified = true,
                Profile = new OrganizerProfile
                {
                    Biography = "We organize community events.",
                    WebsiteUrl = "https://example.com",
                    LogoUrl = "https://example.com/logo.png"
                }
            };

            organizer.Events.Add(new Event
            {
                Title = "EventHub Launch",
                Description = "The first EventHub community event.",
                StartDate = DateTime.UtcNow.AddDays(7),
                MaximumAttendees = 100
            });

            db.Organizers.Add(organizer);
        }

        // Check if the attendee already exists
        var attendee = db.Attendees
            .FirstOrDefault(a => a.Email == "ada@example.com");

        if (attendee == null)
        {
            attendee = new Attendee
            {
                FullName = "Ada Lovelace",
                Email = "ada@example.com",
                Street = "1 Analytical Engine Way",
                City = "London",
                Country = "United Kingdom",
                PostalCode = "EC1A 1BB",
                Badge = new Badge
                {
                    CredentialNumber = "EH-0001",
                    IssuedDate = DateTime.UtcNow,
                    Tier = BadgeTier.VIP
                }
            };

            db.Attendees.Add(attendee);
        }

        db.SaveChanges();

        Console.WriteLine(
            $"EventHub database contains {db.Events.Count()} event(s).");
    }
}
