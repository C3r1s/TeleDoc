using System.ComponentModel.DataAnnotations;

namespace TeleDoc.Domain.Entities;

public class Founder : BaseEntity
{
    public string TaxId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public ICollection<LegalEntity> LegalEntities { get; set; } = new List<LegalEntity>();
}