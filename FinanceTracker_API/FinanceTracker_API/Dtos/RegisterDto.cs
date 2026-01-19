namespace FinanceTracker_API.Dtos
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
