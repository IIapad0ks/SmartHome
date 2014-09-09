using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.SmartHome;
using System.Reflection;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Xml.Serialization;
using System.IO;

namespace SmartHome.CoreObjects
{
    public abstract class OnValue : Trigger, ITrigger
    {
        public override void Invoke(object sender, EventArgs e)
        {
            IValueSensor sensor = (IValueSensor)sender;

            ParameterExpression value = Expression.Parameter(typeof(int), "value");
            Dictionary<string, object> symbols = new Dictionary<string, object>();
            symbols.Add("value", value);
            Expression body = System.Linq.Dynamic.DynamicExpression.Parse(null, this.Condition, symbols);
            LambdaExpression lambda = Expression.Lambda(body, new ParameterExpression[] { value });
            bool result = (bool)lambda.Compile().DynamicInvoke(sensor.Value);

            if (result)
            {
                this.TriggerSuccessFunction();
                this.ExecOnEvent(new SaveEventsManagerArgs("valueConditionSuccess"));
            }
        }

        protected abstract void TriggerSuccessFunction();
    }
}
