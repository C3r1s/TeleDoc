namespace TeleDoc.Application.DTOs.Client;

public class IndividualEntrepreneurReadDto : ClientReadDto
{
    public string FullName { get; set; } = null!;
    public DateTime? RegistrationDate { get; set; }
}