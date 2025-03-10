using Microsoft.AspNetCore.Mvc;
using BankingService.Models;
using BankingService.Interfaces;

namespace BankingService.Controllers
{
    [ApiController]
    [Route("api/banking")]
    public class BankingController : ControllerBase
    {
        private readonly IBankingServiceClient _bankingServiceClient;

        public BankingController(IBankingServiceClient bankingServiceClient)
        {
            _bankingServiceClient = bankingServiceClient;
        }

        [HttpGet("link-token/{userId}")]
        public async Task<IActionResult> GetLinkToken(string userId)
        {
            var linkToken = await _bankingServiceClient.GetLinkTokenAsync(userId).ConfigureAwait(true);
            return Ok(linkToken);
        }

        [HttpPost("exchange-token")]
        public async Task<IActionResult> ExchangePublicToken(ExchangeTokenRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var accessToken = await _bankingServiceClient.ExchangePublicTokenAsync(request.PublicToken).ConfigureAwait(true);
            return Ok(accessToken);
        }


        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions(string accessToken, DateOnly startDate, DateOnly endDate)
        {
            var transactions = await _bankingServiceClient.GetTransactionsAsync(accessToken, startDate, endDate).ConfigureAwait(true);
            return Ok(transactions);
        }

        [HttpGet("health")]
        public IActionResult CheckHealth()
        {
            return Ok();
        }
    }
}