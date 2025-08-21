using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

public class Registration
{
    public int Id { get; set; }
    public int EventId { get; set; }

    [Required, StringLength(100)]
    public string AttendeeName { get; set; } = "";

    [Required, EmailAddress, StringLength(200)]
    public string Email { get; set; } = "";

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}
