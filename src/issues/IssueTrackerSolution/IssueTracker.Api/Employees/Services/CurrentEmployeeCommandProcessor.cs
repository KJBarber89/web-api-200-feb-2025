using IssueTracker.Api.Employees.Api;
using IssueTracker.Api.Employees.Domain;

namespace IssueTracker.Api.Employees.Services;

public class CurrentEmployeeCommandProcessor(IHttpContextAccessor context): IProcessCommandsForTheCurrentEmployee
{

    public Task<ProblemSubmitted> ProcessProblemAsync(SubmitProblem problem)
    {
        var sub = context?.HttpContext?.User.Identity?.Name;
        //need employee to do this need the sub claim
        //Lookup it up in the database, if isn't there, create it
        //        if it is there, load, tell it process '
        throw new Exception();
    }
}
