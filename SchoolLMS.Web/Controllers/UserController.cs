using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SchoolLMS.Domain.Models.Users;
using SchoolLMS.Web.Infrastruture.Email;
using SchoolLMS.Web.Models.DTOs.Users;
using System.Security.Claims;

namespace SchoolLMS.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _user;
        private readonly IEmailServices _email;
        private readonly IRoleServices _role;

        public UserController(IUserServices user, IEmailServices email, IRoleServices role)
        {
            _user = user;
            _email = email;
            _role = role;
        }


        //Register
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterDto dto)
        {
            if (ModelState.IsValid)
            {

                if (dto.Password == dto.ConfirmPassword)
                {

                    string date = DateTime.UtcNow.ToString();


                    string imageName = "noimage.png";


                    Guid guid = new Guid();
                    var token = guid.ToString();




                    if (IsUserExist(dto.Email) == false)
                    {


                        var user = new AppUsers
                        {
                            Name = dto.Name,
                            Email = dto.Email,
                            Password = _user.CreatePasswordHash(dto.Password),
                            Image = imageName,
                            Verify = false,
                            Token = token,
                            CreatedDate = DateTime.UtcNow,


                        };

                        string Link = "<a href='" + Url.Action("Otp", "User", new { token }, "https") + "'>Reset Password</a>";

                        _user.Add(user);


                        _email.SendEmail(dto.Email, Link, dto.Name);

                        string email = dto.Email;

                        TempData["Email"] = email;



                        return RedirectToAction("Otp");
                    }

                    else
                    {
                        return BadRequest("This email has alread Taken ");
                    }
                }


                else
                {
                    return BadRequest("Password does not match");
                }
            }

            else
            {
                return BadRequest("Plz... Enter Valid Imformation");
            }
        }

        // otp
        public IActionResult Otp() => View();

        [HttpPost]
        public IActionResult Otp(string token)
        {
            string Email = (string)TempData["Email"];

            var user = _user.GetUserByEmail(Email);



            if (user.CreatedDate.AddMinutes(15) == DateTime.UtcNow.AddMinutes(15))
            {
                if (user.Token == token)
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
















        //Login
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {

            if (ModelState.IsValid)
            {
                var user = _user.GetUserByEmail(dto.Email);

                

                if (_user.VirifyPassword(dto.Password, user.Password) == true)
                {
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
                    return BadRequest("Wrong email or password!");
                }
            }
            else
            {
                return BadRequest("Plz.. enter valid info");
            }
            
        }




        //logout

        /// <summary>
        /// /////////////////////
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);



            return LocalRedirect("/");
        }













        // forget password


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

                string Link = "<a href='" + Url.Action("ForgetPassword", "User", new { token }, "https") + "'>Reset Password</a>";

                _email.SendEmail(email, Link, user.Name);


                return RedirectToAction("ForgetPassword");
            }

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













































































        // validations //



        private bool IsUserExist(string email)
        {
            var user = _user.GetUserByEmail(email);
            if(user == null)
            {
                return true;
            }
            else
            {
              return  false;
            }

        }



    }
}
 