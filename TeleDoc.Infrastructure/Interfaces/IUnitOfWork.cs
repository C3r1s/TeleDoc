namespace TeleDoc.Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IClientRepository Clients { get; }
    IFounderRepository Founders { get; }
    Task<int> CommitAsync();
}