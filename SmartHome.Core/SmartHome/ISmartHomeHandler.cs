using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.SmartHome
{
    public interface ISmartHomeHandler
    {
        bool IsOn { get; }

        bool Start();
        bool Stop();
        bool Restart();
    }
}
