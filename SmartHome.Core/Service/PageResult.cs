using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Service
{
    public class PageResult<T>
    {
        public List<T> Result { get; set; }
        public int Total { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
    }
}
