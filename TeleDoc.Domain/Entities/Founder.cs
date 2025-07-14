using System.ComponentModel.DataAnnotations;

namespace TeleDoc.Domain.Entities;

public class Founder : BaseEntity
{
    public string TaxId { get; set; } = null!;
    public string FullName { get; set; } = null!;

    [Required] public Guid LegalEntityId { get; set; }

    public LegalEntity LegalEntity { get; set; } = null!;
}