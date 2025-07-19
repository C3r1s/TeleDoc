using FluentResults;
using FluentValidation;
using TeleDoc.Application.DTOs.Client;
using TeleDoc.Application.DTOs.Founder;
using TeleDoc.Application.Interfaces.Services;
using TeleDoc.Domain.Entities;
using TeleDoc.Infrastructure.Interfaces;

namespace TeleDoc.Application.Services;

public class FounderService : IFounderService
{
    private readonly IClientRepository _clientRepository;
    private readonly IFounderRepository _founderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<FounderCreateDto> _validator;

    public FounderService(
        IUnitOfWork unitOfWork,
        IFounderRepository founderRepository,
        IClientRepository clientRepository,
        IValidator<FounderCreateDto> validator)
    {
        _unitOfWork = unitOfWork;
        _founderRepository = founderRepository;
        _clientRepository = clientRepository;
        _validator = validator;
    }

    public async Task<IEnumerable<FounderReadDto>> GetAllAsync()
    {
        var founders = await _founderRepository.GetAllAsync();
        return founders.Select(MapToDto);
    }

    public async Task<FounderReadDto?> GetByIdAsync(Guid id)
    {
        var founder = await _founderRepository.GetByIdAsync(id);
        return founder != null ? MapToDto(founder) : null;
    }

    public async Task<Result<FounderReadDto>> CreateAsync(FounderCreateDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid) return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

        var legalEntity = await _clientRepository.GetByIdAsync(dto.LegalEntityId);
        if (legalEntity is not LegalEntity) return Result.Fail("Указанное юридическое лицо не найдено");

        if (await _founderRepository.TaxIdExistsAsync(dto.TaxId))
            return Result.Fail("Учредитель с таким ИНН уже существует");

        var founder = new Founder
        {
            TaxId = dto.TaxId,
            FullName = dto.FullName,
        };

        await _founderRepository.AddAsync(founder);
        await _unitOfWork.CommitAsync();

        return MapToDto(founder);
    }

    public async Task<Result> UpdateAsync(FounderUpdateDto dto)
    {
        var founder = await _founderRepository.GetByIdAsync(dto.Id);
        if (founder == null) return Result.Fail("Учредитель не найден");

        founder.TaxId = dto.TaxId;
        founder.FullName = dto.FullName;

        _founderRepository.Update(founder);
        await _unitOfWork.CommitAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var founder = await _founderRepository.GetByIdAsync(id);
        if (founder == null) return Result.Fail("Учредитель не найден");

        _founderRepository.Delete(founder);
        await _unitOfWork.CommitAsync();

        return Result.Ok();
    }

    private static FounderReadDto MapToDto(Founder founder)
    {
        return new FounderReadDto
        {
            Id = founder.Id,
            TaxId = founder.TaxId,
            FullName = founder.FullName,
            CreatedAt = founder.CreatedAt,
            UpdatedAt = founder.UpdatedAt,
            LegalEntities = founder.LegalEntities?
                .Select(le => new LegalEntityShortInfoDto
                {
                    Id = le.Id,
                    Name = le.Name
                })
                .ToList() ?? new List<LegalEntityShortInfoDto>()
        };
    }
}