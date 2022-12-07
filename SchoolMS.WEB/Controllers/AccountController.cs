using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Mvc;
using SchoolLMS.Bll.Email;
using SchoolLMS.Bll.Role;
using SchoolLMS.Bll.Users;
using SchoolLMS.Domain.Models.Users;
using SchoolMS.WEB.Models.Users;
using System.Security.Claims;

namespace SchoolMS.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRoleServices _role;
        private readonly IEmailServices _email;
        private readonly IUserServices _user;

        public AccountController(IEmailServices email, IUserServices user, IRoleServices role)
        {
            _role = role;
            _email = email;
            _user = user;

        }

        public IActionResult Register() => View();


        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserRegisterDto dto)
        {




            string date = DateTime.UtcNow.ToString();


            string imageName = "noimage.png";
           

          

            Random grnrator = new Random();
            string num = grnrator.Next(1, 1000).ToString("D4");









                if (dto.Password == dto.confirmPassword)
                {
                    var user = new AppUsers
                    {
                        Name = dto.Name,
                        Email = dto.Email,
                        Password = _user.CreatePasswordHash(dto.Password),
                        Image = imageName,
                        Verify= false,
                        Token = num,
                        CreatedDate = DateTime.UtcNow,


                    };



                //    _user.Add(user);


                    _email.SendEmail(dto.Email, num, dto.Name);

                    string email = dto.Email;

                    TempData["Email"] = email;



                    return RedirectToAction("Otp");
                }

                else
                {
                    return BadRequest("password dosn't match!");


                }
            

            return BadRequest("user on this email is already exixt");

        }












        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserLogindto dto)
        {



            var user = _user.GetUserByEmail(dto.Email);


            if (user.Verify == true)
            {




                if (!_user.VirifyPassword(dto.Password, user.Password))
                {
                    return BadRequest("password dos't match");


                }

                int userId = user.Id;

                var UserRole = _role.GetRoleByuserId(userId).ToArray();



                var claims = new List<Claim>() {


                    new Claim(ClaimTypes.NameIdentifier, user.Email ),


        };

                foreach (var item in UserRole)
                {

                    claims.Add(new Claim(ClaimTypes.Role, item.RName));
                }



                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);



                var principal = new ClaimsPrincipal(identity);




                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                          new ClaimsPrincipal(identity),
                          new AuthenticationProperties
                          {
                              ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                              IsPersistent = true,

                          });


                return RedirectToRoute("Default");

            }



            else
            {
                return BadRequest("you fucking ediot.. go to hell and virify your self");
            }








        }




        public IActionResult Otp() => View();

        [HttpPost]
        public IActionResult Otp(string otp)
        {
            string Email = (string)TempData["Email"];

            var user = _user.GetUserByEmail(Email);



            if (user.CreatedDate.AddMinutes(15) == DateTime.UtcNow.AddMinutes(15))
            {
                if (user.Token == otp)
                {
                    return RedirectToAction("Login");
                }

                else
                {
                    return BadRequest("opp! your fucking Otp is don,t match......");

                }
            }

            else
            {
                return BadRequest("OhO.... Fuck !!! your fucking link was expried");
            }
        }



       
        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);



            return LocalRedirect("/");
        }






    }
}
