using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs;

[ApiController]
public class ProductsController : ProductsControllerBase
{
    public ProductsController(IProductsService service)
        : base(service) { }
}
