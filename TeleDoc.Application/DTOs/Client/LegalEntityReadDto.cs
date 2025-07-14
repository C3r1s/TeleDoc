using TeleDoc.Application.DTOs.Founder;

namespace TeleDoc.Application.DTOs.Client;

public class LegalEntityReadDto : ClientReadDto
{
    public string LegalAddress { get; set; } = null!;
    public List<FounderShortInfoDto> Founders { get; set; } = new();
}