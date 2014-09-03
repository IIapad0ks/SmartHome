
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
