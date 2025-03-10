namespace UserService.Models
{
    public class ValidateTokenRequest
    {
        public required string Token { get; set; }
        public required string ResponseQueue { get; set; }
    }
}

