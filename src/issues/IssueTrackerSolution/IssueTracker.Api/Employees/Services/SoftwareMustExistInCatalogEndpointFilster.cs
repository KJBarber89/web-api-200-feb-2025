
using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace IssueTracker.Api.Employees.Services;

public class SoftwareMustExistInCatalogEndpointFilster(NpgsqlConnection connection, ILogger<SoftwareMustExistInCatalogEndpointFilster>logger) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        //take a look at the parameter in the url called softwareid
        //if it isn't there, this thing is being used incorrectly.  a dev screwed up
        //if its there, chk to see if we have th@softwre and if its there, do nothing(next(context))
        //if it isn't - 404 that bad boy 
        var softwareId = context.HttpContext.GetRouteValue("softwareId") as Guid?;

        if (softwareId is null)
        {
            logger.LogError("filter used on path w/o a software id ");
            return TypedResults.NotFound("Software ain't there, try again");

        }
        var sql = $"SELECT EXISTS(SELECT 1 from Catalog Where Id=uuid(:id))";
        var paramMap = new { id = softwareId };
        var softwareExists = await connection.ExecuteScalarAsync<bool>(sql, paramMap);
        if (softwareExists == false)
        {
            return TypedResults.NotFound("software not in the cagtalog, can't create it");
        }
        return await next(context);

        //im cool, call the method- all good here

    }
}
