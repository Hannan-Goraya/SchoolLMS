using Dapper;
using SchoolLMS.Dal.Dapper;
using SchoolLMS.Domain.DataTable;
using SchoolLMS.Domain.Partials;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLMS.Dal.Repos
{
    public class UserRoleRopository : IUserRoleRepository
    {
        private readonly IDapperRepository _dapper;

        public UserRoleRopository(IDapperRepository dapper)
        {
            _dapper = dapper;
        }

        public async Task<List<UserRoleListingRequest>> GetUserWithRoleAsync(DatatableListingRequest request)
        {
            try
            {

                var parameters = new DynamicParameters();
                parameters.Add("SearchValue", request.SearchValue, DbType.String);
                parameters.Add("PageNo", request.PageNo, DbType.Int32);
                parameters.Add("PageSize", request.PageSize, DbType.Int32);
                parameters.Add("SortColumn", request.SortColumn, DbType.Int32);
                parameters.Add("SortDirection", request.SortDirection, DbType.String);


                return _dapper.ReturnList<UserRoleListingRequest>("GetAllDepartment", parameters).ToList();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
