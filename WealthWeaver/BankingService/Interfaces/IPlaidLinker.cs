using BankingService.Models;

namespace BankingService.Interfaces
{
    public interface IPlaidLinker
    {
        Task<string> CreateLinkTokenAsync(string userId);
        Task<string> ExchangePublicTokenAsync(string publicToken);
        Task<IEnumerable<TransactionCustom>> GetTransactionsAsync(string accessToken, DateOnly startDate, DateOnly endDate);
    }
}