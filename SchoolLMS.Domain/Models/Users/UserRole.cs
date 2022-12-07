using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLMS.Domain.Models.Users
{
    public class UserRole
    {
        public int urId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

    }
}
