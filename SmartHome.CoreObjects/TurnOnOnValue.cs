
namespace SmartHome.CoreObjects
{
    public class TurnOnOnValue : OnValue
    {

        public override void TriggerSuccessFunction()
        {
            ISwitchController controller = (ISwitchController)this.Controller;
            controller.On();
        }
    }
}
