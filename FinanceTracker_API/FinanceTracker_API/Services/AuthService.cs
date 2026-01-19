namespace FinanceTracker_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded)
                return new AuthResponseDto { IsSuccess = false, Message = string.Join(", ", result.Errors.Select(e => e.Description)) };

            return new AuthResponseDto { IsSuccess = true, Message = "Account created successfully" };

        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return new AuthResponseDto { IsSuccess = false, Message = "Email or password is incorrect" };

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = token,
                Username = user.UserName?.Split('@')[0], 
                Message = "Login successful"
            };
        }

        public async Task<AuthResponseDto> LogoutAsync()
        {
            return new AuthResponseDto { IsSuccess = true, Message = "Logged out successfully" };
        }

        public async Task<AuthResponseDto> DeleteAccountAsync(Guid userId, DeleteAccountDto model)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return new AuthResponseDto { IsSuccess = false, Message = "User not found" };

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordValid)
                return new AuthResponseDto { IsSuccess = false, Message = "Incorrect password. Delete failed." };

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return new AuthResponseDto { IsSuccess = false, Message = "Error occurred while deleting account" };

            return new AuthResponseDto { IsSuccess = true, Message = "Account deleted forever" };
        }

        private string GenerateJwtToken(User user)
        {
            var expirationDays = double.Parse(_config["Jwt:DurationInDays"] ?? "7");
            var expires = DateTime.UtcNow.AddDays(expirationDays);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
