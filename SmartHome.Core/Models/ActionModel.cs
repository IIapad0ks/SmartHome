﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public class ActionModel : INameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanSetValue { get; set; }
    }
}
