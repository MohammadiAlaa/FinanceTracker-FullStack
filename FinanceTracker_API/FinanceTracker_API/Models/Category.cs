namespace FinanceTracker_API.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string Icon { get; set; } = "📦";

        public string Color { get; set; } = "bg-gray-500";

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
