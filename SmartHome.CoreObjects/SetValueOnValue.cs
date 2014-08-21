using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using System.Reflection;

namespace SmartHome.CoreObjects
{
    public class SetValueOnValue : ITrigger
    {
        private string condition;
        private MethodInfo checkMethod;

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
                this.checkMethod = SmartHomeHandler.CreateFunction(this.condition);
            }    
        }
        public string Name { get; set; }

        public void Invoke(object sender, EventArgs e)
        {
            ISensor sensor = (ISensor)sender;
            if ((bool)this.checkMethod.Invoke(null, new object[] { sensor.Value }))
            {
                IValueController valueController = (IValueController)this.Controller;
                valueController.Value = Int32.Parse(this.Properties["value"]);
            }
        }
    }
}
