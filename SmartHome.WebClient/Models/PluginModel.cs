using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHome.WebClient.Models
{
    public class PluginModel
    {
        public PluginModel(string pluginsDirname)
        {

        }

        public string Name { get; set; }
        public string Filename { get; set; }
        public List<string> Devices { get; set; }
    }
}
