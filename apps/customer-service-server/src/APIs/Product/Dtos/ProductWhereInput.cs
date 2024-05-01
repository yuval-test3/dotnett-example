namespace CustomerServiceManagement.APIs.Dtos;

public class ProductWhereInput
{
    public DateTime CreatedAt { get; set; }
    public decimal? Price { get; set; }
    public string? Name { get; set; }
    public string? InStock { get; set; }
    public OrderDto OrderId { get; set; }
}
