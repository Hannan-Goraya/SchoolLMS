using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SchoolLMS.Bll.Role;
using SchoolLMS.Bll.Users;
using System.Security.Claims;
using SchoolMS.WEB.Models.Users;
using SchoolLMS.WEB.Infrasruture;
using SchoolLMS.Bll.Email;

namespace SchoolMS.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IEmailServices _email;
        private readonly IWebHostEnvironment _webHost;
        private readonly IUserServices _user;
        private readonly IRoleServices _Role;

        public UserController(IUserServices user, IWebHostEnvironment webHost, IEmailServices email, IRoleServices role)
        {

            _email = email;
            _webHost = webHost;
            _user = user;
            _Role = role;

        }



        public async Task<IActionResult> Profile()
        {

            string email = HttpContext.Response.HttpContext.User.Identity.Name;


            var User = _user.GetUserByEmail(email);






            var ProfileDto = new UserProfiledto
            {
                Name = User.Name,
                Email = User.Email,
                profilePic = User.Image
            };


            return View(ProfileDto);


        }




        








       


        public IActionResult EnterValidEmail() => View();

        [HttpPost]
        public IActionResult EnterValidEmail(string email)
        {
            TempData["Email1"] = email;
            var user = _user.GetUserByEmail(email);

            if (user == null)
            {
                return BadRequest("plz.. enter valid email");

            }

            else
            {
                Guid guid = new Guid();
                var token = guid.ToString();

                string Link = "<a href='" + Url.Action("ResetPassword", "User", new { token }, "https") + "'>Reset Password</a>";

                _email.SendEmail(email, Link, user.Name);


                return RedirectToAction("ForgetPassword");
            }

        }



        public IActionResult ChangePassword(PasswordResetDto dto)
        {

            string Email = HttpContext.Response.HttpContext.User.Identity.Name;

            var user = _user.GetUserByEmail(Email);


            if (!_user.VirifyPassword(dto.OldPassword, user.Password))
            {
                return BadRequest("password dos't match");
            }


            if (dto.NewPassword == dto.ConfirmPassword)
            {
                var hash = _user.CreatePasswordHash(dto.NewPassword);

                _user.UpdatePassword(Email, hash);


                return RedirectToAction("Login");
            }

            else
            {
                return BadRequest("opps! you password and confirm password des't match");
            }


        }




        [HttpPost]
        public async Task<IActionResult> UpdateImage(UserProfiledto dto)
        {
            string date = DateTime.UtcNow.ToString();
            if (dto.UploadImage != null)
            {
                string uploadsDir = Path.Combine(_webHost.WebRootPath, "media/users");

                if (!string.Equals(dto.profilePic, "noimage.png"))
                {
                    string oldImagePath = Path.Combine(uploadsDir, dto.profilePic);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                string imageName = Guid.NewGuid().ToString() + "_" + dto.UploadImage.FileName + "_" + date;
                string filePath = Path.Combine(uploadsDir, imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await dto.UploadImage.CopyToAsync(fs);
                fs.Close();
                dto.profilePic = imageName;
            }



            var email = HttpContext.Response.HttpContext.User.Identity.Name;

            _user.UpdateImage(email, dto.profilePic);

            return View();



        }







        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordDto dto)
        {

            if (dto.NewPassword == dto.ConfirmPassword)
            {
                var passs = _user.CreatePasswordHash(dto.NewPassword);
                string Email = (string)TempData["Email1"];

                _user.UpdatePassword(Email, passs);
                return RedirectToAction("Login");


            }

            else
            {
                return BadRequest("password does not match");
            }


        }



       
































        // Forgrt password












































        private bool IsUserExixts(string email)
        {
            var user = _user.GetUserByEmail(email);

            if (user == null)
            {
                return false;
            }

            return true;
        }



    }
}
