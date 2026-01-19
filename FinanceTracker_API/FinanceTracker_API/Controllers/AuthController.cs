namespace FinanceTracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);
            if (!result.IsSuccess) return Unauthorized(result.Message);

            return Ok(result);
        }

        [Authorize] 
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.LogoutAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("delete-account")]
        public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountDto model)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return Unauthorized();

            var result = await _authService.DeleteAccountAsync(Guid.Parse(userIdString), model);

            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }
    }
}
