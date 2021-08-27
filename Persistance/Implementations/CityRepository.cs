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
    public class CityRepository : ICityRepository
    {
        private readonly MSSQLConnectionProperties _mSSQLConnectionProperties;

        public CityRepository(MSSQLConnectionProperties mSSQLConnectionProperties)
        {
            _mSSQLConnectionProperties = mSSQLConnectionProperties;
        }

        public async Task<List<City>> GetAllAsync()
        {
            var conn = new SqlConnection(_mSSQLConnectionProperties.ConnectionString);
            List<City> cities = null;
            try
            {
                conn.Open();
                var res = await conn.QueryAsync<City>("SELECT * FROM CitiesDetails", commandType: CommandType.Text);
                cities = (res != null) ? res.ToList<City>() : null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return cities;
        }
    }
}
