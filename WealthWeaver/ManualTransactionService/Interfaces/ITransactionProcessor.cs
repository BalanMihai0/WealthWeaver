using ManualTransactionService.Models;

namespace ManualTransactionService.Services
{
    public interface ITransactionProcessor
    {
        Task AddTransactionAsync(TransactionModel transaction);
        Task RemoveTransactionAsync(string transactionId);
    }
}