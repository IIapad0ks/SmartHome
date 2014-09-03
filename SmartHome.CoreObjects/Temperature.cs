using SmartHome.Core.SmartHome;

namespace SmartHome.CoreObjects
{
    public class Temperature : ValueSensor
    {
        protected override void Check(object state)
        {
            this.Value = Utils.GenerateRandomValue(this.Value, 10, 40, 1, 5, ref this.isGrow);
        }
    }
}
