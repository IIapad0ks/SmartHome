using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.SmartHome
{
    public abstract class Config : IConfig
    {
        public int ID { get; set; }

        public int TypeID { get; set; }

        public string Name { get; set; }

        public event EventHandler<SaveEventsManagerArgs> onEvent;

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            reader.Read();
            this.ID = Int32.Parse(reader["ID"]);
            this.TypeID = Int32.Parse(reader["TypeID"]);
            this.Name = reader["Name"];
        }

        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement(this.GetType().Name);
            writer.WriteAttributeString("ID", this.ID.ToString());
            writer.WriteAttributeString("TypeID", this.TypeID.ToString());
            writer.WriteAttributeString("Name", this.Name.ToString());
        }

        protected void ExecOnEvent(SaveEventsManagerArgs args)
        {
            this.onEvent(this, args);
        }
    }
}
