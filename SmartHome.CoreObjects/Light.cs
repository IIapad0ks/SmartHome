﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartHome.Core;

namespace SmartHome.CoreObjects
{
    public class Light : ValueSensor
    {
        protected override void Check(object state)
        {
            this.Value = SmartHomeHandler.GetNewValue(this.Value, 0, 1000, 20, 100, ref this.isGrow);
        }
    }
}
