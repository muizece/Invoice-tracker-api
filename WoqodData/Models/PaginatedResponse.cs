using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoqodData.Models
{
    public class PaginatedResponse<T>
    {
        public List<T> Data { get; set; }         // Paginated data (e.g., invoices)
        public int TotalCount { get; set; }       // Total number of records

        public PaginatedResponse(List<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }
    }

}
