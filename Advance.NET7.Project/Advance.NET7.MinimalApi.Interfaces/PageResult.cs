using System;
using System.Collections.Generic;
using System.Text;

namespace Advance.NET7.MinimalApi.Interfaces
{
    public class PageResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> DataList { get; set; }
    }
}
