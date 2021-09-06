using Dapper;
using Domain;
using Persistance.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;


namespace Persistance.Implementations
{
    public class ApplicationUserRepository : IApplicationUserRepository
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

        public async Task<ApplicationUser> GetApplicationUserByUsername(string username)
        {
            ApplicationUser res;

            using (SqlConnection conn = new(_connectionProperties.MisterApplicationsConnectionString))
            {
                var param = new
                {
                    username,
                    Id = (int?)null,
                    Active = (bool?)null,
                    Visible = (bool?)null,
                    Deleted = (bool?)null,
                };
                conn.Open();
                res = await conn.QueryFirstOrDefaultAsync<ApplicationUser>(
                    ApplicationUserRepository.SQLCommand(_connectionProperties.MisterApplicationsDataBaseVersion),
                    param,
                    commandType: CommandType.Text,
                    commandTimeout: _connectionProperties.MisterApplicationsConnectionTimeOut
                    );
            }

            return res;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public bool AuthenticateViaLDAP(string username, string password)
        {
            bool isValid = false;
            // TODO: Replace "192.168.10.200" with actual AD
            using (PrincipalContext pc = new(ContextType.Domain, "192.168.10.200"))
            {
                isValid = pc.ValidateCredentials(username, password);
            }

            return isValid;
        }

        private static string SQLCommand(string version = null)
        {
            return version switch
            {
                _ => @" SELECT ID, InsertDateTime, InsertUserID, UpdateDateTime, UpdateUserID, Username, Password, LastName, FirstName, Email, IPRestriction, LanguageID, UserTypeID, Active, Visible, Deleted, ResourceID, Description
                        FROM dbo.FN_SysApplicationUsers_Select(@ID, @Username, @Active, @Visible, @Deleted)",
            };
        }

        
    }
}
