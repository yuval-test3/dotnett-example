using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs;

[Route("api/[controller]")]
[ApiController]
public class ServiceTicketsControllerBase : ControllerBase
{
    protected readonly IServiceTicketsService _service;

    public ServiceTicketsControllerBase(IServiceTicketsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceTicketDto>> CreateServiceTicket(
        ServiceTicketCreateInput input
    )
    {
        var dto = await _service.CreateServiceTicket(input);
        return CreatedAtAction(nameof(ServiceTicket), new { id = dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceTicket(string id)
    {
        try
        {
            await _service.DeleteServiceTicket(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceTicketDto>>> ServiceTickets()
    {
        return Ok(await _service.serviceTickets());
    }

    [HttpPost("{id}/serviceRequests")]
    public async Task<IActionResult> ConnectServiceTicket(
        string id,
        [Required] string ServiceRequestId
    )
    {
        try
        {
            await _service.ConnectServiceRequest(id, ServiceTicketId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}/serviceRequests")]
    public async Task<IActionResult> DisconnectServiceTicket(
        string id,
        [Required] string ServiceRequestId
    )
    {
        try
        {
            await _service.DisconnectServiceRequest(id, ServiceTicketId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}/serviceRequests")]
    public async Task<IActionResult> ServiceRequests(string id)
    {
        try
        {
            return Ok(await _service.ServiceRequests(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPatch("{id}/serviceRequests")]
    public async Task<IActionResult> UpdateServiceRequest(
        [FromRoute] ServiceTicketIdDto idDto,
        [FromBody] ServiceRequestIdDto[] serviceRequestIds
    )
    {
        try
        {
            await _service.UpdateServiceRequest(id, ServiceRequestId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateServiceTicket(
        string id,
        ServiceTicketDto serviceTicketDto
    )
    {
        if (id != serviceTicketDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateServiceTicket(id, serviceTicketDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
