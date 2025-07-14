namespace TeleDoc.Application.DTOs.Client;

public class IndividualEntrepreneurUpdateDto : ClientUpdateDto
{
    public string FullName { get; set; } = null!;
    public DateTime? RegistrationDate { get; set; }
}