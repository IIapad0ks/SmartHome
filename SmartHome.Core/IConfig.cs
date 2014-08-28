using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.WebAPI.Models;

namespace SmartHome.Core
{
    public interface IConfig
    {
        int ID { get; set; }
        string Name { get; set; }

        string WriteXml();
        void ReadXml(string xml);
    }
}
