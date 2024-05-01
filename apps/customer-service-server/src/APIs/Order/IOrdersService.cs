using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;

namespace CustomerServiceManagement.APIs;

public interface IOrdersService
{
    public Task DisconnectProduct(string id, [Required] string ProductId);
    public Task UpdateOrder(string id, Order dto);
    public Task<Order> CreateOrder(OrderCreateInput input);
    public Task<IEnumerable<Order>> Orders();
    public Task DeleteOrder(string id);
    public Task UpdateProducts(OrderIdDto idDto, ProductIdDto[] ProductsId);
    public Task ConnectProduct(string id, [Required] string ProductId);
    public Task<IEnumerable<Product>> Products(string id);
}
