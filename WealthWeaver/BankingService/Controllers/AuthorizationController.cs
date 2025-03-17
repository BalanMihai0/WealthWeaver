using Microsoft.AspNetCore.Mvc;
using BankingService.Logic;
using BankingService.Models;

namespace BankingService.Controllers
{
    [ApiController]
    [Route("internal/auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly RabbitMQClient _rabbitMqClient;

        public AuthorizationController(RabbitMQClient rabbitMqClient)
        {
            _rabbitMqClient = rabbitMqClient;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ConfirmAuthorization([FromBody] string request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            await _rabbitMqClient.SendMessageAsync(request).ConfigureAwait(true);

            return Ok();
        }
    }
}
