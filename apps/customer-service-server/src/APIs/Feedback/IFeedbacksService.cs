using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;

namespace CustomerServiceManagement.APIs;

public interface IFeedbacksService
{
    public Task DeleteFeedback(string id);
    public Task<Feedback> CreateFeedback(FeedbackCreateInput input);
    public Task UpdateFeedback(string id, Feedback dto);
    public Task<IEnumerable<Customer>> Customers(string id);
    public Task<IEnumerable<Feedback>> Feedbacks();
}
