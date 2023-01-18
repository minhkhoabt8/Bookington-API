using Bookington.Infrastructure.DTOs.ApiResponse;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bookington_Api.Filters;
public class AutoValidateModelState : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = ResponseFactory.BadRequest(context.ModelState);
        }
    }
}