using CustomerServiceManagement.Infrastructure;

namespace CustomerServiceManagement.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(CustomerServiceManagementContext context)
        : base(context) { }
}
