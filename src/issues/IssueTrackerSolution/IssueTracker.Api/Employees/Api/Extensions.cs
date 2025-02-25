namespace IssueTracker.Api.Employees.Api;

public static class Extensions
{
    public static IEndpointRouteBuilder MapEmployees(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("employee")
            .WithTags("Employees")
            .WithDescription("Employee Related Stuff")
            .RequireAuthorization(); //chk to mk sure there is a trusted JWT on the Authoriazation Header.

        group.MapPost("/software/{softwareId:guid}/problems", SubmittingAProblem.SubmitAsync);

        return routes;
    }


}