namespace TeleDoc.Application.DTOs.Client;

public class LegalEntityCreateDto
{
    public string TaxId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string LegalAddress { get; set; } = null!;
}