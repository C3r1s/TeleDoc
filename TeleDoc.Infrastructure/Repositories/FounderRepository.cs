using Microsoft.EntityFrameworkCore;
using TeleDoc.Data;
using TeleDoc.Domain.Entities;
using TeleDoc.Infrastructure.Interfaces;

namespace TeleDoc.Infrastructure.Repositories;

public class FounderRepository(AppDbContext context) : IFounderRepository
{
    public async Task<IEnumerable<Founder>> GetAllAsync()
    {
        return await context.Founders
            .Include(f => f.LegalEntity)
            .ToListAsync();
    }

    public async Task<Founder?> GetByIdAsync(Guid id)
    {
        return await context.Founders
            .Include(f => f.LegalEntity)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<bool> TaxIdExistsAsync(string taxId)
    {
        return await context.Founders.AnyAsync(f => f.TaxId == taxId);
    }

    public async Task AddAsync(Founder founder)
    {
        await context.Founders.AddAsync(founder);
    }

    public void Update(Founder founder)
    {
        context.Founders.Update(founder);
    }

    public void Delete(Founder founder)
    {
        context.Founders.Remove(founder);
    }

    public async Task<IEnumerable<Founder>> GetByLegalEntityIdAsync(Guid legalEntityId)
    {
        return await context.Founders
            .Where(f => f.LegalEntityId == legalEntityId)
            .ToListAsync();
    }
}