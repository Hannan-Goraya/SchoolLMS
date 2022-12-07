using Microsoft.AspNetCore.Mvc;

namespace SchoolMS.WEB.Controllers
{
    public class ClassesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
