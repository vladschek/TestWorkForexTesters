using Common.DTOs.Subscription;
using Core.Domain.Entities;
using Core.Domain.Enums;
using FluentValidation;

namespace Users.API.Validators
{
    public class SubscriptionDTOValidator : AbstractValidator<SubscriptionDTO>
    {
        public SubscriptionDTOValidator()
        {
            RuleFor(s => s.Type)
                .Must(BeValidEnumValue).WithMessage("Invalid subscription type.");

            RuleFor(s => s.StartDate)
                .LessThan(s => s.EndDate)            
                .WithMessage("StartDate must be less than EndDate.");
        }

        private bool BeValidEnumValue(string type)
        {
            return Enum.TryParse(typeof(SubscriptionType), type, true, out _);
        }
    }
}
