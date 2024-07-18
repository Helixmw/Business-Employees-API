using System.ComponentModel.DataAnnotations;

namespace Employees_API.DTOs.Users
{
    public class LoginUserDTO : ILoginUserDTO
    {
        [Required(ErrorMessage = "Please enter your password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Make sure you enter a valid email")]
        public string? Email { get; set; }
    }
}
