namespace CustomerServiceManagement.APIs.Dtos;

public class ProductDto
{
    public DateTime CreatedAt { get; set; }
    public decimal? Price { get; set; }
    public string? Name { get; set; }
    public string? InStock { get; set; }
    public OrderDto OrderId { get; set; }
}
