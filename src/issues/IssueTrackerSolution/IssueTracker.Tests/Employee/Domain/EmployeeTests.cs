
using System.Threading.Tasks;
using IssueTracker.Api.Employee.Domain;
using IssueTracker.Tests.Fixtures;
using Marten;

namespace IssueTracker.Tests.Employee.Domain;

[Trait("Category", "UnitIntegration")] // this is metadata so we can run just some of these at a time.
[Collection("UnitIntegration")] // everything here should use the same database as everything else in this "collection"
public class EmployeeTests(UnitIntegrationTestFixture fixture)
{
    [Fact]

    public async Task CanCreateAnEmployee()
    {
        IDocumentSession session = fixture.Store.LightweightSession(); 


        var repository = new EmployeeRepository(session); // a thing that handles persistence.
        var sub = "bob@company";
        var emp = repository.Create(sub);

        await emp.SaveAsync();
        var emp2 = await repository.GetByIdAsync(emp.Id);

        Assert.NotNull(emp2);
        Assert.Equal(emp.Id, emp2.Id);
        var emp3 = await repository.GetBySubAsync(sub);
        Assert.NotNull(emp3);
        //Assert.Equal(/// no sub?? we should probably check that)
        //backdoor where i can chk to make sure the tech requ are met
        var loadedEntity = await session.LoadAsync<EmployeeEntity>(emp.Id);
        Assert.NotNull(loadedEntity);
        Assert.Equal(loadedEntity.Sub, sub);

        




    }

    [Fact]
    public void EmployeeCanSubmitProblems()
    {

    }
}
