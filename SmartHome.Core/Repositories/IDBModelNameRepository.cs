using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Models;

namespace SmartHome.Core.Repositories
{
    public interface IDBModelNameRepository<T> : IDBModelRepository<T> where T : class, INameModel
    {
        T Get(string name);
    }
}
