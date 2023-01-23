using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Model.Common
{
    public interface IBookModelCommon
    {
        Guid Id { get; set; }
        string Title { get; set; }
        int Pages { get; set; }
    }
}