using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceManagement.Infrastructure.Models;

[Table("Orders")]
public class Order
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public OptionSet? Status { get; set; }

    public string? TotalAmount { get; set; }

    public string? OrderNumber { get; set; }

    public string? PlacedAt { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
