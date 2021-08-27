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
    public class IOTypeRepository : IIOTypeRepository
    {
        private readonly MSSQLConnectionProperties _mSSQLConnectionProperties;

        public IOTypeRepository(MSSQLConnectionProperties mSSQLConnectionProperties)
        {
            _mSSQLConnectionProperties = mSSQLConnectionProperties;
        }

        public async Task<List<IOType>> GetAllAsync()
        {
            var conn = new SqlConnection(_mSSQLConnectionProperties.ConnectionString);
            List<IOType> iOTypes = null;
            try
            {
                conn.Open();
                var res = await conn.QueryAsync<IOType>("SELECT * FROM IOTypesDetails", commandType: CommandType.Text);
                iOTypes = (res != null) ? res.ToList<IOType>() : null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return iOTypes;
        }
    }
}
