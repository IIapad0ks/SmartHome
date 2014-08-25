using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using System.Reflection;

namespace SmartHome.CoreObjects
{
    public class TurnOffOnValue : OnValue
    {

        public override void TriggerSuccessFunction()
        {
            ISwitchController controller = (ISwitchController)this.Controller;
            controller.Off();
        }
    }
}
