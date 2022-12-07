
using SchoolLMS.WEB.Infrasruture;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolMS.WEB.Models.Users
{
    public class UserRegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required,EmailAddress,RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }

        [Required, PasswordPropertyText,  RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$")]
        public string Password { get; set; }

        [Required, PasswordPropertyText, RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$")]
        public string confirmPassword { get; set; }

    }
}
