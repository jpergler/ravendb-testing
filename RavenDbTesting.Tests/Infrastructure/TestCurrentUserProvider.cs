using RavenDbTesting.Logic.Infrastructure;

namespace RavenDbTesting.Tests.Infrastructure;

public class TestCurrentUserProvider(string? userId) : ICurrentUserProvider
{
    public string? UserId => userId;
}