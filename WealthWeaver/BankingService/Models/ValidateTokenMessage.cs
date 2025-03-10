namespace BankingService.Models
{
    public class ValidateTokenMessage
    {
        public required string Token { get; set; }
        public required string ResponseQueue { get; set; }
    }
}
