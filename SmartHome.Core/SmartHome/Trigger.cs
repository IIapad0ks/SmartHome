using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.SmartHome
{
    public abstract class Trigger : Config, ITrigger
    {
        public IController Controller { get; set; }

        public string Condition { get; set; }

        public abstract void Invoke(object sender, EventArgs e);

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            base.ReadXml(reader);
            this.Condition = reader["Condition"];
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("Condition", this.Condition);
        }
    }
}
