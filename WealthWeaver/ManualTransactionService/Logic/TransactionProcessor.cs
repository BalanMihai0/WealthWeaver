using ManualTransactionService.Models;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace ManualTransactionService.Services
{
    public class TransactionProcessor : ITransactionProcessor
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public TransactionProcessor(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer("DatabaseId", "ContainerId");
        }

        public async Task AddTransactionAsync(TransactionModel transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction);
            await _container.CreateItemAsync(transaction, new PartitionKey(transaction.Id)).ConfigureAwait(true);
        }

        public async Task RemoveTransactionAsync(TransactionModel transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction);
            await _container.DeleteItemAsync<TransactionModel>(transaction.Id, new PartitionKey(transaction.Id)).ConfigureAwait(true);
        }
    }
}
