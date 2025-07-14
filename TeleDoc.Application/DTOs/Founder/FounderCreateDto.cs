namespace TeleDoc.Application.DTOs.Founder;

public class FounderCreateDto
{
    public string TaxId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public Guid LegalEntityId { get; set; }
}