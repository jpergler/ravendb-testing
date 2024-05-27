using System.Diagnostics.CodeAnalysis;

namespace RavenDbTesting.Logic.Infrastructure;

public interface ICurrentUserProvider
{
    public string? UserId { get; }

    [MemberNotNullWhen(returnValue: true, member: nameof(UserId))]
    public bool IsAuthenticated => UserId is not null;
    
    public string RequiredUserId => UserId ?? throw new InvalidOperationException("User is not authenticated.");
}