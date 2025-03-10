namespace BankingService.Models
{
    public class TransactionCustom
    {
        public string? Name { get; set; }
        public decimal? Amount { get; set; }
        public string? MerchantName { get; set; }
        public DateOnly? Date { get; set; } 
        public string? MerchantAddress { get; set; }
    }
}
