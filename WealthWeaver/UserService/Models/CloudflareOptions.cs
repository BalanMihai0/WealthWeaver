namespace UserService.Models
{
    internal sealed class CloudflareOptions
    {
        public required string Secret { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}
