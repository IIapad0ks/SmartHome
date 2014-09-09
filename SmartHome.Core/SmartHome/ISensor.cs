using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SmartHome.Core.SmartHome
{
    public interface ISensor : IConfig, IDisposable
    {
        int TimerPeriod { get; set; }
        event EventHandler<EventArgs> onChange;

        void Start();
        void Stop();
    }
}
