using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Exceptions
{
    [Serializable]
    public class SmartHomeException : ApplicationException
    {
        public SmartHomeException() { }
        public SmartHomeException(string message) : base(message) { }
        public SmartHomeException(string message, Exception inner) : base(message, inner) { }
        protected SmartHomeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
