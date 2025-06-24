

using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    public class UserRegisterDTO
    {
        public int IdUser { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        [MaxLength(80, ErrorMessage = "Name cannot exceed 80 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        [MaxLength(250, ErrorMessage = "Email cannot exceed 250 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not the same")]
        public string PasswordConfirm { get; set; }

        public string Role { get; set; }
    }
}
