using Microsoft.EntityFrameworkCore;
using TeleDoc.Domain.Entities;
using TeleDoc.Domain.Enums;

namespace TeleDoc.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<LegalEntity> LegalEntities { get; set; } = null!;
    public DbSet<IndividualEntrepreneur> IndividualEntrepreneurs { get; set; } = null!;
    public DbSet<Founder> Founders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Client>()
            .HasDiscriminator<ClientType>(c => c.Type)
            .HasValue<LegalEntity>(ClientType.LegalEntity)
            .HasValue<IndividualEntrepreneur>(ClientType.IndividualEntrepreneur);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var transaction = await Database.BeginTransactionAsync(cancellationToken);
        try
        {
            UpdateTimestamps();
            var result = await base.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity == null) continue;
            
            entry.Entity.UpdatedAt = DateTime.UtcNow;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
        }
    }
}