using Microsoft.AspNetCore.Mvc.Filters;

namespace IssueTracker.Api.Employees.Services;
//to do for demo- maybe later. use at y our own discretion 
public class ReturnNotFoundIfNoUserFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if(context.HttpContext.User is null)
        {
            return TypedResults.NotFound();
        }
        return await next(context);
    }
}
