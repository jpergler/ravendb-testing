using Newtonsoft.Json;

namespace RavenDbTesting.Data.Model;

public class User
{
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    [JsonIgnore]
    public string FullName => $"{FirstName} {LastName}";
}