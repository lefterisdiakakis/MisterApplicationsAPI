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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MSSQLConnectionProperties _mSSQLConnectionProperties;

        public DepartmentRepository(MSSQLConnectionProperties mSSQLConnectionProperties)
        {
            _mSSQLConnectionProperties = mSSQLConnectionProperties;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            var conn = new SqlConnection(_mSSQLConnectionProperties.ConnectionString);
            List<Department> departments = null;
            try
            {
                conn.Open();
                var res = await conn.QueryAsync<Department>("SELECT * FROM DepartmentsDetails", commandType: CommandType.Text);
                departments = (res != null) ? res.ToList<Department>() : null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return departments;
        }
    }
}
