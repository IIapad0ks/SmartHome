﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartHome.Core.SmartHome;

namespace SmartHome.CoreObjects
{
    public class Light : ValueSensor
    {
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        protected override void Check(object state)
        {
            this.Value = Utils.GenerateRandomValue(this.Value, 0, 1000, 20, 100, ref this.isGrow);
        }
    }
}
