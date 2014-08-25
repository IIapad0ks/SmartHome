using System;
using System.Threading.Tasks;
using SmartHome.Core;
using System.Threading;

namespace SmartHome.CoreObjects
{
    public class Temperature : ValueSensor
    {
        protected override void Check(object state)
        {
            this.Value = SmartHomeHandler.GetNewValue(this.Value, 10, 40, 1, 5, ref this.isGrow);
        }
    }
}
