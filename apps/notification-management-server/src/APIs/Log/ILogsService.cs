using System.ComponentModel.DataAnnotations;
using NotificationManagement.APIs.Dtos;

namespace NotificationManagement.APIs;

public interface ILogsService
{
    public Task<Log> CreateLog(LogCreateInput input);
    public Task DeleteLog(string id);
    public Task<IEnumerable<Log>> Logs();
    public Task UpdateLog(string id, Log dto);
}
