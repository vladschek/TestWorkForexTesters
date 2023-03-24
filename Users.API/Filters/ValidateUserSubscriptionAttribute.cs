using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Users.API.Validators;

namespace Users.API.Filters
{
    public class ValidateUserSubscriptionAttribute : ActionFilterAttribute
    {
        private readonly string _parameterName;

        public ValidateUserSubscriptionAttribute(string parameterName)
        {
            _parameterName = parameterName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue(_parameterName, out var userObject))
            {
                var user = userObject as UserCreateDTO;

                if (user != null)
                {
                    var subscription = user.Subscription;

                    if (subscription != null)
                    {
                        var validator = context.HttpContext.RequestServices.GetService<SubscriptionDTOValidator>();

                        var validationResult = validator.Validate(subscription);

                        if (!validationResult.IsValid)
                        {
                            context.Result = new BadRequestObjectResult(validationResult.Errors);
                        }
                    }
                }
                else
                {
                    context.Result = new BadRequestObjectResult("Invalid user object.");
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult($"Missing {_parameterName} parameter.");
            }
        }
    }
}
