using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Common
{
    public class Paging
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Paging(int pageSize, int pageNumber)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
