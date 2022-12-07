global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel;

namespace SchoolLMS.Web.Models.DTOs.Users
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        [PasswordPropertyText]
        public string ConfirmPassword { get; set; }

        

    }
}
