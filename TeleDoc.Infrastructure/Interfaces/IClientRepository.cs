using TeleDoc.Domain.Entities;

namespace TeleDoc.Infrastructure.Interfaces;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<IEnumerable<Client>> GetAllLegalEntityAsync();
    Task<IEnumerable<Client>> GetAllIndividualEntrepreneurAsync();

    Task<Client?> GetByIdAsync(Guid id);
    Task<bool> TaxIdExistsAsync(string taxId);
    Task AddAsync(Client client);
    void Update(Client client);
    void Delete(Client client);
}