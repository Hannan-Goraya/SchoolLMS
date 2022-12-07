
using SchoolLMS.Domain.Models.Users;
using System.Security.Principal;

namespace SchoolMS.WEB.Models.Users
{
    public class RoleEdit
    {

        public string Email { get; set; }

        public AppRoleList rolesList { get; set; }



        public List<AppRoleList> rolesLists { get; set;}
    }
}
