namespace SchoolLMS.Web.Models.DTOs.Users
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("")]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
