using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SmartHome.Core;

namespace SmartHome.CoreObjects
{
    public class SwitchController : ISwitchController
    {
        private bool isOn;

        public int ID { get; set; }

        public bool IsOn
        {
            get
            {
                return this.isOn;
            }
            set
            {
                if (value != this.isOn)
                {
                    this.isOn = value;
                    Console.WriteLine("************************************************************");
                    Console.WriteLine("{0}({2}): is {1}.", this.GetType().Name, this.isOn ? "on" : "off", this.Name);
                    Console.WriteLine("************************************************************");

                    WebAPIManager.AddEvent(this, this.isOn ? "turnOn" : "turnOff");
                }
            }
        }

        public void On()
        {
            this.IsOn = true;
        }

        public void Off()
        {
            this.IsOn = false;
        }

        public string Name { get; set; }

        public virtual string WriteXml()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, this);
            return writer.ToString();
        }

        public virtual void ReadXml(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringReader reader = new StringReader(xml);
            ISwitchController switchController = (ISwitchController)serializer.Deserialize(reader);

            this.Name = switchController.Name;
            this.isOn = switchController.IsOn;
        }
    }
}
