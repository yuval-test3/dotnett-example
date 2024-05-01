using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs;

[Route("api/[controller]")]
[ApiController]
public class FeedbacksControllerBase : ControllerBase
{
    protected readonly IFeedbacksService _service;

    public FeedbacksControllerBase(IFeedbacksService service)
    {
        _service = service;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeedback(string id)
    {
        try
        {
            await _service.DeleteFeedback(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<FeedbackDto>> CreateFeedback(FeedbackCreateInput input)
    {
        var dto = await _service.CreateFeedback(input);
        return CreatedAtAction(nameof(Feedback), new { id = dto.Id }, dto);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateFeedback(string id, FeedbackDto feedbackDto)
    {
        if (id != feedbackDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateFeedback(id, feedbackDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedbackDto>>> Feedbacks()
    {
        return Ok(await _service.feedbacks());
    }
}
