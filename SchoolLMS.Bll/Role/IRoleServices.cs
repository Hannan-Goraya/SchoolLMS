
using SchoolLMS.Domain.DataTable;
using SchoolLMS.Domain.Models.Users;

namespace SchoolLMS.Bll.Role
{
    public interface IRoleServices
    {
        IEnumerable<AppUsersRole> GetRoleByuserId(int userId);

        IEnumerable<UserListWithRoles> GetUserLsitWithRole();

        List<AppRole> GetAllRole(int uId);

        int AddRole(int userId, int roleId);
        int RemoveRole(int userId, int roleId);
        public void DeleteUser(int id);
        void AddNewRole(string RName);

       Task<DataTableResponse<UserListWithRoles>> GetUserListAsync(DataTableRequest request);


    }
}
