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
    public class FloorRepository : IFloorRepository
    {
        private readonly ConnectionProperties _connectionProperties;

        public FloorRepository(ConnectionProperties connectionProperties)
        {
            _connectionProperties = connectionProperties;
        }

        public async Task<List<Floor>> GetAllAsync()
        {
            IEnumerable<Floor> res;

            using (SqlConnection conn = new SqlConnection(_connectionProperties.MisterRecordingConnectionString))
            {
                conn.Open();                
                res = await conn.QueryAsync<Floor>(
                    FloorRepository.SQLCommand(_connectionProperties.MisterRecordingDataBaseVersion),
                    commandType: CommandType.Text,
                    commandTimeout: _connectionProperties.MisterRecordingConnectionTimeOut
                    );
            }

            return res.ToList<Floor>();
        }

        private static string SQLCommand(string version = null)
        {
            switch (version)
            {
                // TODO: change query string to use function
                default: return "SELECT * FROM FloorsDetails";
            }
        }
    }
}
