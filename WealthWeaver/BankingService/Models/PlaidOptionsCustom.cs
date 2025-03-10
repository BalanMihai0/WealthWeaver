using Going.Plaid;

namespace BankingService.Models
{
    internal sealed class PlaidOptionsCustom
    {
        public required string ClientId { get; set; }
        public required string Secret { get; set; }
        public required string Environment { get; set; }
        public string? DefaultAccessToken { get; set; }
        public required ApiVersion ApiVersion { get; set; }
    }
}
