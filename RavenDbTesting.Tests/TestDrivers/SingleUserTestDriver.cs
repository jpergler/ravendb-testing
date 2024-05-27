using Moq;
using Raven.Client.Documents;
using Raven.TestDriver;
using RavenDbTesting.Data.Model;
using RavenDbTesting.Logic.Infrastructure;

namespace RavenDbTesting.Tests.TestDrivers;

public class SingleUserTestDriver : RavenTestDriver
{
    protected static readonly User MainUser = new()
    {
        Id = "users/1",
        FirstName = "John",
        LastName = "Doe"
    };

    protected readonly ICurrentUserProvider MainUserProvider;

    protected readonly IDocumentStore DocumentStore;

    protected SingleUserTestDriver()
    {
        var mainUserProviderMock = new Mock<ICurrentUserProvider>();
        mainUserProviderMock.Setup(x => x.CurrentUserId).Returns(MainUser.Id);
        mainUserProviderMock.Setup(x => x.IsAuthenticated).Returns(true);
        MainUserProvider = mainUserProviderMock.Object;

        DocumentStore = GetDocumentStore();
    }
}