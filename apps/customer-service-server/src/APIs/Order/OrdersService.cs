using CustomerServiceManagement.Infrastructure;

namespace CustomerServiceManagement.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(CustomerServiceManagementContext context)
        : base(context) { }
}
