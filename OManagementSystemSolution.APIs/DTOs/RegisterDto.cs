using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.ComponentModel.DataAnnotations;

namespace OManagementSystemSolution.APIs.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        //[RegularExpression(pattern: "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$",
        //ErrorMessage = "Password should at least has one uppercase letter, one lowercase letter and one number")]
        public string Password { get; set; }
    }
}
