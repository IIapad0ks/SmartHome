using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;

namespace SmartHome.CoreObjects
{
    public class LightSwitcher : ISwitchController
    {
        private bool isOn;
        public string Name { get; set; }
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
    }
}
