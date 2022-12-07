
using SchoolLMS.Domain.Models.Users;

namespace SchoolLMS.Bll.Users
{
    public interface IUserServices
    {
        int Add(AppUsers user);


        AppUsers GetUserByEmail(string email);


        AppUsers GetUserById(int Id);

        void UpdatePassword(string email, string Password);


        void UpdateImage(string email, string Image);


        string CreatePasswordHash(string password);

        bool VirifyPassword(string password, string dbPassword);



    }
}
