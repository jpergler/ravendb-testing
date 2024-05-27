using Raven.Client.Documents;
using Raven.TestDriver;
using RavenDbTesting.Data.Model;
using RavenDbTesting.Logic.Infrastructure;
using RavenDbTesting.Tests.Infrastructure;

namespace RavenDbTesting.Tests.TestDrivers;

public class UserTestDriver : RavenTestDriver
{
    protected static readonly User MainUser = new()
    {
        Id = "users/1",
        FirstName = "John",
        LastName = "Doe"
    };

    protected static readonly User OtherUser = new()
    {
        Id = "users/2",
        FirstName = "Jane",
        LastName = "Fu"
    };

    protected readonly ICurrentUserProvider MainUserProvider = new TestCurrentUserProvider(MainUser.Id);
    protected readonly ICurrentUserProvider OtherUserProvider = new TestCurrentUserProvider(OtherUser.Id);
    protected readonly ICurrentUserProvider NonExistingUserProvider = new TestCurrentUserProvider("users/non-existing");
    protected readonly ICurrentUserProvider NotAuthenticatedUserProvider = new TestCurrentUserProvider(null);

    protected readonly IDocumentStore DocumentStore;

    protected UserTestDriver()
    {
        DocumentStore = GetDocumentStore();
    }

    protected override void SetupDatabase(IDocumentStore documentStore)
    {
        using var session = documentStore.OpenSession();
        session.Store(MainUser);
        session.Store(OtherUser);
        session.SaveChanges();
    }
}