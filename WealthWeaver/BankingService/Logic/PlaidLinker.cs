using Going.Plaid;
using Going.Plaid.Entity;
using Microsoft.Extensions.Options;
using BankingService.Interfaces;
using BankingService.Models;

namespace BankingService
{
    /// <summary>
    /// Handles interactions with the Plaid API for creating link tokens, exchanging public tokens, and fetching transactions.
    /// Plaid Link must be initialized on Client Side after getting the link-token for users' id using the JavaScript Library for Plaid Link
    /// User then calls ExchangePublicTokenAsync to change the token and link bank
    /// </summary>
    internal class PlaidLinker : IPlaidLinker
    {
        private readonly PlaidClient _plaidClient;

        public PlaidLinker(
            IOptions<PlaidOptionsCustom> options,
            IHttpClientFactory httpClientFactory,
            ILogger<PlaidClient> clientLogger)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(httpClientFactory);
            ArgumentNullException.ThrowIfNull(clientLogger);

            var plaidEnvironment = options.Value.Environment.ToString().ToLower(Constants.DefaultCulture) switch
            {
                "sandbox" => Going.Plaid.Environment.Sandbox,
                "development" => Going.Plaid.Environment.Development,
                "production" => Going.Plaid.Environment.Production,
                _ => throw new ArgumentException("Invalid environment")
            };

            _plaidClient = new PlaidClient(
                plaidEnvironment,
                options.Value.ClientId,
                options.Value.Secret,
                options.Value.DefaultAccessToken, // Optional
                httpClientFactory,
                clientLogger,
                options.Value.ApiVersion // Optional
            );
        }

        /// <summary>
        /// Creates a link token for a given user ID.
        /// </summary>
        /// <param name="userId">The user ID for which to create the link token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the link token.</returns>
        /// <exception cref="HttpRequestException">Thrown when there is an error creating the link token.</exception>
        public async Task<string> CreateLinkTokenAsync(string userId)
        {
            var response = await _plaidClient.LinkTokenCreateAsync(new()
            {
                User = new() { ClientUserId = userId },
                ClientName = "WealthWeaverPlaidLinker",
                Products = new[] { Products.Transactions },
                CountryCodes = new[] { CountryCode.Us },
                Language = Constants.DefaultLanguage,
            }).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error creating link token: {response.Error?.ErrorMessage}");

            return response.LinkToken;
        }

        /// <summary>
        /// Exchanges a public token for an access token.
        /// </summary>
        /// <param name="publicToken">The public token to exchange.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the access token.</returns>
        /// <exception cref="HttpRequestException">Thrown when there is an error exchanging the public token.</exception>
        public async Task<string> ExchangePublicTokenAsync(string publicToken)
        {
            var response = await _plaidClient.ItemPublicTokenExchangeAsync(new()
            {
                PublicToken = publicToken
            }).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error exchanging public token: {response.Error?.ErrorMessage}");

            return response.AccessToken;
        }

        /// <summary>
        /// Fetches transactions for a given access token and date range.
        /// </summary>
        /// <param name="accessToken">The access token to use for fetching transactions.</param>
        /// <param name="startDate">The start date of the transaction period.</param>
        /// <param name="endDate">The end date of the transaction period.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of transactions.</returns>
        /// <exception cref="HttpRequestException">Thrown when there is an error fetching transactions.</exception>
        public async Task<IEnumerable<TransactionCustom>> GetTransactionsAsync(string accessToken, DateOnly startDate, DateOnly endDate)
        {
            var response = await _plaidClient.TransactionsGetAsync(new()
            {
                AccessToken = accessToken,
                StartDate = startDate,
                EndDate = endDate
            }).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error fetching transactions: {response.Error?.ErrorMessage}");

            return response.Transactions.Select(t => new TransactionCustom
            {
                Name = t.Name,
                Amount = t.Amount,
                MerchantName = t.MerchantName,
                Date = t.Date!.HasValue ? t.Date.Value : null,
                MerchantAddress = t.Location!.Address
            });
        }
    }
}