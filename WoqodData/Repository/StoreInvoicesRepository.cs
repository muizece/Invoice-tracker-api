using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoqodData.Data;
using WoqodData.Models;

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

            var query = @"EXEC GetInvoicesWithPagination @ReceiptNo, @StoreId, @FromDate, @ToDate, @PageSize, @PageNumber";
            var invoices = await _db.GetData<StoreInvoices, dynamic>(query, parameters);

            return invoices;
        }


        public async Task<StoreInvoices> GetInvoiceById(int id)
        {
            string query = "select * from dbo.StoreInvoices where id=@Id";
            IEnumerable<StoreInvoices> invoices = await _db.GetData<StoreInvoices, dynamic>(query, new {Id=id });

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
