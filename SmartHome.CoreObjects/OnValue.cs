using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using System.Reflection;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Xml.Serialization;
using System.IO;

namespace SmartHome.CoreObjects
{
    public abstract class OnValue : ITrigger
    {
        public int ID { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public IController Controller { get; set; }
        public string Condition { get; set; }
        public string Name { get; set; }

        public void Invoke(object sender, EventArgs e)
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
            }
        }

        public abstract void TriggerSuccessFunction();

        public virtual string WriteXml()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, this);
            return writer.ToString();
        }

        public virtual void ReadXml(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringReader reader = new StringReader(xml);
            ITrigger trigger = (ITrigger)serializer.Deserialize(reader);

            this.ID = trigger.ID;
            this.Name = trigger.Name;
            this.Condition = trigger.Condition;
            this.Controller = trigger.Controller;
            this.Properties = trigger.Properties;
        }
    }
}
