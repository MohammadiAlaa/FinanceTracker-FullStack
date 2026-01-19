namespace FinanceTracker_API.Dtos
{
    public class TransactionUpsertDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public Guid CategoryId { get; set; }
    }
}
