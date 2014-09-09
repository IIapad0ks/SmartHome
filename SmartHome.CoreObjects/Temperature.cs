using SmartHome.Core.SmartHome;

namespace SmartHome.CoreObjects
{
    public class Temperature : ValueSensor
    {
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        protected override void Check(object state)
        {
            this.Value = Utils.GenerateRandomValue(this.Value, 10, 40, 1, 5, ref this.isGrow);
        }
    }
}
