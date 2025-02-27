using Marten;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IssueTracker.Api.Employees.Api;

public class CancellingSubmittedProblems
{
    public static async Task<Results<NoContent, Conflict<string>>> CancelAProblem(
        Guid problemId,
        IDocumentSession session,
        CancellationToken token)
    {
        var problem = await session.LoadAsync<EmployeeProblemReadModel>(problemId);
        if(problem is null)
        {
            return TypedResults.NoContent();
        }
        if(problem.Status != "Submitted")
        {
            return TypedResults.Conflict("Cannot be cancelled in this state");
        }
        session.Events.Append(problemId, new ProblemCancelledByUser());
        await session.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}
