using FluentValidation;
using TeleDoc.Application.DTOs;
using TeleDoc.Application.DTOs.Client;

namespace TeleDoc.Application.Validators.Client;

public class IndividualEntrepreneurUpdateDtoValidator : AbstractValidator<IndividualEntrepreneurUpdateDto>
{
    public IndividualEntrepreneurUpdateDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.TaxId)
            .NotEmpty().WithMessage("ИНН обязательно для заполнения")
            .Length(12).WithMessage("ИНН должен содержать 12 цифр")
            .Matches(@"^\d+$").WithMessage("ИНН должен содержать только цифры");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Наименование обязательно для заполнения")
            .MaximumLength(255).WithMessage("Наименование не должно превышать 255 символов");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("ФИО обязательно для заполнения")
            .MaximumLength(255).WithMessage("ФИО не должно превышать 255 символов");
    }
}