using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Bookington_Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode;
            object response;

            switch (exception)
            {
                case InputValidationException valEx:
                    statusCode = 400;
                    response = new ApiBadRequestResponse(valEx.ValidationResults, exception.Message);
                    break;
                case HandledException handledEx:
                    statusCode = handledEx.StatusCode;
                    response = new ApiResponse(handledEx.StatusCode, isError: true, message: exception.Message);
                    break;
                // Unexpected exceptions
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    response = new ApiInternalServerErrorResponse(exception.ToString());
                    break;
            }

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
