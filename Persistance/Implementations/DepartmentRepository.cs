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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ConnectionProperties _connectionProperties;

        public DepartmentRepository(ConnectionProperties connectionProperties)
        {
            _connectionProperties = connectionProperties;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            IEnumerable<Department> res;

            using (SqlConnection conn = new SqlConnection(_connectionProperties.MisterRecordingConnectionString))
            {
                conn.Open();
                res = await conn.QueryAsync<Department>(DepartmentRepository.SQLCommand(
                    _connectionProperties.MisterRecordingDataBaseVersion),
                    commandType: CommandType.Text,
                    commandTimeout: _connectionProperties.MisterRecordingConnectionTimeOut
                    );
            }

            return res.ToList<Department>();
        }

        private static string SQLCommand(string version = null)
        {
            switch (version)
            {
                // TODO: change query string to use function
                default: return "SELECT * FROM DepartmentsDetails";
            }
        }
    }
}
