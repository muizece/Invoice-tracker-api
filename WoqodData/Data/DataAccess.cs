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
using WoqodData.Models;

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
        public async Task<IEnumerable<T>> GetData<T,P>(string query , P parameters, CommandType commandType,
            string connectionId= "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(query, parameters);

        }

        public async Task<(List<StoreInvoices> Invoices, int TotalCount)> GetInvoicesWithTotalCount(string query, object parameters, 
            string connectionId = "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            {
                connection.Open();
                var command = new CommandDefinition(query, parameters, commandType: System.Data.CommandType.StoredProcedure);
                using (var multi = await connection.QueryMultipleAsync(command))
                {
                    var invoices = (await multi.ReadAsync<StoreInvoices>()).ToList();  
                    var totalCount = await multi.ReadSingleAsync<int>();               
                    return (invoices, totalCount);
                }
            }
        }

        //this method will not return anything but saving data
        public async Task SaveData<P>(string query, P parameters, CommandType commandType,
            string connectionId = "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
