using FluentResults;
using TeleDoc.Application.DTOs;
using TeleDoc.Application.DTOs.Client;

namespace TeleDoc.Application.Interfaces.Services;

public interface IClientService
{
    Task<IEnumerable<ClientReadDto>> GetAllAsync();
    Task<IEnumerable<LegalEntityReadDto>> GetAllLegalEntityAsync();
    Task<IEnumerable<IndividualEntrepreneurReadDto>> GetAllIndividualEntrepreneurAsync();

    Task<ClientReadDto?> GetByIdAsync(Guid id);
    Task<Result<ClientReadDto>> CreateLegalEntityAsync(LegalEntityCreateDto dto);
    Task<Result<ClientReadDto>> CreateIndividualEntrepreneurAsync(IndividualEntrepreneurCreateDto dto);
    Task<Result<ClientReadDto>> UpdateAsync(ClientUpdateDto dto);
    Task<Result> DeleteAsync(Guid id);
}