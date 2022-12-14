using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLMS.Domain.DataTable
{
    public class DatatableListingRequest
    {

        public string SearchValue { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int SortColumn { get; set; }
        public string SortDirection { get; set; } = "ASC";
    }
}
