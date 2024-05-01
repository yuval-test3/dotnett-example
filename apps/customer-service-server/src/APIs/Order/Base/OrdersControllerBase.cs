using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs;

[Route("api/[controller]")]
[ApiController]
public class OrdersControllerBase : ControllerBase
{
    protected readonly IOrdersService _service;

    public OrdersControllerBase(IOrdersService service)
    {
        _service = service;
    }

    [HttpDelete("{id}/products")]
    public async Task<IActionResult> DisconnectOrder(string id, [Required] string ProductId)
    {
        try
        {
            await _service.DisconnectProduct(id, OrderId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOrder(string id, OrderDto orderDto)
    {
        if (id != orderDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateOrder(id, orderDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(OrderCreateInput input)
    {
        var dto = await _service.CreateOrder(input);
        return CreatedAtAction(nameof(Order), new { id = dto.Id }, dto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> Orders()
    {
        return Ok(await _service.orders());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(string id)
    {
        try
        {
            await _service.DeleteOrder(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}/products")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] OrderIdDto idDto,
        [FromBody] ProductIdDto[] productIds
    )
    {
        try
        {
            await _service.UpdateProduct(id, ProductId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{id}/products")]
    public async Task<IActionResult> ConnectOrder(string id, [Required] string ProductId)
    {
        try
        {
            await _service.ConnectProduct(id, OrderId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}/products")]
    public async Task<IActionResult> Products(string id)
    {
        try
        {
            return Ok(await _service.Products(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
