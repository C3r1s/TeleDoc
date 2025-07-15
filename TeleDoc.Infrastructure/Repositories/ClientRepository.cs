using Microsoft.EntityFrameworkCore;
using TeleDoc.Data;
using TeleDoc.Domain.Entities;
using TeleDoc.Infrastructure.Interfaces;

namespace TeleDoc.Infrastructure.Repositories;

public class ClientRepository(AppDbContext context) : IClientRepository
{
    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await context.Clients
            .Cast<Client>()
            .ToListAsync();
    }

    public async Task<IEnumerable<Client>> GetAllLegalEntityAsync()
    {
        return await context.Clients
            .OfType<LegalEntity>()
            .Include(le => le.Founders)
            .Cast<Client>()
            .ToListAsync();
    }

    public async Task<IEnumerable<Client>> GetAllIndividualEntrepreneurAsync()
    {
        return await context.Clients
            .OfType<IndividualEntrepreneur>()
            .Cast<Client>()
            .ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(Guid id)
    {
        var client = await context.Clients
            .FirstOrDefaultAsync(c => c.Id == id);

        if (client is LegalEntity legalEntity)
            await context.Entry(legalEntity)
                .Collection(le => le.Founders)
                .LoadAsync();

        return client;
    }

    public async Task<bool> TaxIdExistsAsync(string taxId)
    {
        return await context.Clients.AnyAsync(c => c.TaxId == taxId);
    }

    public async Task AddAsync(Client client)
    {
        await context.Clients.AddAsync(client);
    }

    public void Update(Client client)
    {
        context.Clients.Update(client);
    }

    public void Delete(Client client)
    {
        context.Clients.Remove(client);
    }
}