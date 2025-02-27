namespace IssueTracker.Api.VipApiEndpoings;

public static class Extensions
{

    public static IEndpointRouteBuilder AddVipApiEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("vipapi");
        group.MapGet("/problems/{problemId:guid}", () => "Awesome");

        return group;
    }
}
public record VipIssueReadModel
{
    public Guid IssueId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Status { get; set } = string.Empty;
}