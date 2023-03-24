using Common.DTOs;
using Core.Domain.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Projects.API.Validator
{
    public class IndicatorDtoValidator : AbstractValidator<IndicatorDTO>
    {
        public IndicatorDtoValidator()
        {
            RuleFor(x => x.Name).Must(BeAValidIndicatorName)
                .WithMessage("Invalid indicator name. Allowed values are: " + string.Join(", ", Enum.GetNames(typeof(IndicatorName))));
            RuleFor(x => x.Parameters).Must(BeAValidParametersString)
                .WithMessage("Invalid parameters string. It should follow the pattern 'a=1;b=2;c=3'");
        }

        private bool BeAValidParametersString(string parameters)
        {
            var pattern = @"^([a-z]+=[0-9]+;?)+$";
            return Regex.IsMatch(parameters, pattern);
        }
        private bool BeAValidIndicatorName(string name)
        {
            return Enum.GetNames(typeof(IndicatorName)).Contains(name);
        }
    }
}
