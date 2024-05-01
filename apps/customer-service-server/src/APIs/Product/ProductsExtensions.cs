using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.Infrastructure.Models;

namespace CustomerServiceManagement.APIs.Extensions;

public static class ProductsExtensions
{
    public static ProductDto ToDto(this Product model)
    {
        return new ProductDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Description = model.Description,
            Price = model.Price,
            Name = model.Name,
            InStock = model.InStock,
            Orders = model.Orders.Select(x => x.ToDto()).ToList(),
        };
    }
}
