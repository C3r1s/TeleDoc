using TeleDoc.Domain.Entities;

namespace TeleDoc.Infrastructure.Interfaces;

public interface IFounderRepository
{
    Task<IEnumerable<Founder>> GetAllAsync();
    Task<Founder?> GetByIdAsync(Guid id);
    Task<bool> TaxIdExistsAsync(string taxId);
    Task AddAsync(Founder founder);
    void Update(Founder founder);
    void Delete(Founder founder);
}