using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoqodData.Data
{
    public class DataAccess :IDataAccess
    {
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration config)
        {

            _config = config;
        }

        //this method will return list of data
        public async Task<IEnumerable<T>> GetData<T,P>(string query , P parameters,
            string connectionId= "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(query, parameters);

        }
        //this method will not return anything but saving data
        public async Task SaveData<P>(string query, P parameters,
            string connectionId = "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
