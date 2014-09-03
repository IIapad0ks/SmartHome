using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.SmartHome
{
    public interface IConfig
    {
        int ID { get; set; }
        int TypeID { get; set; }
        string Name { get; set; }
        //bool IsActive { get; set; }

        event EventHandler<SaveEventsManagerArgs> onEvent;

        string WriteXml();
        void ReadXml(string xml);
    }
}
