using FluentResults;
using TeleDoc.Application.DTOs.Founder;

namespace TeleDoc.Application.Interfaces.Services;

public interface IFounderService
{
    Task<IEnumerable<FounderReadDto>> GetAllAsync();
    Task<FounderReadDto?> GetByIdAsync(Guid id);
    Task<Result<FounderReadDto>> CreateAsync(FounderCreateDto dto);
    Task<Result> UpdateAsync(FounderUpdateDto dto);
    Task<Result> DeleteAsync(Guid id);
}