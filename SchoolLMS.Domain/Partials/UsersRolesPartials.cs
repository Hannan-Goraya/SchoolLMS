using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLMS.Domain.Partials
{
    public partial class  AppUsersRole
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public bool Verify { get; set; }

        public string Image { get; set; }

        public DateTime CreatedDate { get; set; }
    }
    public partial class AppUsersRole
    {
        public int urId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

    }
    public partial class AppUsersRole
    {
        public int RId { get; set; }

        public string RName { get; set; }
    }
}

