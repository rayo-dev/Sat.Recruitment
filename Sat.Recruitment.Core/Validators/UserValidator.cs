using FluentMigrator.Infrastructure;
using FluentValidation;
using Sat.Recruitment.Core.Entities;

namespace Sat.Recruitment.Core.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().NotNull();
            RuleFor(u => u.Email).NotNull().EmailAddress();
            RuleFor(u => u.Address).NotEmpty().NotNull();
            RuleFor(u => u.Phone).NotNull().PhoneNumber();
            RuleFor(u => u.Money).GreaterThan(0);
            RuleFor(u => u.UserType).NotEmpty().NotNull();
        }
    }
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")
                .WithMessage("'{PropertyName}' is not valid Phone number.");
            return options;
        }

        public static IRuleBuilder<T, string> EmailAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
                .WithMessage("'{PropertyName}' is not valid Email address.");
            return options;
        }
    }
}
