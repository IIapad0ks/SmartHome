using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Exceptions
{
    [Serializable]
    public class SmartHomePluginException : ApplicationException
    {
        public SmartHomePluginException() { }
        public SmartHomePluginException(string message) : base(message) { }
        public SmartHomePluginException(string message, Exception inner) : base(message, inner) { }
        protected SmartHomePluginException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
