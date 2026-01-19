namespace FinanceTracker_API.Dtos
{
    public class TransactionSummaryDto
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal Balance { get; set; }
    }
}
