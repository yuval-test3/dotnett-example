namespace CustomerServiceManagement.APIs.Dtos;

public class OrderWhereInput
{
    public DateTime CreatedAt { get; set; }
    public string? TotalAmount { get; set; }
    public string? OrderNumber { get; set; }
    public string? PlacedAt { get; set; }
    public ICollection<ProductDto>? Products { get; set; }
}
