using Moq;
using Raven.Client.Documents;
using Raven.TestDriver;
using RavenDbTesting.Data.Model;
using RavenDbTesting.Logic.Infrastructure;

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

    protected readonly ICurrentUserProvider MainUserProvider;

    protected readonly IDocumentStore DocumentStore;

    protected UserTestDriver()
    {
        var mainUserProviderMock = new Mock<ICurrentUserProvider>();
        mainUserProviderMock.Setup(x => x.CurrentUserId).Returns(MainUser.Id);
        mainUserProviderMock.Setup(x => x.IsAuthenticated).Returns(true);
        MainUserProvider = mainUserProviderMock.Object;

        DocumentStore = GetDocumentStore();
    }
}