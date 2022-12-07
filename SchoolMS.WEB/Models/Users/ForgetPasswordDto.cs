using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace SchoolMS.WEB.Models.Users
{
    public class ForgetPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Otp { get; set; }

        [Required, PasswordPropertyText]
        public string NewPassword { get; set; }

        [Required, PasswordPropertyText]
        public string ConfirmPassword { get; set; }
    }
}
