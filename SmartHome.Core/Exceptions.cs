using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core
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

    [Serializable]
    public class SmartHomeWebAPIException : ApplicationException
    {
        public SmartHomeWebAPIException() { }
        public SmartHomeWebAPIException(string message) : base(message) { }
        public SmartHomeWebAPIException(string message, Exception inner) : base(message, inner) { }
        protected SmartHomeWebAPIException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
