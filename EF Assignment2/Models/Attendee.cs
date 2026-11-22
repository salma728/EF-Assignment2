using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Assignment2.Models;

public class Attendee
{
    [Key]
    public int AttendeeId { get; set; }

    [Required, MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string Street { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string City { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Country { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    [Column("ZipCode")]
    public string PostalCode { get; set; } = string.Empty;

    [InverseProperty(nameof(Badge.Attendee))]
    public Badge? Badge { get; set; }

    [NotMapped]
    public string FullAddress => $"{Street}, {City}, {Country}, {PostalCode}";
}