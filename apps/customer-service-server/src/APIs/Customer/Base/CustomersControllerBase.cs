using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs;

[Route("api/[controller]")]
[ApiController]
public class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerCreateInput input)
    {
        var dto = await _service.CreateCustomer(input);
        return CreatedAtAction(nameof(Customer), new { id = dto.Id }, dto);
    }

    [HttpPost("{id}/feedbacks")]
    public async Task<IActionResult> ConnectCustomer(string id, [Required] string FeedbackId)
    {
        try
        {
            await _service.ConnectFeedback(id, CustomerId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{id}/serviceRequests")]
    public async Task<IActionResult> ConnectCustomer(string id, [Required] string ServiceRequestId)
    {
        try
        {
            await _service.ConnectServiceRequest(id, CustomerId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}/feedbacks")]
    public async Task<IActionResult> DisconnectCustomer(string id, [Required] string FeedbackId)
    {
        try
        {
            await _service.DisconnectFeedback(id, CustomerId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}/serviceRequests")]
    public async Task<IActionResult> DisconnectCustomer(
        string id,
        [Required] string ServiceRequestId
    )
    {
        try
        {
            await _service.DisconnectServiceRequest(id, CustomerId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}/feedbacks")]
    public async Task<IActionResult> Feedbacks(string id)
    {
        try
        {
            return Ok(await _service.Feedbacks(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
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

    [HttpPatch("{id}/feedbacks")]
    public async Task<IActionResult> UpdateFeedback(
        [FromRoute] CustomerIdDto idDto,
        [FromBody] FeedbackIdDto[] feedbackIds
    )
    {
        try
        {
            await _service.UpdateFeedback(id, FeedbackId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}/serviceRequests")]
    public async Task<IActionResult> UpdateServiceRequest(
        [FromRoute] CustomerIdDto idDto,
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        try
        {
            await _service.DeleteCustomer(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> Customers()
    {
        return Ok(await _service.customers());
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCustomer(string id, CustomerDto customerDto)
    {
        if (id != customerDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateCustomer(id, customerDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
