using Dapper;
using Domain;
using Persistance.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Persistance.Implementations
{
    public class CityRepository : ICityRepository
    {
        private readonly ConnectionProperties _connectionProperties;

        public CityRepository(ConnectionProperties connectionProperties)
        {
            _connectionProperties = connectionProperties;
        }

        public async Task<List<City>> GetAllAsync()
        {
            IEnumerable<City> res;

            using (SqlConnection conn = new(_connectionProperties.MisterApplicationsConnectionString))
            {
                conn.Open();
                res = await conn.QueryAsync<City>(
                    CityRepository.SQLCommand(_connectionProperties.MisterApplicationsDataBaseVersion),
                    commandType: CommandType.Text,
                    commandTimeout: _connectionProperties.MisterApplicationsConnectionTimeOut
                    );
            }

            return res.ToList<City>();
        }

        private static string SQLCommand(string version = null)
        {
            return version switch
            {
                // TODO: change query string to use function
                _ => "SELECT * FROM CitiesDetails",
            };
        }
    }
}
