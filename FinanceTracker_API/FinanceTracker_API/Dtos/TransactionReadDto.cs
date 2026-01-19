namespace FinanceTracker_API.Dtos
{
    public class TransactionReadDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryIcon { get; set; } = string.Empty;

        public TransactionType Type { get; set; }
    }
}
