using Microsoft.EntityFrameworkCore;
using TeleDoc.Domain.Entities;
using TeleDoc.Domain.Enums;

namespace TeleDoc.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<LegalEntity> LegalEntities { get; set; }
    public DbSet<IndividualEntrepreneur> IndividualEntrepreneurs { get; set; }
    public DbSet<Founder> Founders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Client>()
            .HasDiscriminator(c => c.Type)
            .HasValue<LegalEntity>(ClientType.LegalEntity)
            .HasValue<IndividualEntrepreneur>(ClientType.IndividualEntrepreneur);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;

            if (entry.State == EntityState.Added) entry.Entity.CreatedAt = DateTime.UtcNow;
        }
    }
}