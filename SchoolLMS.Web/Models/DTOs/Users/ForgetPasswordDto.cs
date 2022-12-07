namespace SchoolLMS.Web.Models.DTOs.Users
{
    public class ForgetPasswordDto
    {
        [Required]
        [PasswordPropertyText]
        public string NewPassword { get; set; }

        [Required]
        [PasswordPropertyText]
        public string ConfirmPassword { get; set; }
    }
}
