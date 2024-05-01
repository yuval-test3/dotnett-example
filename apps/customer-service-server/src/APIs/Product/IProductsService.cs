using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;

namespace CustomerServiceManagement.APIs;

public interface IProductsService
{
    public Task<Product> CreateProduct(ProductCreateInput input);
    public Task<IEnumerable<Order>> Orders(string id);
    public Task<IEnumerable<Product>> Products();
    public Task DeleteProduct(string id);
    public Task UpdateProduct(string id, Product dto);
}
