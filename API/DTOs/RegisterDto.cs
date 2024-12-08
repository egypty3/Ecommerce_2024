using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Display Name is required") ]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100,ErrorMessage ="The {0} must be at least {2} characers long",MinimumLength = 6)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
