using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YomiOlatunji.Core
{
    public class PageModel<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageCount { get; set; } = 10;
        public int DataCount { get; set; } = 0;
        public int PageSize { set; get; }
        public IList<T> Data { get; set; }

        public PageModel()
        {
            Data = new List<T>();
        }
    }
}
