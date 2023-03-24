using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Projects.API.Validator
{
    public class ParameterValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string input = value as string;
            string pattern = "^([a-zA-Z]+=[0-9]+;?)+$";

            if (Regex.IsMatch(input, pattern))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid parameters format.");
            }
        }
    }
}
