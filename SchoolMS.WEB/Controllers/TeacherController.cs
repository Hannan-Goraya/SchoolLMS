using Microsoft.AspNetCore.Mvc;

namespace SchoolMS.WEB.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult TeacherDetail() => View();

        public IActionResult AddTeacher() => View();


        public IActionResult EditTeacher() => View();


        public IActionResult DeleteTeacher() => View(); 
    }
}
