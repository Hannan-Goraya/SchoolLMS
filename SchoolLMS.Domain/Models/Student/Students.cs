using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLMS.Domain.Models.Student
{
    public class Students
    {
        public int Id { get; set; }

        public string RollNo { get; set;}

        public string StudentName { get; set; }

        public string FatherName { get; set; }


        public DateTime DOB { get; set; }

        public string Gender { get; set; }

        public int  ClassId { get; set; }

        public string Image { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }







        [NotMapped]
        public IFormFile UploadImg { get; set; }







    }
}
