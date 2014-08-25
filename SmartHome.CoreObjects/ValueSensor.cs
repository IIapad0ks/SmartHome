using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using System.Threading;

namespace SmartHome.CoreObjects
{
    public abstract class ValueSensor : ISensor
    {
        private int value;
        private Timer timer;
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
                            this.onChange(this, new EventArgs());
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

        public event EventHandler<EventArgs> onChange;

        public async void StartAsync()
        {
            await Task.Run(() =>
            {
                this.timer = new Timer(this.Check, null, 0, 1000);
            });
        }

        public string Name { get; set; }

        public void Stop()
        {
            this.timer.Dispose();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected abstract void Check(object state);

        private void Dispose(bool flag)
        {
            this.timer.Dispose();
        }
    }
}
