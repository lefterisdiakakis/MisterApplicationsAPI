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

            using (SqlConnection conn = new SqlConnection(_connectionProperties.MisterRecordingConnectionString))
            {
                conn.Open();
                res = await conn.QueryAsync<Site>(SiteRepository.SQLCommand(
                    _connectionProperties.MisterRecordingDataBaseVersion),
                    commandType: CommandType.Text,
                    commandTimeout: _connectionProperties.MisterRecordingConnectionTimeOut
                    );
            }

            return res.ToList<Site>();
        }

        private static string SQLCommand(string version = null)
        {
            switch (version)
            {
                // TODO: change query string to use function
                default: return "SELECT * FROM SitesDetails";
            }
        }
    }
}
