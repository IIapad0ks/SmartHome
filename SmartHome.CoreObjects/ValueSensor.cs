using System;
using System.Threading.Tasks;
using SmartHome.Core.SmartHome;
using System.Threading;
using System.Xml.Serialization;
using System.IO;

namespace SmartHome.CoreObjects
{
    public abstract class ValueSensor : Sensor, IValueSensor
    {
        private int value;
        private object lockObject = new object();

        protected bool isGrow = true;
        
        public int Value
        {
            get
            {
                lock (this.lockObject)
                {
                    return this.value;
                }
            }
            set
            {
                lock (this.lockObject)
                {
                    if (value != this.value)
                    {
                        Console.WriteLine("{0}({3}): value changed from {1} to {2}", this.GetType().Name, this.value, value, this.Name);
                        this.value = value;

                        try
                        {
                            this.ExecOnChange();
                            this.ExecOnEvent(new SaveEventsManagerArgs("changeValue"));
                        }
                        catch (Exception e)
                        {
                            this.Stop();
                            Console.WriteLine("`onChange` event error!!!");
                            Console.WriteLine(e.GetType().Name);
                            Console.WriteLine(e.Message);
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                }
            }
        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            base.ReadXml(reader);
            this.value = Int32.Parse(reader["value"]);
            this.isGrow = Boolean.Parse(reader["isGrow"]);
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("value", this.value.ToString());
            writer.WriteAttributeString("isGrow", this.isGrow.ToString());
        }

        protected override void Check(object state) { }
    }
}
