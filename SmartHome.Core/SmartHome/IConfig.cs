using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SmartHome.Core.SmartHome
{
    public interface IConfig : IXmlSerializable
    {
        int ID { get; set; }
        int TypeID { get; set; }
        string Name { get; set; }

        event EventHandler<SaveEventsManagerArgs> onEvent;
    }
}
