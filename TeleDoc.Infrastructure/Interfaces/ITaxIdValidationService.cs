using TeleDoc.Domain.Enums;

namespace TeleDoc.Infrastructure.Interfaces;

public interface ITaxIdValidationService
{
    Task<bool> ValidateTaxIdAsync(string taxId, ClientType clientType);
}