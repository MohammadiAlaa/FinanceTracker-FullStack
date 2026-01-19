namespace FinanceTracker_API.Models
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
