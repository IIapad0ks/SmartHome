using System;
using System.Threading.Tasks;
using SmartHome.Core;
using System.Threading;

namespace SmartHome.CoreObjects
{
    public class Temperature : IValueSensor
    {
        private int value;
        private bool isGrow = true;
        private Timer timer;
        private object lockObject = new object();

        public event EventHandler<EventArgs> onChange;
        public string Name { get; set; }
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
                        Console.WriteLine("{0}: value changed from {1} to {2}", this.GetType().Name, this.value, value);
                        this.value = value;
                        this.onChange(this, new EventArgs());
                    }
                }
            }
        }

        public async void StartAsync()
        {
            await Task.Run(() =>
            {
                this.timer = new Timer(delegate(object state)
                    {
                        this.Value = SmartHomeHandler.GetNewValue(this.Value, 10, 40, 1, 5, ref this.isGrow);
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
