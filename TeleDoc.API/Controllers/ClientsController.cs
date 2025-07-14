using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TeleDoc.Application.DTOs;
using TeleDoc.Application.DTOs.Client;
using TeleDoc.Application.Interfaces.Services;

namespace TeleDoc.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IValidator<ClientUpdateDto> _clientUpdateValidator;
    private readonly IValidator<IndividualEntrepreneurCreateDto> _individualEntrepreneurValidator;
    private readonly IValidator<LegalEntityCreateDto> _legalEntityValidator;

    public ClientsController(
        IClientService clientService,
        IValidator<LegalEntityCreateDto> legalEntityValidator,
        IValidator<IndividualEntrepreneurCreateDto> individualEntrepreneurValidator,
        IValidator<ClientUpdateDto> clientUpdateValidator)
    {
        _clientService = clientService;
        _legalEntityValidator = legalEntityValidator;
        _individualEntrepreneurValidator = individualEntrepreneurValidator;
        _clientUpdateValidator = clientUpdateValidator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<ClientReadDto>>> GetAll()
    {
        var clients = await _clientService.GetAllAsync();
        return Ok(clients);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("legal-entities")]
    public async Task<ActionResult<IEnumerable<ClientReadDto>>> GetAllLegalEntity()
    {
        var clients = await _clientService.GetAllLegalEntityAsync();
        return Ok(clients);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("individual-entrepreneurs")]
    public async Task<ActionResult<IEnumerable<ClientReadDto>>> GetAllIndividualEntrepreneur()
    {
        var clients = await _clientService.GetAllIndividualEntrepreneurAsync();
        return Ok(clients);
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientReadDto>> GetById(Guid id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client == null) return NotFound();

        return Ok(client);
    }

    [HttpPost("legal-entities")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientReadDto>> CreateLegalEntity(LegalEntityCreateDto dto)
    {
        var validationResult = await _legalEntityValidator.ValidateAsync(dto);
        if (!validationResult.IsValid) return BadRequest(new { errors = validationResult.Errors });

        var result = await _clientService.CreateLegalEntityAsync(dto);

        if (result.IsFailed) return BadRequest(new { errors = result.Errors });

        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPost("individual-entrepreneurs")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientReadDto>> CreateIndividualEntrepreneur(IndividualEntrepreneurCreateDto dto)
    {
        var validationResult = await _individualEntrepreneurValidator.ValidateAsync(dto);
        if (!validationResult.IsValid) return BadRequest(new { errors = validationResult.Errors });

        var result = await _clientService.CreateIndividualEntrepreneurAsync(dto);

        if (result.IsFailed) return BadRequest(new { errors = result.Errors });

        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientReadDto>> Update(ClientUpdateDto dto)
    {
        var validationResult = await _clientUpdateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid) return BadRequest(new { errors = validationResult.Errors });

        var result = await _clientService.UpdateAsync(dto);

        if (result.IsFailed) return BadRequest(new { errors = result.Errors });

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _clientService.DeleteAsync(id);

        if (result.IsFailed) return NotFound(new { message = "Client not found" });

        return Ok(new { message = "Client successfully deleted" });
    }
}