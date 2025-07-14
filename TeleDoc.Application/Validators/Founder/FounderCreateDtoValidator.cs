using FluentValidation;
using TeleDoc.Application.DTOs.Founder;

namespace TeleDoc.Application.Validators.Founder;

public class FounderCreateDtoValidator : AbstractValidator<FounderCreateDto>
{
    public FounderCreateDtoValidator()
    {
        RuleFor(x => x.TaxId)
            .NotEmpty().WithMessage("ИНН обязательно для заполнения")
            .Length(12).WithMessage("ИНН должен содержать 12 цифр")
            .Matches(@"^\d+$").WithMessage("ИНН должен содержать только цифры");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("ФИО обязательно для заполнения")
            .MaximumLength(255).WithMessage("ФИО не должно превышать 255 символов");

        RuleFor(x => x.LegalEntityId)
            .NotEmpty().WithMessage("ID юридического лица обязательно для заполнения");
    }
}