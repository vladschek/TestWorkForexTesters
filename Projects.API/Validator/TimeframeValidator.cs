using Core.Domain.Enums;
using FluentValidation;

namespace Projects.API.Validator
{
    public class TimeframeValidator : AbstractValidator<string>
    {
        public TimeframeValidator()
        {
            RuleFor(x => x).Must(BeAValidTimeframe).WithMessage("Invalid timeframe. Allowed values are: " + string.Join(", ", Enum.GetNames(typeof(Timeframe))));
        }

        private bool BeAValidTimeframe(string timeframe)
        {
            return Enum.GetNames(typeof(Timeframe)).Contains(timeframe);
        }
    }
}
