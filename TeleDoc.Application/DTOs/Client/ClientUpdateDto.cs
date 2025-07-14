namespace TeleDoc.Application.DTOs.Client;

public class ClientUpdateDto
{
    public Guid Id { get; set; }
    public string TaxId { get; set; } = null!;
    public string Name { get; set; } = null!;
}