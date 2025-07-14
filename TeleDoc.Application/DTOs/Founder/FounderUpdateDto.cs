using System.ComponentModel.DataAnnotations;

namespace TeleDoc.Application.DTOs.Founder;

public class FounderUpdateDto
{
    [Required(ErrorMessage = "ID обязателен для заполнения")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "ИНН обязателен для заполнения")]
    [RegularExpression(@"^\d{12}$", ErrorMessage = "ИНН должен содержать 12 цифр")]
    public string TaxId { get; set; } = null!;

    [Required(ErrorMessage = "ФИО обязательно для заполнения")]
    [MaxLength(255, ErrorMessage = "ФИО не должно превышать 255 символов")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "ID юридического лица обязателен для заполнения")]
    public Guid LegalEntityId { get; set; }
}