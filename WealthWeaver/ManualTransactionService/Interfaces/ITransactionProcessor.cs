// ITransactionService.cs
using ManualTransactionService.Models;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ManualTransactionService.Services
{
    public interface ITransactionProcessor
    {
        Task AddTransactionAsync(TransactionModel transaction);
        Task RemoveTransactionAsync(TransactionModel transaction);
    }
}

// TransactionService.cs