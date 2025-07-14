using FluentValidation;
using TeleDoc.Application.DTOs.Client;

namespace TeleDoc.Application.Validators.Client;

public class LegalEntityUpdateDtoValidator : AbstractValidator<LegalEntityUpdateDto>
{
    public LegalEntityUpdateDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.TaxId)
            .NotEmpty().WithMessage("ИНН обязательно для заполнения")
            .Length(10).WithMessage("ИНН должен содержать 10 цифр")
            .Matches(@"^\d+$").WithMessage("ИНН должен содержать только цифры");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Наименование обязательно для заполнения")
            .MaximumLength(255).WithMessage("Наименование не должно превышать 255 символов");

        RuleFor(x => x.LegalAddress)
            .NotEmpty().WithMessage("Юридический адрес обязателен для заполнения")
            .MaximumLength(500).WithMessage("Адрес не должен превышать 500 символов");
    }
}