using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolMS.WEB.Models.Users
{
    public class PasswordResetDto
    {
        [Required, PasswordPropertyText]
        public string OldPassword { get; set; }

        [Required, PasswordPropertyText]
        public string NewPassword { get; set; }

        [Required, PasswordPropertyText]
        public string ConfirmPassword { get; set; }

    }
}
