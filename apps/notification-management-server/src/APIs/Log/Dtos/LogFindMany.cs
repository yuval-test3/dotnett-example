using Microsoft.AspNetCore.Mvc;
using NotificationManagement.Infrastructure.Models;

namespace NotificationManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class LogFindMany : FindManyInput<Log, LogWhereInput> { }
