using FluentValidation;
using Vanilla.Shared.Domain;

namespace Vanilla.Domain.Options.Entities;

public class Option : Entity<int, Option>
{
    public string Config { get; set; }
    public string Value { get; set; }

    public override bool Validate()
    {
        RuleFor(r => r.Config).NotNull().NotEmpty().WithMessage("NAME_FIELD_VALIDATION");
        RuleFor(r => r.Value).NotNull().NotEmpty().WithMessage("DESCRIPTION_FIELD_VALIDATION");

        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}
