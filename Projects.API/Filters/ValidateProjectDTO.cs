using Common.DTOs;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Projects.API.Validator;

namespace Projects.API.Filters
{
    public class ValidateProjectDTO : ActionFilterAttribute
    {
        private readonly string _parameterName;

        public ValidateProjectDTO(string parameterName)
        {
            _parameterName = parameterName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue(_parameterName, out var userObject))
            {
                var project = userObject as ProjectCreateDTO;
                
                if (project != null)
                {
                    var charts = project.Charts;
                    var validator = context.HttpContext.RequestServices.GetService<ChartDtoValidator>();
                    if (charts != null)
                    {
                        foreach(var chart in charts)
                        {
                            var validationResult = validator.Validate(chart);

                            if (!validationResult.IsValid)
                            {
                                context.Result = new BadRequestObjectResult(validationResult.Errors);
                            }
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
