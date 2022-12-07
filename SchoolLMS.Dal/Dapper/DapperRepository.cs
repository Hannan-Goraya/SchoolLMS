using Dapper;
using Microsoft.Extensions.Configuration;

using System.Data;
using System.Data.SqlClient;


namespace SchoolLMS.Dal.Dapper
{
    public class DapperRepository : IDapperRepository
    {
        private readonly string constring;

        public DapperRepository(IConfiguration configuration)
        {

            constring = configuration.GetConnectionString("default");
        }




        public IEnumerable<T> ReturnList<T>(string procrdureName, DynamicParameters parameter)
        {
            using (SqlConnection sqlCon = new SqlConnection(constring))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procrdureName, parameter, commandType: CommandType.StoredProcedure);
            }

        }




        public int ReturnInt(string procrdureName, DynamicParameters parameter)
        {
            using (SqlConnection sqlCon = new SqlConnection(constring))
            {
                sqlCon.Open();
                sqlCon.Execute(procrdureName, parameter, commandType: CommandType.StoredProcedure);
                return parameter.Get<int>("Id");

            }
        }



        public void Excute(string procrdureName, DynamicParameters parameter)
        {
            using (SqlConnection sqlCon = new SqlConnection(constring))
            {
                sqlCon.Open();
                sqlCon.Execute(procrdureName, parameter, commandType: CommandType.StoredProcedure);


            }
        }







        public T ExecuteReturnScalar<T>(string procrdureName, DynamicParameters parameter)
        {
            using (SqlConnection sqlCon = new SqlConnection(constring))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.Execute(procrdureName, parameter, commandType: CommandType.StoredProcedure), typeof(T));
           
            
            }

        }
    }
}
