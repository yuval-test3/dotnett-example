using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs;

[Route("api/[controller]")]
[ApiController]
public class ProductsControllerBase : ControllerBase
{
    protected readonly IProductsService _service;

    public ProductsControllerBase(IProductsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreateInput input)
    {
        var dto = await _service.CreateProduct(input);
        return CreatedAtAction(nameof(Product), new { id = dto.Id }, dto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Products()
    {
        return Ok(await _service.products());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        try
        {
            await _service.DeleteProduct(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, ProductDto productDto)
    {
        if (id != productDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateProduct(id, productDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
