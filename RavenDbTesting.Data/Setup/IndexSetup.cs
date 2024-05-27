using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;

namespace RavenDbTesting.Data.Setup;

public static class IndexSetup
{
    public static void DeployIndexes(this IDocumentStore store)
    {
        IndexCreation.CreateIndexes(typeof(IndexSetup).Assembly, store);
    }
}