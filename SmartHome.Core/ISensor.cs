﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SmartHome.Core
{
    public interface ISensor : IDisposable
    {
        event EventHandler<EventArgs> onChange;
        string Name { get; set; }

        void StartAsync();
        void Stop();
    }
}
