using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.CoreObjects
{
    public class Conditioner : ISwitchController, IValueController
    {
        private int value;
        public bool isOn { get; set; }

        public void On()
        {
            this.isOn = true;
        }

        public void Off()
        {
            this.isOn = false;
        }

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
                    this.value = value;
                    Console.WriteLine("{0}: value set to {1}.", this.GetType().Name, this.value);
                }
            }
        }
    }
}
