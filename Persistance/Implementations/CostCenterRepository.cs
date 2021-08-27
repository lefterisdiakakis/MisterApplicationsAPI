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
    public class CostCenterRepository:ICostCenterRepository
    {
        private readonly MSSQLConnectionProperties _mSSQLConnectionProperties;

        public CostCenterRepository(MSSQLConnectionProperties mSSQLConnectionProperties)
        {
            _mSSQLConnectionProperties = mSSQLConnectionProperties;
        }

        public async Task<List<CostCenter>> GetAllAsync()
        {
            var conn = new SqlConnection(_mSSQLConnectionProperties.ConnectionString);
            List<CostCenter> costCenters = null;
            try
            {
                conn.Open();
                var res = await conn.QueryAsync<CostCenter>("SELECT * FROM CostCentersDetails", commandType: CommandType.Text);
                costCenters = (res != null) ? res.ToList<CostCenter>() : null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return costCenters;
        }
    }
}
