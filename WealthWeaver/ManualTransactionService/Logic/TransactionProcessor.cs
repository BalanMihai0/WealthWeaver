using ManualTransactionService.Models;
using Microsoft.Azure.Cosmos;

namespace ManualTransactionService.Services
{
    public class TransactionProcessor : ITransactionProcessor
    {
#pragma warning disable S1450 // Private fields only used as local variables in methods should become local variables
        private readonly CosmosClient _cosmosClient;
#pragma warning restore S1450 // Private fields only used as local variables in methods should become local variables
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

        public async Task RemoveTransactionAsync(string transactionId)
        {
            ArgumentNullException.ThrowIfNull(transactionId);
            await _container.DeleteItemAsync<TransactionModel>(transactionId, new PartitionKey(transactionId)).ConfigureAwait(true);
        }
    }
}
