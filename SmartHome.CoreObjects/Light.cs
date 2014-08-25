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
        private object lockObject = new object();

        public int Value { 
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
                        Console.WriteLine("{0}: value changed from {1} to {2}", this.GetType().Name, this.value, value);
                        this.value = value;
                        this.onChange(this, new EventArgs());
                    }
                }
            } 
        }
        public string Name { get; set; }
        public event EventHandler<EventArgs> onChange;

        public async void StartAsync()
        {
            await Task.Run(() =>
            {
                this.timer = new Timer(delegate(object state)
                    {
                        this.Value = SmartHomeHandler.GetNewValue(this.Value, 0, 1000, 20, 100, ref this.isGrow);
                    }, null, 0, 1000);
            });
        }

        public void Stop()
        {
            this.timer.Dispose();
        }

        public void Dispose()
        {
            this.timer.Dispose();
        }
    }
}
