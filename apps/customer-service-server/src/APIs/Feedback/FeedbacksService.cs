using CustomerServiceManagement.Infrastructure;

namespace CustomerServiceManagement.APIs;

public class FeedbacksService : FeedbacksServiceBase
{
    public FeedbacksService(CustomerServiceManagementContext context)
        : base(context) { }
}
