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
        public string Name { get; set; }
        public bool isOn { get; set; }

        public void On()
        {
            if (!this.isOn)
            {
                Console.WriteLine("{0}: is on.", this.GetType().Name);
                this.isOn = true;
            }
        }

        public void Off()
        {
            if (this.isOn)
            {
                Console.WriteLine("{0}: is off.", this.GetType().Name);
                this.isOn = false;
            }

        }
    }
}
