namespace FinanceTracker_API.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public TransactionType Type { get; set; }

        
        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
