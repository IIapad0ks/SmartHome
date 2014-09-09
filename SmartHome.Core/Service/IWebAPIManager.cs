using SmartHome.Core.Models;
using SmartHome.Core.SmartHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Service
{
    public interface IWebAPIManager
    {
        Uri Uri { get; set; }

        List<T> Get<T>() where T : class, IModel;
        T Get<T>(int id) where T : class, IModel;
        T Save<T>(T item) where T : class, IModel;
        bool Update<T>(T item) where T : class, IModel;
        bool Delete<T>(int id) where T : class, IModel;

        void SaveEvents(IList<EventLogModel> eventList);
        void SHCommandRequest(int id, SHCommamd command);
    } 
}
