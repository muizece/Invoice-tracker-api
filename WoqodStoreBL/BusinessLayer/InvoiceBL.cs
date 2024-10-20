using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoqodStoreBL.BusinessLayer
{
    public class InvoiceBL
    {
        public string ValidateRequestParameters(long receiptNo, int storeId, DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
            {
                return "Invalid date range. 'From Date' cannot be greater than 'To Date'.";
            }
            if (storeId <= 0)
            {
                return "Please provide a valid storeId.";
            }
            if (receiptNo <= 0)
            {
                return "Please provide a valid receipt number.";
            }

            return null;
        }


    }
}
