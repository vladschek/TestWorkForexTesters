using Common.DTOs;
using FluentValidation;

namespace Projects.API.Validator
{
    public class ChartDtoValidator : AbstractValidator<ChartDTO>
    {
        public ChartDtoValidator()
        {
            RuleFor(x => x.Symbol).SetValidator(new SymbolValidator());
            RuleFor(x => x.Timeframe).SetValidator(new TimeframeValidator());
            RuleForEach(x => x.Indicators).ChildRules(indicator =>
            {
                indicator.RuleFor(x=>x).SetValidator(new IndicatorDtoValidator());
            });
        }
    }
}
