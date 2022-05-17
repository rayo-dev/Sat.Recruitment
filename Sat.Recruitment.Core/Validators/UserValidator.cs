using FluentValidation;
using Sat.Recruitment.Core.Entities;

namespace Sat.Recruitment.Core.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().NotNull();
            RuleFor(u => u.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(u => u.Address).NotEmpty().NotNull();
            RuleFor(u => u.Phone).NotEmpty().NotNull();
            RuleFor(u => u.Money).GreaterThan(0);
            RuleFor(u => u.UserType).NotEmpty().NotNull();
        }
    }
}
