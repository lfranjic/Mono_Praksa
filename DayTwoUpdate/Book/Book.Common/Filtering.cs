using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Common
{
    public class Filtering
    {
        //public string Query { get; set; }
        public int? PageMin { get; set; }
        public int? PageMax { get; set; }
        public string BookTitle { get; set; }
        public Filtering(int? PageMin, int? PageMax, string BookTitle)
        {
            this.PageMax = PageMax;
            this.PageMin = PageMin;
            this.BookTitle = BookTitle;
        }
    }
}
