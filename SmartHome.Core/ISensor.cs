using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SmartHome.Core
{
    public interface ISensor : IConfig, IDisposable
    {
        event EventHandler<EventArgs> onChange;

        void StartAsync();
        void Stop();
    }
}
