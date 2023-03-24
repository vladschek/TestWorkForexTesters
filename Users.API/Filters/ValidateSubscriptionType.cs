using Common.DTOs.Subscription;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Users.API.Validators;

namespace Users.API.Filters
{
    public class ValidateSubscriptionType : ActionFilterAttribute
    {
        private readonly string _parameterName;

        public ValidateSubscriptionType(string parameterName)
        {
            _parameterName = parameterName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue(_parameterName, out var subscriptionObject))
            {
                var subscription = subscriptionObject as string;
                if (subscription != null)
                {
                    var validator = context.HttpContext.RequestServices.GetService<SubscriptionTypeValidator>();

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
