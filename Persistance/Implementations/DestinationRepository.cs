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
    public class DestinationRepository : IDestinationRepository
    {
        private readonly MSSQLConnectionProperties _mSSQLConnectionProperties;

        public DestinationRepository(MSSQLConnectionProperties mSSQLConnectionProperties)
        {
            _mSSQLConnectionProperties = mSSQLConnectionProperties;
        }

        public async Task<List<Destination>> GetAllAsync()
        {
            var conn = new SqlConnection(_mSSQLConnectionProperties.ConnectionString);
            List<Destination> destinations = null;
            try
            {
                conn.Open();
                var res = await conn.QueryAsync<Destination>("SELECT * FROM DestinationsDetails", commandType: CommandType.Text);
                destinations = (res != null) ? res.ToList<Destination>() : null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return destinations;
        }
    }
}
