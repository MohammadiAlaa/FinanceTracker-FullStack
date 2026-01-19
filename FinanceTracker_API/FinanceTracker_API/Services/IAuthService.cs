namespace FinanceTracker_API.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto model);
        Task<AuthResponseDto> LoginAsync(LoginDto model);
        Task<AuthResponseDto> LogoutAsync();
        Task<AuthResponseDto> DeleteAccountAsync(Guid userId, DeleteAccountDto model);
    }
}
