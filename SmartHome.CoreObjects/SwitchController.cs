using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SmartHome.Core.SmartHome;

namespace SmartHome.CoreObjects
{
    public class SwitchController : Controller, ISwitchController
    {
        private bool isOn;

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

                    this.ExecOnEvent(new SaveEventsManagerArgs(this.isOn ? "turnOn" : "turnOff"));
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

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            base.ReadXml(reader);
            this.isOn = Boolean.Parse(reader["isOn"]);
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("isOn", this.isOn.ToString());
        }
    }
}
