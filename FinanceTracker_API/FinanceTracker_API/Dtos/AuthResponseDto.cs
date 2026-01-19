namespace FinanceTracker_API.Dtos
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? Username { get; set; }
    }
}
