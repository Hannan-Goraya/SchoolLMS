global using SchoolLMS.Domain.Models;
global using SchoolLMS.Domain.Partials;
using SchoolLMS.Dal.Repos;
using SchoolLMS.Domain.DataTable;
using SchoolLMS.Domain.Models.Users;


namespace SchoolLMS.Bll.Role
{
    public class RoleServices : IRoleServices
    {
        private readonly IUserRoleRepository _repo;
        private readonly IDapperRepository _dapper;

        public RoleServices(IDapperRepository dapper, IUserRoleRepository repo)
        {
            _repo = repo;
            _dapper = dapper;
        }












        public async Task<DataTableResponse<UserListWithRoles>> GetDepartmentAsync(DataTableRequest request)
        {
            var req = new DatatableListingRequest()
            {
                PageNo = request.Start,
                PageSize = request.Length,
                SortColumn = request.Order[0].Column,
                SortDirection = request.Order[0].Dir,
                SearchValue = request.Search != null ? request.Search.Value.Trim() : ""
            };

            var userRoleList = await _repo.GetUserWithRoleAsync(req);

            return new DataTableResponse<UserListWithRoles>()
            {
                Draw = request.Draw,
                RecordsTotal = userRoleList[0].TotalCount,
                RecordsFiltered = userRoleList[0].FilteredCount,

                Error = ""
            };

        }




        public void DeleteUser(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id);
            _dapper.Excute("DeleteUser", parameters);

        }




        public int AddNewRole(string RName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RName" , RName);
            return _dapper.ReturnInt("RName" , parameters);
        }
        public void RemoveAppRole(int RId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RId", RId);
            _dapper.Excute("", parameters);
        }






        public IEnumerable<AppUsersRole> GetRoleByuserId(int userId)
        {

            DynamicParameters parameter = new DynamicParameters();


            parameter.Add("@UserId", userId);



            return _dapper.ReturnList<AppUsersRole>("GetUserRole", parameter);
        }







        public IEnumerable<UserListWithRoles> GetUserLsitWithRole()
        {

            DynamicParameters parameters = new DynamicParameters();

            return _dapper.ReturnList<UserListWithRoles>("GetAllRoles", parameters).ToList();
        }


        public List<AppRole> GetAllRole(int uId)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", uId);

            return _dapper.ReturnList<AppRole>("GetAllRoles", parameters).ToList();
        }


        public int AddRole(int userId, int roleId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@RoleId", roleId);

            return _dapper.ReturnInt("AddUserRole", parameters);

        }


        public int RemoveRole(int userId, int roleId)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@uid", userId);
            parameters.Add("@rid", roleId);

            var res = _dapper.ExecuteReturnScalar<UserRole>("RemoveRole", parameters);



            return res.UserId;


        }

        void IRoleServices.AddNewRole(string RName)
        {
            throw new NotImplementedException();
        }

        public Task<DataTableResponse<UserListWithRoles>> GetUserListAsync(DataTableRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
