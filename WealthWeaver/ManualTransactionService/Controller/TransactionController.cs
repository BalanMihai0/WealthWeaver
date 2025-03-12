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
            await _transactionService.AddTransactionAsync(request);
            return Ok();
        }

        [HttpPost("removetransaction")]
        public async Task<IActionResult> RemoveTransactions([FromBody] TransactionModel request)
        {
            await _transactionService.RemoveTransactionAsync(request);
            return Ok();
        }
    }
}
