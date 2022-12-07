

using SchoolLMS.WEB.Infrasruture;

namespace SchoolMS.WEB.Models.Users
{
    public class UserProfiledto
    {
        public string Name

            { get; set; }


        public string Email { get; set; }

        public string profilePic { get; set; }



        [FileExtension]
        public IFormFile UploadImage { get; set; }


    }
}
