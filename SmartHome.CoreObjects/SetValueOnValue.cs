using System;
using SmartHome.Core.SmartHome;

namespace SmartHome.CoreObjects
{
    public class SetValueOnValue : OnValue, IValueTrigger
    {
        public int Value { get; set; }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            base.ReadXml(reader);
            this.Value = Int32.Parse(reader["Value"]);
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("Value", this.Value.ToString());
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        protected override void TriggerSuccessFunction()
        {
            IValueController controller = (IValueController)this.Controller;
            controller.Value = this.Value;
        }
    }
}
