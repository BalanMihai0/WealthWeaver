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
        public async Task<IActionResult> ConfirmAuthorization([FromBody] ValidateTokenMessage request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var message = new ValidateTokenMessage { Token = request.Token, ResponseQueue = "auth_response_queue" };
            await _rabbitMqClient.SendMessageAsync(message).ConfigureAwait(true);

            return Ok();
        }
    }
}
