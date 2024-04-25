using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs;

[Route("api/[controller]")]
[ApiController]
public class ServiceRequestsControllerBase : ControllerBase
{
    protected readonly IServiceRequestsService _service;

    public ServiceRequestsControllerBase(IServiceRequestsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceRequestDto>> CreateServiceRequest(
        ServiceRequestCreateInput input
    )
    {
        var dto = await _service.CreateServiceRequest(input);
        return CreatedAtAction(nameof(ServiceRequest), new { id = dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceRequest(string id)
    {
        try
        {
            await _service.DeleteServiceRequest(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceRequestDto>>> ServiceRequests()
    {
        return Ok(await _service.serviceRequests());
    }

    [HttpPost("{id}/serviceTickets")]
    public async Task<IActionResult> ConnectServiceRequest(
        string id,
        [Required] string ServiceTicketId
    )
    {
        try
        {
            await _service.ConnectServiceTicket(id, ServiceRequestId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{id}/customers")]
    public async Task<IActionResult> ConnectServiceRequest(string id, [Required] string CustomerId)
    {
        try
        {
            await _service.ConnectCustomer(id, ServiceRequestId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}/serviceTickets")]
    public async Task<IActionResult> DisconnectServiceRequest(
        string id,
        [Required] string ServiceTicketId
    )
    {
        try
        {
            await _service.DisconnectServiceTicket(id, ServiceRequestId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}/customers")]
    public async Task<IActionResult> DisconnectServiceRequest(
        string id,
        [Required] string CustomerId
    )
    {
        try
        {
            await _service.DisconnectCustomer(id, ServiceRequestId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}/serviceTickets")]
    public async Task<IActionResult> ServiceTickets(string id)
    {
        try
        {
            return Ok(await _service.ServiceTickets(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{id}/customers")]
    public async Task<IActionResult> Customers(string id)
    {
        try
        {
            return Ok(await _service.Customers(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPatch("{id}/serviceTickets")]
    public async Task<IActionResult> UpdateServiceTicket(
        [FromRoute] ServiceRequestIdDto idDto,
        [FromBody] ServiceTicketIdDto[] serviceTicketIds
    )
    {
        try
        {
            await _service.UpdateServiceTicket(id, ServiceTicketId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}/customers")]
    public async Task<IActionResult> UpdateCustomer(
        [FromRoute] ServiceRequestIdDto idDto,
        [FromBody] CustomerIdDto[] customerIds
    )
    {
        try
        {
            await _service.UpdateCustomer(id, CustomerId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateServiceRequest(
        string id,
        ServiceRequestDto serviceRequestDto
    )
    {
        if (id != serviceRequestDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateServiceRequest(id, serviceRequestDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
