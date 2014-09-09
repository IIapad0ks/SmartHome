using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHome.Core.SmartHome
{
    public abstract class Sensor : Config, ISensor
    {
        protected Timer timer;
        public int TimerPeriod { get; set; }

        public event EventHandler<EventArgs> onChange;

        public void Start()
        {
            this.timer = new Timer(this.Check, null, 0, this.TimerPeriod);
        }

        public void Stop()
        {
            this.timer.Dispose();
        }

        public virtual void ExecOnChange()
        {
            this.onChange(this, new EventArgs());
        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            base.ReadXml(reader);
            this.TimerPeriod = Int32.Parse(reader["TimePeriod"]);
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
 	        base.WriteXml(writer);
            writer.WriteAttributeString("TimePeriod", this.TimerPeriod.ToString());
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected void Dispose(bool flag)
        {
            this.timer.Dispose();
        }

        protected abstract void Check(object state);
    }
}
