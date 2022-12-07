using Microsoft.AspNetCore.Mvc;
using SchoolLMS.Bll.Student;

namespace SchoolLMS.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _student;

        public StudentController(IStudentService student)
        {
            _student = student;
        }


        public IActionResult Index()
        {
            var studentlist = _student.GetStudentWithClass();
            return View(studentlist);
        }

    }
}
