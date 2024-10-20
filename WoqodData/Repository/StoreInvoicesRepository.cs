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

        public async Task<IEnumerable<StoreInvoices>> GetInvoices(long receiptNo, int storeId, DateTime fromDate, DateTime toDate, int pageSize, int pageNumber)
        {
            var parameters = new
            {
                ReceiptNo = receiptNo,
                StoreId = storeId,
                FromDate = fromDate,
                ToDate = toDate,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var query = "GetInvoicesWithPagination";

            var invoices = await _db.GetData<StoreInvoices, dynamic>(query, parameters, CommandType.StoredProcedure);

            return invoices;
        }


        public async Task<StoreInvoices> GetInvoiceById(int id)
        {
            string query = "select * from dbo.StoreInvoices where id=@Id";
            IEnumerable<StoreInvoices> invoices = await _db.GetData<StoreInvoices, dynamic>(query,  new {Id=id },CommandType.Text);

            return invoices.FirstOrDefault();
        }

        public async Task<bool> UpdateInvoice(StoreInvoices storeInvoices)
        {
            try
            {
                string query = "update dbo.StoreInvoices set customerName=@CustomerName, email=@Email,passport=@Passport, qid=@Qid ,mobileNumber=@MobileNumber where id=@Id";
                await _db.SaveData(query, storeInvoices);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
