using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "The Email is requered")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [MaxLength(250, ErrorMessage = "Email cannot exceed 250 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is requered")]
        [DataType(DataType.Password)]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        public string Password { get; set; }
    }
}
