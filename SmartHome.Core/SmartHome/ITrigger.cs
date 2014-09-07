using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.SmartHome
{
    public interface ITrigger : IConfig
    {
        SerializableDictionary<string, string> Properties { get; set; }
        IController Controller { get; set; }
        string Condition { get; set;  }

        void Invoke(object sender, EventArgs e);
    }
}
