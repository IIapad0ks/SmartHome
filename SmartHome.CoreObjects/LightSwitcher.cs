
namespace SmartHome.CoreObjects
{
    public class LightSwitcher : SwitchController
    {
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }
}
