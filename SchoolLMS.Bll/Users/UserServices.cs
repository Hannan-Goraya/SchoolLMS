global using Dapper;
global using SchoolLMS.Dal.Dapper;
using System.Data;

using System.Security.Cryptography;
using System.Text;
using SchoolLMS.Domain.Models.Users;

namespace SchoolLMS.Bll.Users
{
    public class UserServices : IUserServices
        {
        private readonly IDapperRepository _dapper;

            public UserServices(IDapperRepository dapper)
            {
                _dapper = dapper;
            }


            public int Add(AppUsers user)
            {



                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameter.Add("@Name", user.Name);
                parameter.Add("@Email", user.Email);
                parameter.Add("@Password", user.Password);
                parameter.Add("@Image", user.Image);
                parameter.Add("@Token", user.Token);





                var result = _dapper.ReturnInt("AddUser", parameter);
                if (result > 0)
                {

                }

                return result;


            }



            public AppUsers GetUserByEmail(string email)
            {



                Dapper.DynamicParameters parameter = new DynamicParameters();


                parameter.Add("@Email", email);




                return _dapper.ReturnList<AppUsers>("GetUserByEmail", parameter).FirstOrDefault();


            }


            public AppUsers GetUserById(int Id)
            {



                DynamicParameters parameter = new DynamicParameters();


                parameter.Add("@Id", Id);




                return _dapper.ReturnList<AppUsers>("GetUserById", parameter).FirstOrDefault();


            }






            public void UpdatePassword(string email, string Password)
            {

                DynamicParameters parameter = new DynamicParameters();


                parameter.Add("@Email", email);
                parameter.Add("@Password", Password);


                _dapper.Excute("", parameter);

            }




            public void UpdateToken(string email, string Token)
            {



                DynamicParameters parameter = new DynamicParameters();


                parameter.Add("@Email", email);
                parameter.Add("@Token", Token);


                _dapper.Excute("ResetToken", parameter);

            }
















            public string CreatePasswordHash(string password)
            {


                using var hmac = new HMACSHA512();

                byte[] passwordSalt = hmac.Key;
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                string Passalt = Convert.ToBase64String(passwordSalt);
                string Pashash = Convert.ToBase64String(passwordHash);


                var newpassword = Passalt + ":" + Pashash;

                return newpassword;

            }



            public bool VirifyPassword(string password, string dbPassword)
            {

                string[] passwordarry = dbPassword.Split(':');
                byte[] orignalhash = Convert.FromBase64String(passwordarry[0]);
                using (var hmac = new HMACSHA512(orignalhash))
                {
                    var verifyHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    var orignalsalt = Convert.FromBase64String(passwordarry[1]);
                    return verifyHash.SequenceEqual(orignalsalt);
                }
            }





            public void UpdateImage(string email, string Image)
            {
                DynamicParameters parameter = new DynamicParameters();


                parameter.Add("@Email", email);
                parameter.Add("@Image", Image);


                _dapper.Excute("UpdateImage", parameter);
            }
        }




    }




