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

                    this.initEvent("changeValue");
                }
            }
        }

        public override void ReadXml(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringReader reader = new StringReader(xml);
            IValueController valueController = (IValueController)serializer.Deserialize(reader);

            this.value = valueController.Value;
            base.ReadXml(xml);
        }
    }
}
