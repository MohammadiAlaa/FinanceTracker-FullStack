namespace FinanceTracker_API.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // مهم جداً لاستدعاء إعدادات جداول الـ Identity الأساسية
            base.OnModelCreating(modelBuilder);

            // ضبط دقة رقم العمليات المالية (Money Precision)
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade); // لو اتمسح اليوزر تتمسح معاملاته

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // نمنع مسح فئة مرتبطة بمعاملات

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = Guid.NewGuid(), 
                    Name = "Food & Drinks",
                    Icon = "🍔",
                    Color = "bg-orange-500"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Transport",
                    Icon = "🚗",
                    Color = "bg-blue-500"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Shopping",
                    Icon = "🛍️",
                    Color = "bg-purple-500"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Salary",
                    Icon = "💰",
                    Color = "bg-green-500"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Rent",
                    Icon = "🏠",
                    Color = "bg-red-500"
                }
            );
        }
    }
}
