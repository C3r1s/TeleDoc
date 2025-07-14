namespace TeleDoc.Application.DTOs.Client;

public class LegalEntityUpdateDto : ClientUpdateDto
{
    public string LegalAddress { get; set; } = null!;
}