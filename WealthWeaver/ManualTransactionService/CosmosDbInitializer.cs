using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace ManualTransactionService.Services
{
    public static class CosmosDbInitializer
    {
        public static async Task<Container> InitializeCosmosResourcesAsync(CosmosClient cosmosClient, string databaseId, string containerId)
        {
            ArgumentNullException.ThrowIfNull(cosmosClient);
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId).ConfigureAwait(true);
            Container container = await database.CreateContainerIfNotExistsAsync(containerId, "/Id").ConfigureAwait(true);
            return container;
        }
    }
}
