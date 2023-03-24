using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Users.API.Validators;
using Common.DTOs.Subscription;

namespace Users.API.Filters
{
    public class ValidateSubscriptionAttribute : ActionFilterAttribute
    {
        private readonly string _parameterName;

        public ValidateSubscriptionAttribute(string parameterName)
        {
            _parameterName = parameterName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue(_parameterName, out var subscriptionObject))
            {
                var subscription = subscriptionObject as SubscriptionDTO;
                if (subscription != null)
                {
                    var validator = context.HttpContext.RequestServices.GetService<SubscriptionDTOValidator>();

                    var validationResult = validator.Validate(subscription);

                    if (!validationResult.IsValid)
                    {
                        context.Result = new BadRequestObjectResult(validationResult.Errors);
                    }
                }
                else
                {
                    context.Result = new BadRequestObjectResult($"Missing {_parameterName} parameter.");
                }
            }

        }
    }
}
