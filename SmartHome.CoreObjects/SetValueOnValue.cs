using System;

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
