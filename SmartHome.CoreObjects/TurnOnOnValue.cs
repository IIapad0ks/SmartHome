using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using System.Reflection;

namespace SmartHome.CoreObjects
{
    public class TurnOnOnValue : ITrigger
    {
        private MethodInfo checkMethod;
        private string condition;

        public Dictionary<string, string> Properties { get; set; }
        public IController Controller { get; set; }
        public string Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value;
                checkMethod = SmartHomeHandler.CreateFunction(this.condition);
            }    
        }
        public string Name { get; set; }

        public void Invoke(object sender, EventArgs e)
        {
            ISensor sensor = (ISensor)sender;
            if ((bool)this.checkMethod.Invoke(null, new object[] { sensor.Value }))
            {
                ISwitchController switchController = (ISwitchController)this.Controller;
                switchController.On();
            }
        }
    }
}
