namespace TeleDoc.Domain.Entities;

public class IndividualEntrepreneur : Client
{
    public string FullName { get; set; } = null!;
    public DateTime? RegistrationDate { get; set; }
}