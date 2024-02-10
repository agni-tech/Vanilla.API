using FluentValidation;
using Vanilla.Domain.UserGroups.Entities;
using Vanilla.Shared.Domain;

namespace Vanilla.Domain.UserGroupFeatures.Entities;

public class UserGroupFeature : Entity<int, UserGroupFeature>
{
    public int UserGroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual UserGroup UserGroup { get; set; }

    public override bool Validate()
    {
        RuleFor(r => r.UserGroupId).NotEqual(0).WithMessage("USER_GROUP_FIELD_VALIDATION");
        RuleFor(r => r.Name).NotNull().NotEmpty().WithMessage("NAME_FIELD_VALIDATION");
        RuleFor(r => r.Description).NotNull().NotEmpty().WithMessage("DESCRIPTION_FIELD_VALIDATION");

        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}