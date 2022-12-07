using AspNetCore;
using Microsoft.AspNetCore.Mvc;

using SchoolLMS.Domain.DataTable;
using SchoolLMS.Domain.Models.Users;

namespace SchoolLMS.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleServices _role;
        private readonly IUserServices _user;

        public RoleController(IRoleServices role, IUserServices user)
        {
            _role = role;
            _user = user;
           
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUserList()
        {

            var request = new DataTableRequest();

            request.Draw = Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
            request.Start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            request.Length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            request.Search = new DataTableSearch()
            {
                Value = Request.Form["search[value]"].FirstOrDefault()
            };
            request.Order = new DataTableOrder[] {
            new DataTableOrder()
            {
                Dir = Request.Form["order[0][dir]"].FirstOrDefault(),
                Column = Convert.ToInt32(Request.Form["order[0][column]"].FirstOrDefault())
            }};

            return Json(_role.GetUserListAsync(request).Result);

        }









        public IActionResult AddRole(string RoleNme)
        {
            _role.AddNewRole(RoleNme);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveAppRoles(int id)
        {
            return View();
        }

        public IActionResult EditRole(int uId)
        {
            var user = _user.GetUserById(uId).Email;
            ViewBag.Email = user.ToString();

            TempData["uId"] = uId;
            return View(_role.GetAllRole(uId));
        }


        public IActionResult EditRole(List<AppRoleList> editRole)
        {



            int UId = (int)TempData["uId"];
            var roleChk = editRole.Where(x => x.Checked == true);

            foreach (var item in roleChk)
            {

                if (item.Checked = true)
                {
                    _role.AddRole(UId, item.RId);
                }
            };

            var roleUchk = editRole.Where(x => x.Checked == false);
            foreach (var item in roleUchk)
            {
                _role.RemoveRole(UId, item.RId);

            };
            return RedirectToAction("Index");







          

        }

    }
}
