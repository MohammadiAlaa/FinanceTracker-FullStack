namespace FinanceTracker_API.Services
{
    public class TransactionService : ITransactionService   
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionReadDto>> GetUserTransactionsAsync(Guid userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.Category) 
                .OrderByDescending(t => t.Date)
                .Select(t => new TransactionReadDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Description = t.Description,
                    Date = t.Date,
                    CategoryName = t.Category.Name,
                    CategoryIcon = t.Category.Icon,
                    Type = t.Type
                }).ToListAsync();
        }
        public async Task<bool> AddTransactionAsync(Guid userId, TransactionUpsertDto model)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = model.Amount,
                Description = model.Description,
                Date = model.Date,
                Type = model.Type,
                CategoryId = model.CategoryId,
                UserId = userId
            };

            _context.Transactions.Add(transaction);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<TransactionSummaryDto> GetUserDashboardSummaryAsync(Guid userId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.UserId == userId)
                .ToListAsync();

            var totalIncome = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var totalExpenses = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

            return new TransactionSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                Balance = totalIncome - totalExpenses
            };
        }

        public async Task<bool> UpdateTransactionAsync(Guid userId, Guid transactionId, TransactionUpsertDto model)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == transactionId && t.UserId == userId);

            if (transaction == null) return false;

            transaction.Amount = model.Amount;
            transaction.Description = model.Description;
            transaction.Date = model.Date;
            transaction.Type = model.Type;
            transaction.CategoryId = model.CategoryId;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTransactionAsync(Guid userId, Guid transactionId)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == transactionId && t.UserId == userId);

            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
