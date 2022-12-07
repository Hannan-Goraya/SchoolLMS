global using SchoolLMS.Domain.Models.Student;
using SchoolLMS.Domain.Models.Classes;
using System.Data;
using System.Reflection.Metadata;

namespace SchoolLMS.Bll.Student
{
    public class StudentServices : IStudentService
    {
        private readonly IDapperRepository _dapper;

        public StudentServices(IDapperRepository dapper)
        {
            _dapper = dapper;
        }



        public int AddStudent(Students student)
        {
            DynamicParameters parameters = new DynamicParameters();

            var res = _dapper.ReturnInt("", parameters);

            return res;
        }



        public List<StudentList> GetStudentWithClass()
        {
            DynamicParameters parameters = new DynamicParameters();

            var list =   _dapper.ReturnList<StudentList>("GetStudentListWithClass",parameters);

            return list.ToList();

        }





        public List<Classes> GetAllClasses()
        {
            DynamicParameters parameters = new DynamicParameters();

            var list = _dapper.ReturnList<Classes>("GetAllClasses", parameters);

            return list.ToList();

        }



        public int Add(Students student)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@RollNo", student.RollNo );
            parameters.Add("@Name", student.StudentName );
            parameters.Add("@FatherName", student.FatherName );
            parameters.Add("@Email", student.Email );
            parameters.Add("@ClassId", student.ClassId );
            parameters.Add("@DOB", student.DOB );
            parameters.Add("@Gender", student.Gender );
            parameters.Add("@Image", student.Image );

            var res = _dapper.ReturnInt("AddStudent", parameters);
            return res;

        }













    }
}
