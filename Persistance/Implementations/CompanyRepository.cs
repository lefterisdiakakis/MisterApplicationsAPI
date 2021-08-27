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
    public class CompanyRepository:ICompanyRepository
    {
        private readonly MSSQLConnectionProperties _mSSQLConnectionProperties;

        public CompanyRepository(MSSQLConnectionProperties mSSQLConnectionProperties)
        {
            _mSSQLConnectionProperties = mSSQLConnectionProperties;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            var conn = new SqlConnection(_mSSQLConnectionProperties.ConnectionString);
            List<Company> companies = null;
            try
            {
                conn.Open();
                var res = await conn.QueryAsync<Company>("SELECT * FROM CompaniesDetails", commandType: CommandType.Text);
                companies = (res != null) ? res.ToList<Company>() : null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return companies;
        }
    }
}
