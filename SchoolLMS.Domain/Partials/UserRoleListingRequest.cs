using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLMS.Domain.Partials
{
    public partial class UserRoleListingRequest
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string RoleList { get; set; }
    }
    public partial class UserRoleListingRequest
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
}
