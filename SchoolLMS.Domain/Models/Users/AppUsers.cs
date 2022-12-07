using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLMS.Domain.Models.Users
{
    public class AppUsers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        string userNAme { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public bool Verify { get; set; }

        public string Image { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
