using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.CoreObjects;

namespace SmartHome.MyObjects
{
    public class TableLamp : ISwitchController, IValueController
    {
        private int value;
        private bool isOn;

        public bool IsOn
        {
            get
            {
                return this.isOn;
            }
            set
            {
                if (value != this.isOn)
                {
                    this.isOn = value;
                    Console.WriteLine("************************************************************");
                    Console.WriteLine("{0}: is {1}.", this.GetType().Name, this.isOn ? "on" : "off");
                    Console.WriteLine("************************************************************");
                }
            }
        }

        public void On()
        {
            this.IsOn = true;
        }

        public void Off()
        {
            this.IsOn = false;
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
                    Console.WriteLine("************************************************************");
                    Console.WriteLine("{0}: value set to {1}.", this.GetType().Name, this.value);
                    Console.WriteLine("************************************************************");
                }
            }
        }
    }
}
