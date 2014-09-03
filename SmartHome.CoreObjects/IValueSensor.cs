﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.SmartHome;

namespace SmartHome.CoreObjects
{
    interface IValueSensor : ISensor
    {
        int Value { get; set; }
    }
}
