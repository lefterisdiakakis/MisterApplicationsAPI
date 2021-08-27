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
    public class OfficeRepository:IOfficeRepository
    {
        private readonly MSSQLConnectionProperties _mSSQLConnectionProperties;

        public OfficeRepository(MSSQLConnectionProperties mSSQLConnectionProperties)
        {
            _mSSQLConnectionProperties = mSSQLConnectionProperties;
        }

        public async Task<List<Office>> GetAllAsync()
        {
            var conn = new SqlConnection(_mSSQLConnectionProperties.ConnectionString);
            List<Office> offices = null;
            try
            {
                conn.Open();
                var res = await conn.QueryAsync<Office>("SELECT * FROM OfficesDetails", commandType: CommandType.Text);
                offices = (res != null) ? res.ToList<Office>() : null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return offices;
        }
    }
}
