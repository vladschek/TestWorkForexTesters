using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Text.Json;

namespace Common.Middleware
{
    public sealed class ExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new { error = exception.Message };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
