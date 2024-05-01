using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NotificationManagement.APIs.Dtos;
using NotificationManagement.APIs.Errors;

namespace NotificationManagement.APIs;

[Route("api/[controller]")]
[ApiController]
public class NotificationsControllerBase : ControllerBase
{
    protected readonly INotificationsService _service;

    public NotificationsControllerBase(INotificationsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<NotificationDto>> CreateNotification(
        NotificationCreateInput input
    )
    {
        var dto = await _service.CreateNotification(input);
        return CreatedAtAction(nameof(Notification), new { id = dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(string id)
    {
        try
        {
            await _service.DeleteNotification(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> Notifications()
    {
        return Ok(await _service.notifications());
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateNotification(string id, NotificationDto notificationDto)
    {
        if (id != notificationDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateNotification(id, notificationDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
