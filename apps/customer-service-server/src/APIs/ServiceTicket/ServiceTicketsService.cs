using CustomerServiceManagement.Infrastructure;

namespace CustomerServiceManagement.APIs;

public class ServiceTicketsService : ServiceTicketsServiceBase
{
    public ServiceTicketsService(CustomerServiceManagementContext context)
        : base(context) { }
}
