using CustomerServiceManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ServiceTicketFindMany : FindManyInput<ServiceTicket, ServiceTicketWhereInput> { }
