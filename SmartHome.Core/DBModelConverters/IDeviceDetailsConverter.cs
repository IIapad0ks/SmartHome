using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Models;
using SmartHome.Core.Service;

namespace SmartHome.Core.DBModelConverters
{
    public interface IEventListConverter<T> where T : class
    {
        List<T> GetPage(int pageNumber);
    }
}
