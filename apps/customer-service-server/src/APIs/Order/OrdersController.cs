using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs;

[ApiController]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
