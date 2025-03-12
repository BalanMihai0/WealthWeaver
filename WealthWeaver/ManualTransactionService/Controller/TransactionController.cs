using ManualTransactionService.Models;
using ManualTransactionService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingService.Controllers
{
    [ApiController]
    [Route("api/manual")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ITransactionProcessor _transactionService;

        public AuthorizationController(ITransactionProcessor transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("addtransaction")]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionModel request)
        {
            ArgumentNullException.ThrowIfNull(request);
            await _transactionService.AddTransactionAsync(request).ConfigureAwait(true);
            return Ok();
        }

        [HttpPost("removetransaction")]
        public async Task<IActionResult> RemoveTransactions([FromBody] string requestId)
        {
            ArgumentNullException.ThrowIfNull(requestId);
            await _transactionService.RemoveTransactionAsync(requestId).ConfigureAwait(true);
            return Ok();
        }
    }
}
