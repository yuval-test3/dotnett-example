using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using CustomerServiceManagement.APIs.Extensions;
using CustomerServiceManagement.Infrastructure;
using CustomerServiceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceManagement.APIs;

public abstract class ProductsServiceBase : IProductsService
{
    protected readonly CustomerServiceManagementContext _context;

    public ProductsServiceBase(CustomerServiceManagementContext context)
    {
        _context = context;
    }

    private bool ProductExists(long id)
    {
        return _context.Products.Any(e => e.Id == id);
    }

    public async Task<ProductDto> CreateProduct(ProductCreateInput inputDto)
    {
        var model = new Product { Id = inputDto.Id, Name = inputDto.Name, };
        _context.products.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Product>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task<IEnumerable<ProductDto>> products(ProductFindMany findManyArgs)
    {
        var products = await _context
            .products.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return products.ConvertAll(product => product.ToDto());
    }

    public async Task DeleteProduct(string id)
    {
        var product = await _context.products.FindAsync(id);

        if (product == null)
        {
            throw new NotFoundException();
        }

        _context.products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProduct(string id, ProductDto productDto)
    {
        var product = new Product { Id = productDto.Id, Name = productDto.Name, };

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
