using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceManagement.Infrastructure.Models;

[Table("Products")]
public class Product
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? Name { get; set; }

    public string? InStock { get; set; }

    public string OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }
}
