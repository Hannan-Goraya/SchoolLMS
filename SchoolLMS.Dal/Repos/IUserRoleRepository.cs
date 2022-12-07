using SchoolLMS.Domain.DataTable;
using SchoolLMS.Domain.Partials;

namespace SchoolLMS.Dal.Repos
{
    public interface IUserRoleRepository
    {
         Task<List<UserRoleListingRequest>> GetUserWithRoleAsync(DatatableListingRequest request);
    }
}