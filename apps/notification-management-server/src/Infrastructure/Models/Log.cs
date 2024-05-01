using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationManagement.Infrastructure.Models;

[Table("Logs")]
public class Log
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public string? Level { get; set; }

    public string? Timestamp { get; set; }

    public string? Message { get; set; }
}
