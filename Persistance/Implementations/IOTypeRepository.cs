﻿using Dapper;
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
    public class IOTypeRepository : IIOTypeRepository
    {
        private readonly ConnectionProperties _connectionProperties;

        public IOTypeRepository(ConnectionProperties connectionProperties)
        {
            _connectionProperties = connectionProperties;
        }

        public async Task<List<IOType>> GetAllAsync()
        {
            IEnumerable<IOType> res;

            using (SqlConnection conn = new(_connectionProperties.MisterApplicationsConnectionString))
            {
                conn.Open();
                res = await conn.QueryAsync<IOType>(
                    IOTypeRepository.SQLCommand(_connectionProperties.MisterApplicationsDataBaseVersion),
                    commandType: CommandType.Text,
                    commandTimeout: _connectionProperties.MisterApplicationsConnectionTimeOut
                    );
            }

            return res.ToList<IOType>();
        }

        private static string SQLCommand(string version = null)
        {
            return version switch
            {
                // TODO: change query string to use function
                _ => "SELECT * FROM IOTypesDetails",
            };
        }
    }
}
