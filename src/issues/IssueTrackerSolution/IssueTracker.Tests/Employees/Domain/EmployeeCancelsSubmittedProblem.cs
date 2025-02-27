using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTracker.Api.Employees.Api;
using IssueTracker.Api.Middleware;
using IssueTracker.Tests.Fixtures;

namespace IssueTracker.Tests.Employees.Domain
{
    [Trait("Category", "UnitIntegration")]
    [Collection("UnitIntegration")]
    public class EmployeeCancelsSubmittedProblem(UnitIntegrationTestFixture fixture)
    {
        [Fact]
        public async Task Sketch()
        {

            await using var session = fixture.Store.LightweightSession();

            var createEvent = new EmployeeCreated(Guid.NewGuid(), "Carlo");

            session.Events.StartStream(createEvent.Id, createEvent);//get a connection to DB
            //if an employee
            ////create a new employee 
            ///
            var employeeSubmittedProblemEvent = new EmployeeSubmittedAProblem(Guid.NewGuid(), createEvent.Id, Guid.Parse(SeededSoftware.Rider), "Broken");
            session.Events.StartStream(employeeSubmittedProblemEvent.ProblemId, employeeSubmittedProblemEvent);

            await session.SaveChangesAsync();
            ///have employee submited a problem - has submitted a problem and the problem is still in the status of 'submitted',
            //then it should be cancelled (removed, vetoed, w/e)

            var readModelBeforeCancel = await session.Events.AggregateStreamAsync<EmployeeProblemReadModel>(employeeSubmittedProblemEvent)
            Assert.NotNull(readModelBeforeCancel);
            readModelBeforeCancel.Status.ShouldBe("Submitted");

            session.Events.Append(employeeSubmittedProblemEvent.ProblemId, new ProblemCancelledByUser());

            //chk readmodel?
            //if not - it should stayyyy

            var readModelAfterCancel = await session.Events.AggregateStreamAsync<EmployeeProblemReadModel>(employeeSubmittedProblemEvent)
            Assert.NotNull(readModelBeforeCancel);
        }
    }
}
