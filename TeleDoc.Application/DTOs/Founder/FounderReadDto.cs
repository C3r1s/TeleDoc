namespace TeleDoc.Application.DTOs.Founder;

public class FounderReadDto
{
    public Guid Id { get; set; }
    public string TaxId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid LegalEntityId { get; set; }
    public string LegalEntityName { get; set; } = null!;
}