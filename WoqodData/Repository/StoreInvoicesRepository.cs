using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoqodData.Data;
using WoqodData.Models;
using CommandType = WoqodData.Data.CommandType;

namespace WoqodData.Repository
{
    public class StoreInvoicesRepository : IStoreInvoicesRepository
    {
        public readonly IDataAccess _db;

        public StoreInvoicesRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task<PaginatedResponse<StoreInvoices>> GetInvoices(long? receiptNo, int storeId, DateTime fromDate, DateTime toDate, int pageSize, int pageNumber)
        {
            var parameters = new
            {
                ReceiptNo = receiptNo ?? (object)DBNull.Value,
                StoreId = storeId,
                FromDate = fromDate,
                ToDate = toDate,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var query = "GetInvoicesWithPagination";

            var (invoices, totalCount) = await _db.GetInvoicesWithTotalCount(query, parameters);

            return new PaginatedResponse<StoreInvoices>(invoices, totalCount);
        }




        public async Task<StoreInvoices> GetInvoiceById(int id)
        {
            string query = "GetStoreInvoiceById";
            IEnumerable<StoreInvoices> invoices = await _db.GetData<StoreInvoices, dynamic>(query,  new {Id=id },CommandType.StoredProcedure);

            return invoices.FirstOrDefault();
        }

        public async Task<bool> UpdateInvoice(StoreInvoices storeInvoices)
        {
            try
            {
                string query = "UpdateStoreInvoice";
                await _db.SaveData(query, storeInvoices, CommandType.StoredProcedure);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
