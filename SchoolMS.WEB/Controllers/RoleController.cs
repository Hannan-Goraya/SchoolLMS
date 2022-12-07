using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using SchoolLMS.Bll.Role;
using SchoolLMS.Bll.Users;
using SchoolLMS.Domain.Models.Users;

namespace SchoolMS.WEB.Controllers
{
    public class RoleController : Controller
    {
        private readonly IUserServices _user;
        private readonly IRoleServices _role;

        public RoleController(IRoleServices role, IUserServices user)
        {
            _user = user;
            _role = role;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllUserWithRole()
        {

            int totalRecord = 0;
            int filterRecord = 0;
            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
            var data = _role.GetUserLsitWithRole().AsQueryable();

            totalRecord = data.Count();

            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Name.ToLower().Contains(searchValue.ToLower()) ||
                x.Name.ToLower().Contains(searchValue.ToLower()) ||
                x.Email.ToLower().Contains(searchValue.ToLower()) ||
                x.RoleList.ToString().ToLower().Contains(searchValue.ToLower()));
            }

            filterRecord = data.Count();



            var userRoleList = data.Skip(skip).Take(pageSize).ToList();
            var returnObj = new
            {
                draw = draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = userRoleList
            };

            return Json(returnObj);




        }



      public IActionResult DeleteUSer(int id)
        {
            _role.DeleteUser(id);
            return RedirectToAction("Index");
        }





        public IActionResult EditRole(int uId)
        {


            return ViewBag(_role.GetAllRole(uId));







        }

        [HttpPost]
        public IActionResult EditRole(List<AppRoleList> editRole)
        {

            var RoleCHk = editRole.Where(x => x.Checked == true);



            foreach (var item in RoleCHk)
            {

            }




            return View();



        }
    }
}
