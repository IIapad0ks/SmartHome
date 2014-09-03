using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Exceptions
{
    [Serializable]
    public class SmartHomeConfigException : ApplicationException
    {
        public SmartHomeConfigException() { }
        public SmartHomeConfigException(string message) : base(message) { }
        public SmartHomeConfigException(string message, Exception inner) : base(message, inner) { }
        protected SmartHomeConfigException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
