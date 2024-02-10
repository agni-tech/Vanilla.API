using FluentValidation;
using Vanilla.Domain.UserGroups.Entities;
using Vanilla.Shared.Domain;

namespace Vanilla.Domain.Users.Entities;

public class User : Entity<int, User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Password { get; set; }
    public string RecoveryCode { get; set; }
    public int? EnterpriseId { get; set; }
    public int? UserGroupId { get; set; }
    public string Locale { get; set; }
    public DateTime? RecoveryCodeExpiration { get; set; }

    public virtual UserGroup UserGroup { get; set; }

    public override bool Validate()
    {
        int passwordMinimumLength = 5;

        RuleFor(r => r.FirstName).NotEmpty().NotNull().WithMessage("FIRST_NAME_FIELD_VALIDATION");
        RuleFor(r => r.LastName).NotEmpty().NotNull().WithMessage("LAST_NAME_FIELD_VALIDATION");
        RuleFor(r => r.Email).NotEmpty().NotNull().EmailAddress().WithMessage("EMAIL_FIELD_VALIDATION");
        RuleFor(r => r.Password).NotEmpty().NotNull().MinimumLength(passwordMinimumLength).WithMessage($"PASSWORD_FIELD_VALIDATION");

        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}
