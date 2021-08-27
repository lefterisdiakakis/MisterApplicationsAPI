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
    public class SiteRepository:ISiteRepository
    {
        private readonly MSSQLConnectionProperties _mSSQLConnectionProperties;

        public SiteRepository(MSSQLConnectionProperties mSSQLConnectionProperties)
        {
            _mSSQLConnectionProperties = mSSQLConnectionProperties;
        }

        public async Task<List<Site>> GetAllAsync()
        {
            var conn = new SqlConnection(_mSSQLConnectionProperties.ConnectionString);
            List<Site> floors = null;
            try
            {
                conn.Open();
                var res = await conn.QueryAsync<Site>("SELECT * FROM SitesDetails", commandType: CommandType.Text);
                floors = (res != null) ? res.ToList<Site>() : null;
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
