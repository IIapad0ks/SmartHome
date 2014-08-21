using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;

namespace SmartHome.CoreObjects
{
    public class Temperature : ISensor
    {
        private int value;
        private bool isGrow = true;

        public event EventHandler<EventArgs> onChange;
        public string Name { get; set; }
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
                    Console.WriteLine("{0}: value changed from {1} to {2}", this.GetType().Name, this.value, value);
                    this.value = value;  
                    this.onChange(this, new EventArgs());
                }
            }
        }

        public void Check(object state)
        {
            this.Value = SmartHomeHandler.GetNewValue(this.Value, 10, 40, 1, 5, ref this.isGrow);
        }
    }
}
