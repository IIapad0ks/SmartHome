using SmartHome.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHome.WebClient.Models
{
    public class SettingsModel
    {
        public SettingsModel(SHServiceModel shService)
        {

        }

        public string XmlConfig { get; set; }
        public string ConfigFilename { get; set; }
        public List<PluginModel> Plugins { get; set; }
        public PluginModel newPlugin { get; set; }
        public bool SHIsOn { get; set; }
    }
}