using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;

namespace SmartHome.CoreObjects
{
    public interface IValueController : IController
    {
        int Value { get; set; }
    }
}
