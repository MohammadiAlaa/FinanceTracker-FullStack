namespace FinanceTracker_API.Controllers
{
    [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionReadDto>>> GetMyTransactions()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdString == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdString);

            var transactions = await _transactionService.GetUserTransactionsAsync(userId);

            return Ok(transactions);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var summary = await _transactionService.GetUserDashboardSummaryAsync(userId);
            return Ok(summary);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionUpsertDto model)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdString == null) 
                return Unauthorized();

            var userId = Guid.Parse(userIdString);

            var result = await _transactionService.AddTransactionAsync(userId, model);

            if (!result)
                return BadRequest("Could not save the transaction.");

            return Ok(new { message = "Transaction added successfully!" });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TransactionUpsertDto model)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _transactionService.UpdateTransactionAsync(userId, id, model);
            if (!result) return NotFound("Transaction not found or update failed.");
            return Ok(new { message = "Updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdString == null) 
                return Unauthorized();

            var userId = Guid.Parse(userIdString);

            var result = await _transactionService.DeleteTransactionAsync(userId, id);

            if (!result) 
                return NotFound("Transaction not found or you don't have permission.");

            return Ok(new { message = "Transaction deleted successfully." });
        }
    }
}
