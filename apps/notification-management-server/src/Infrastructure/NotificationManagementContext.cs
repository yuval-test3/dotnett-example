using Microsoft.EntityFrameworkCore;
using NotificationManagement.Infrastructure.Models;

namespace NotificationManagement.Infrastructure;

public class NotificationManagementContext : DbContext
{
    public NotificationManagementContext(DbContextOptions<NotificationManagementContext> options)
        : base(options) { }

    public DbSet<Log> Logs { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
}
