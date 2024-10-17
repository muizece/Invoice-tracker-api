using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoqodData.Models
{
    public  class StoreInvoices
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public long ReceiptNo { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public long MobileNumber { get; set; }
        public string Passport { get; set; }
        public string Email { get; set; }
        public string CustomerName { get; set; }
        public long QID { get; set; }


    }
}
