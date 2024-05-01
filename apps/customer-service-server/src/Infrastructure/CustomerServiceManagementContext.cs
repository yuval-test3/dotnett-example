using CustomerServiceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceManagement.Infrastructure;

public class CustomerServiceManagementContext : DbContext
{
    public CustomerServiceManagementContext(
        DbContextOptions<CustomerServiceManagementContext> options
    )
        : base(options) { }

    public DbSet<ServiceRequest> ServiceRequests { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<ServiceTicket> ServiceTickets { get; set; } = null!;
    public DbSet<Feedback> Feedbacks { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
}
