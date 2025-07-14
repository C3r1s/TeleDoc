using TeleDoc.Data;
using TeleDoc.Infrastructure.Interfaces;

namespace TeleDoc.Infrastructure.Repositories;

public class UnitOfWork(
    AppDbContext context,
    IClientRepository clientRepository,
    IFounderRepository founderRepository)
    : IUnitOfWork
{
    public IClientRepository Clients { get; } = clientRepository;
    public IFounderRepository Founders { get; } = founderRepository;

    public async Task<int> CommitAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}