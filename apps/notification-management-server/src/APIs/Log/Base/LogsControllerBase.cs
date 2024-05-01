using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NotificationManagement.APIs.Dtos;
using NotificationManagement.APIs.Errors;

namespace NotificationManagement.APIs;

[Route("api/[controller]")]
[ApiController]
public class LogsControllerBase : ControllerBase
{
    protected readonly ILogsService _service;

    public LogsControllerBase(ILogsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<LogDto>> CreateLog(LogCreateInput input)
    {
        var dto = await _service.CreateLog(input);
        return CreatedAtAction(nameof(Log), new { id = dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLog(string id)
    {
        try
        {
            await _service.DeleteLog(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LogDto>>> Logs()
    {
        return Ok(await _service.logs());
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateLog(string id, LogDto logDto)
    {
        if (id != logDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateLog(id, logDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
