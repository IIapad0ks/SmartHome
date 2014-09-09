using SmartHome.CoreObjects;

namespace SmartHome.MyObjects
{
    public class TableLamp : SwitchValueController
    {
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }
}
