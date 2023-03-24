using Core.Domain.Enums;
using FluentValidation;

namespace Projects.API.Validator
{
    public class SymbolValidator : AbstractValidator<string>
    {
        public SymbolValidator()
        {
            RuleFor(x => x).Must(BeAValidSymbol).WithMessage("Invalid symbol. Allowed values are: " + string.Join(", ", Enum.GetNames(typeof(Symbol))));
        }

        private bool BeAValidSymbol(string symbol)
        {
            return Enum.GetNames(typeof(Symbol)).Contains(symbol);
        }
    }
}
