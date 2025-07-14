using TeleDoc.Domain.Enums;

namespace TeleDoc.Domain.Entities;

public abstract class Client : BaseEntity
{
    public string TaxId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ClientType Type { get; set; }
}