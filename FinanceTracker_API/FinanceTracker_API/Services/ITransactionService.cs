namespace FinanceTracker_API.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionReadDto>> GetUserTransactionsAsync(Guid userId);
        Task<bool> AddTransactionAsync(Guid userId, TransactionUpsertDto model);
        Task<bool> DeleteTransactionAsync(Guid userId, Guid transactionId);
        Task<TransactionSummaryDto> GetUserDashboardSummaryAsync(Guid userId);
        Task<bool> UpdateTransactionAsync(Guid userId, Guid transactionId, TransactionUpsertDto model);
    }
}
