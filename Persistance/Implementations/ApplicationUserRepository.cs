using Dapper;
using Domain;
using Persistance.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Persistance.Implementations
{
    class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ConnectionProperties _connectionProperties;

        public ApplicationUserRepository(ConnectionProperties connectionProperties)
        {
            _connectionProperties = connectionProperties;
        }

        public async Task<ApplicationUser> GetApplicationUser(ApplicationUser applicationUser)
        {
            ApplicationUser res;

            using (SqlConnection conn = new(_connectionProperties.MisterApplicationsConnectionString))
            {
                conn.Open();
                res = await conn.QueryFirstOrDefaultAsync<ApplicationUser>(
                    ApplicationUserRepository.SQLCommand(_connectionProperties.MisterApplicationsDataBaseVersion),
                    param: applicationUser,
                    commandType: CommandType.Text,
                    commandTimeout: _connectionProperties.MisterApplicationsConnectionTimeOut
                    );
            }

            return res;
        }

        private static string SQLCommand(string version = null)
        {
            return version switch
            {
                // TODO: change query string to use function
                _ => @" SELECT ID, InsertDateTime, InsertUserID, UpdateDateTime, UpdateUserID, Username, Password, LastName, FirstName, Email, IPRestriction, LanguageID, UserTypeID, Active, Visible, Deleted, ResourceID, Description
                        FROM dbo.FN_SysApplicationUsers_Select(@ID, @Username, @Active, @Visible, @Deleted)",
            };
        }
    }
}
