using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.CoreObjects
{
    public class Conditioner : SwitchValueController
    {
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }
}
