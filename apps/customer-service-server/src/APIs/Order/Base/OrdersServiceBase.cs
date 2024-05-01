using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using CustomerServiceManagement.APIs.Extensions;
using CustomerServiceManagement.Infrastructure;
using CustomerServiceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceManagement.APIs;

public abstract class OrdersServiceBase : IOrdersService
{
    protected readonly CustomerServiceManagementContext _context;

    public OrdersServiceBase(CustomerServiceManagementContext context)
    {
        _context = context;
    }

    private bool OrderExists(long id)
    {
        return _context.Orders.Any(e => e.Id == id);
    }

    public async Task DisconnectProduct(string id, [Required] string productId)
    {
        var order = await _context.orders.FindAsync(id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var product = await _context.products.FindAsync(productId);
        if (product == null)
        {
            throw new NotFoundException();
        }

        order.products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrder(string id, OrderDto orderDto)
    {
        var order = new Order { Id = orderDto.Id, Name = orderDto.Name, };

        _context.Entry(order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderExists(id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<OrderDto> CreateOrder(OrderCreateInput inputDto)
    {
        var model = new Order { Id = inputDto.Id, Name = inputDto.Name, };
        _context.orders.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Order>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task<IEnumerable<OrderDto>> orders(OrderFindMany findManyArgs)
    {
        var orders = await _context
            .orders.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return orders.ConvertAll(order => order.ToDto());
    }

    public async Task DeleteOrder(string id)
    {
        var order = await _context.orders.FindAsync(id);

        if (order == null)
        {
            throw new NotFoundException();
        }

        _context.orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProducts(OrderIdDto idDto, ProductIdDto[] productsId)
    {
        var order = await _context
            .orders.Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var products = await _context
            .Products.Where(t => productsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (products.Count == 0)
        {
            throw new NotFoundException();
        }

        order.Products = products;
        await _context.SaveChangesAsync();
    }

    public async Task ConnectProduct(string id, [Required] string productId)
    {
        var order = await _context.orders.FindAsync(id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var product = await _context.products.FindAsync(productId);
        if (product == null)
        {
            throw new NotFoundException();
        }

        order.products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductDto>> Products(string id)
    {
        var order = await _context.orders.FindAsync(id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        return order.Products.Select(product => product.ToDto()).ToList();
    }
}
