using Dapper;
using Domain;
using Persistance.ConnectionProperties;
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
        private readonly MSSQLConnectionProperties _mSSQLConnectionProperties;

        public FloorRepository(MSSQLConnectionProperties mSSQLConnectionProperties)
        {
            _mSSQLConnectionProperties = mSSQLConnectionProperties;
        }

        public async Task<List<Floor>> GetAllAsync()
        {
            var conn = new SqlConnection(_mSSQLConnectionProperties.ConnectionString);
            List<Floor> floors = null;
            try
            {
                conn.Open();
                var res = await conn.QueryAsync<Floor>("SELECT * FROM FloorsDetails", commandType: CommandType.Text);
                floors = (res != null) ? res.ToList<Floor>() : null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return floors;
        }
    }
}
