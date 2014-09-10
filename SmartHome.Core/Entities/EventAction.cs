﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHome.Core.Entities
{
    public class EventAction : INameEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
