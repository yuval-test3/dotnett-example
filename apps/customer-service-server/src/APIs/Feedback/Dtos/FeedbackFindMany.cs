using CustomerServiceManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class FeedbackFindMany : FindManyInput<Feedback, FeedbackWhereInput> { }
