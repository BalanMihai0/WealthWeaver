using BankingService.Interfaces;
using BankingService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingService.Services
{
    public class BankingServiceClient : IBankingServiceClient
    {
        private readonly IPlaidLinker _plaidLinker;

        public BankingServiceClient(IPlaidLinker plaidLinker)
        {
            _plaidLinker = plaidLinker;
        }

        public async Task<string> GetLinkTokenAsync(string userId)
        {
            return await _plaidLinker.CreateLinkTokenAsync(userId).ConfigureAwait(false);
        }

        public async Task<string> ExchangePublicTokenAsync(string publicToken)
        {
            return await _plaidLinker.ExchangePublicTokenAsync(publicToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TransactionCustom>> GetTransactionsAsync(string accessToken, DateOnly startDate, DateOnly endDate)
        {
            return await _plaidLinker.GetTransactionsAsync(accessToken, startDate, endDate).ConfigureAwait(false);
        }
    }
}

