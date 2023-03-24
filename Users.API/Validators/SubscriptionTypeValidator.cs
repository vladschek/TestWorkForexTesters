using Common.DTOs.Subscription;
using Core.Domain.Enums;
using FluentValidation;

namespace Users.API.Validators
{
    public class SubscriptionTypeValidator : AbstractValidator<string>
    {
        public SubscriptionTypeValidator()
        {
            RuleFor(s => s)
                .Must(BeValidEnumValue).WithMessage("Invalid subscription type.");
        }

        private bool BeValidEnumValue(string type)
        {
            return Enum.TryParse(typeof(SubscriptionType), type, true, out _);
        }
    }
}
