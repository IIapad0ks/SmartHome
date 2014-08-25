using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartHome.Core;

namespace SmartHome.CoreObjects
{
    public class Light : IValueSensor
    {
        private int value;
        private bool isGrow = true;
        private Timer timer;

        public int Value { 
            get 
            {
                return this.value;
            } 
            set 
            {
                if (value != this.value)
                {
                    Console.WriteLine("{0}: value changed from {1} to {2}", this.GetType().Name, this.value, value);
                    this.value = value;
                    this.onChange(this, new EventArgs());
                }
            } 
        }
        public string Name { get; set; }
        public event EventHandler<EventArgs> onChange;

        public void Start()
        {
            this.timer = new Timer(delegate(object state)
            {
                this.Value = SmartHomeHandler.GetNewValue(this.Value, 0, 1000, 20, 100, ref this.isGrow);
            }, null, 0, 1000);
        }

        public void Stop()
        {
            timer.Dispose();
        }
    }
}
