using CustomerServiceManagement.Infrastructure;

namespace CustomerServiceManagement.APIs;

public class ProductsService : ProductsServiceBase
{
    public ProductsService(CustomerServiceManagementContext context)
        : base(context) { }
}
