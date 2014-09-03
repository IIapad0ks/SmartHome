﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.SmartHome;

namespace SmartHome.CoreObjects
{
    public interface ISwitchController : IController
    {
        bool IsOn { get; set; }

        void On();
        void Off();
    }
}
