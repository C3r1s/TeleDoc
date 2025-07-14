namespace TeleDoc.Domain.Entities;

public class LegalEntity : Client
{
    public string LegalAddress { get; set; } = null!;
    public ICollection<Founder> Founders { get; set; } = new List<Founder>();
}