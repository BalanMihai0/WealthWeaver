using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserService.Interfaces;

namespace UserService.Services
{
    internal sealed class TokenVerifier : ITokenVerifier
    {
        private readonly IConfiguration _configuration;

        public TokenVerifier(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> VerifyTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Cloudflare:Secret"]!);
            ArgumentNullException.ThrowIfNull(key);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Cloudflare:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Cloudflare:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var validationResult = await tokenHandler.ValidateTokenAsync(token, validationParameters).ConfigureAwait(true);

                return validationResult.IsValid;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                return false; // return false if anything goes wrong, suppressing the code analyzer warning
            }
        }
    }
}
