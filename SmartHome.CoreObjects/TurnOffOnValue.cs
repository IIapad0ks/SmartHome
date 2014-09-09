
namespace SmartHome.CoreObjects
{
    public class TurnOffOnValue : OnValue
    {
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        protected override void TriggerSuccessFunction()
        {
            ISwitchController controller = (ISwitchController)this.Controller;
            controller.Off();
        }
    }
}
