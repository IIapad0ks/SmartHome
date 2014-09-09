using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Models;

namespace SmartHome.Core.SmartHome
{
    public interface ISaveEventsManager
    {
        List<EventLogModel> Events { get; }

        void AddEvent(IConfig config, string actionName);
        void SaveEvents();
    }
}
