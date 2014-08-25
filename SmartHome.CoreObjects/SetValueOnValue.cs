using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using System.Reflection;

namespace SmartHome.CoreObjects
{
    public class SetValueOnValue : OnValue
    {

        public override void TriggerSuccessFunction()
        {
            IValueController controller = (IValueController)this.Controller;
            controller.Value = Int32.Parse(this.Properties["value"]);
        }
    }
}
