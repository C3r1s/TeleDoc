using TeleDoc.Application.DTOs.Client;

namespace TeleDoc.Application.DTOs.Founder;

public class FounderReadDto
{
    public Guid Id { get; set; }
    public string TaxId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public List<LegalEntityShortInfoDto> LegalEntities { get; set; } = new();

}