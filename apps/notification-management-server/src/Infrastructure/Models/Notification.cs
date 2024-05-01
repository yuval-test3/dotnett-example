using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationManagement.Infrastructure.Models;

[Table("Notifications")]
public class Notification
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public OptionSet? Status { get; set; }

    public string? Message { get; set; }

    public string? Recipient { get; set; }

    public string? Title { get; set; }
}
