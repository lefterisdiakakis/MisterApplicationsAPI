using Dapper;
using Domain;
using Persistance;
using Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Implementations
{
    public class SiteRepository:ISiteRepository
    {
        private readonly ConnectionProperties _connectionProperties;

        public SiteRepository(ConnectionProperties connectionProperties)
        {
            _connectionProperties = connectionProperties;
        }

        public async Task<List<Site>> GetAllAsync()
        {
            IEnumerable<Site> res;

            using (SqlConnection conn = new(_connectionProperties.MisterApplicationsConnectionString))
            {
                conn.Open();
                res = await conn.QueryAsync<Site>(
                    SiteRepository.SQLCommand(_connectionProperties.MisterApplicationsDataBaseVersion),
                    commandType: CommandType.Text,
                    commandTimeout: _connectionProperties.MisterApplicationsConnectionTimeOut
                    );
            }

            return res.ToList<Site>();
        }

        private static string SQLCommand(string version = null)
        {
            return version switch
            {
                // TODO: change query string to use function
                _ => "SELECT * FROM SitesDetails",
            };
        }
    }
}
