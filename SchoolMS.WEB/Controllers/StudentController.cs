using Microsoft.AspNetCore.Mvc;
using SchoolLMS.Bll.Student;
using SchoolLMS.Domain.Models.Student;

using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMS.WEB.Controllers
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

            

            var studentlist = _student.GetStudentWithClass().ToList();
            return View(studentlist);




        }










        public IActionResult AddStudent() => View();


        [HttpPost]
        public IActionResult AddStudent(Students students)
        {
            if (ModelState.IsValid)
            {
                var student =  _student.Add(students);
            }    
            return RedirectToAction();
            
        }



        public IActionResult Detail(int id)
        {
            var student = _student.GetStudentbyId(id);
            return View(student);
        }











    }
}
