using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SmartHome.Core;

namespace SmartHome.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SmartHomeService : ISmartHomeService
    {
        private SmartHomeHandler home = new SmartHomeHandler(@"E:\Bohdan\dotnet_workspace\SmartHome\SmartHome.Service\bin\SmartHome.xml", @"E:\Bohdan\dotnet_workspace\SmartHome\SmartHome.Service\bin\libs\");

        public void Start()
        {
            home.Start();
        }

        public void Stop()
        {
            home.Stop();
        }

        public void Restart()
        {
            home.Restart();
        }

        public bool IsOn()
        {
            return home.IsOn;
        }
    }
}
