using Microsoft.AspNetCore.Mvc;
using TeleDoc.Application.DTOs.Founder;
using TeleDoc.Application.Interfaces.Services;

namespace TeleDoc.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoundersController : ControllerBase
{
    private readonly IFounderService _founderService;

    public FoundersController(IFounderService founderService)
    {
        _founderService = founderService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FounderReadDto>>> GetAll()
    {
        var founders = await _founderService.GetAllAsync();
        return Ok(founders);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FounderReadDto>> GetById(Guid id)
    {
        var founder = await _founderService.GetByIdAsync(id);
        if (founder == null) return NotFound();

        return Ok(founder);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FounderReadDto>> Create(FounderCreateDto dto)
    {
        var result = await _founderService.CreateAsync(dto);

        if (result.IsFailed) return BadRequest(new { errors = result.Errors });

        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _founderService.DeleteAsync(id);

        if (result.IsFailed) return NotFound(new { message = "Учредитель не найден" });

        return Ok(new { message = "Учредитель успешно удален" });
    }
}