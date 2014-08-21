using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core
{
    public interface ISensor
    {
        event EventHandler<EventArgs> onChange;
        string Name { get; set; }
        int Value { get; set; }

        void Check(object state);
    }
}
