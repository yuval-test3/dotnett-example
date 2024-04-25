using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;

namespace CustomerServiceManagement.APIs;

public interface ICustomersService
{
    public Task<Customer> CreateCustomer(CustomerCreateInput input);
    public Task ConnectFeedback(string id, [Required] string FeedbackId);
    public Task ConnectServiceRequest(string id, [Required] string ServiceRequestId);
    public Task DisconnectFeedback(string id, [Required] string FeedbackId);
    public Task DisconnectServiceRequest(string id, [Required] string ServiceRequestId);
    public Task<IEnumerable<Feedback>> Feedbacks(string id);
    public Task<IEnumerable<ServiceRequest>> ServiceRequests(string id);
    public Task<IEnumerable<Feedback>> Feedbacks(string id);
    public Task<IEnumerable<ServiceRequest>> ServiceRequests(string id);
    public Task UpdateFeedbacks(CustomerIdDto idDto, FeedbackIdDto[] FeedbacksId);
    public Task UpdateServiceRequests(CustomerIdDto idDto, ServiceRequestIdDto[] ServiceRequestsId);
    public Task DeleteCustomer(string id);
    public Task<IEnumerable<Customer>> Customers();
    public Task UpdateCustomer(string id, Customer dto);
}
