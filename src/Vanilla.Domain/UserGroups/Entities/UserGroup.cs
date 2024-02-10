using FluentValidation;
using Vanilla.Domain.UserGroupFeatures.Entities;
using Vanilla.Shared.Domain;

namespace Vanilla.Domain.UserGroups.Entities;

public class UserGroup : Entity<int, UserGroup>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual List<UserGroupFeature> UserGroupFeatures { get; set; }
    public virtual List<Users.Entities.User> Users { get; set; }

    public override bool Validate()
    {
        RuleFor(r => r.Name).NotNull().NotEmpty().WithMessage("NAME_FIELD_VALIDATION");
        RuleFor(r => r.Description).NotNull().NotEmpty().WithMessage("DESCRIPTION_FIELD_VALIDATION");

        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}
