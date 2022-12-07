using SchoolLMS.Domain.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLMS.Bll.Student
{
    public interface IStudentService
    {
        int Add(Students student);
        List<Classes> GetAllClasses();
        int AddStudent(Students student);
        List<StudentList> GetStudentWithClass();
    }
}
