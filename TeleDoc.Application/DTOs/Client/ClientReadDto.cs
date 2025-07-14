using TeleDoc.Domain.Enums;

namespace TeleDoc.Application.DTOs.Client;

public abstract class ClientReadDto
{
    public Guid Id { get; set; }
    public string TaxId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ClientType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}