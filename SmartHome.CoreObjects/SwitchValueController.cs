using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using System.Xml.Serialization;
using System.IO;
using SmartHome.Core.SmartHome;

namespace SmartHome.CoreObjects
{
    public class SwitchValueController : SwitchController, IValueController
    {
        private int value;

        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (value != this.value)
                {
                    this.value = value;
                    Console.WriteLine("************************************************************");
                    Console.WriteLine("{0}({2}): value set to {1}.", this.GetType().Name, this.value, this.Name);
                    Console.WriteLine("************************************************************");

                    this.ExecOnEvent(new SaveEventsManagerArgs("setValue"));
                }
            }
        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            base.ReadXml(reader);
            this.value = Int32.Parse(reader["value"]);
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("value", this.value.ToString());
        }
    }
}
