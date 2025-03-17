using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserService.Interfaces;
using UserService.Models;
using Microsoft.Extensions.Options;

namespace UserService.Services
{
    internal sealed class TokenVerifier : ITokenVerifier
    {
        private readonly CloudflareOptions _cloudflareOptions;

        public TokenVerifier(IOptions<CloudflareOptions> options)
        {
            _cloudflareOptions = options.Value;
        }

        public async Task<bool> VerifyTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_cloudflareOptions.Secret);
            ArgumentNullException.ThrowIfNull(key);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _cloudflareOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _cloudflareOptions.Audience,
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
