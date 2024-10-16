using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoqodData.Models;

namespace WoqodData.Repository
{
    public interface IStoreInvoicesRepository
    {
        Task<bool> UpdateInvoice(StoreInvoices storeInvoices);

        Task<StoreInvoices> GetInvoiceById(int id);

        Task<IEnumerable<StoreInvoices>> GetInvoices(long receiptNo, int storeId, DateTime fromDate, DateTime toDate);

    }


}
