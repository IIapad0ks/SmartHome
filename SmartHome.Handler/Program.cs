using System;
using SmartHome.Core.SmartHome;
using System.ServiceProcess;
using SmartHome.Core.Service;
using SmartHome.Core.Models;
using SmartHome.Core;
using SimpleInjector;

namespace SmartHome.Handler
{
    class Program
    {
        static void Main(string[] args)
        {
            InitIOCContainer(SIManager.Container);
            IWebAPIManager webAPIManager = SIManager.Container.GetInstance<IWebAPIManager>();
            int shID = 8;

            try
            {
                do
                {
                    string command = Console.ReadLine().ToLower();
                    switch (command)
                    {
                        case "on":
                            Console.WriteLine("Starting...");
                            webAPIManager.SHCommandRequest(shID, SHCommamd.Start);
                            Console.WriteLine("SmartHome is started.");
                            break;
                        case "off":
                            Console.WriteLine("Stopping...");
                            webAPIManager.SHCommandRequest(shID, SHCommamd.Stop);
                            Console.WriteLine("SmartHome is stopped.");
                            break;
                        case "restart":
                            Console.WriteLine("Restarting...");
                            webAPIManager.SHCommandRequest(shID, SHCommamd.Restart);
                            Console.WriteLine("SmartHome is restarted.");
                            break;
                        case "ison":
                            bool isOn = webAPIManager.Get<SHServiceModel>(shID).IsOn;
                            Console.WriteLine("SH is {0}", isOn ? "on" : "off");
                            break;
                        case "exit":
                            return;
                        default:
                            Console.WriteLine("I don't understand you :'(");
                            break;
                    }
                } while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Uknown application error. Sorry :'(");
                Console.WriteLine(e.GetType().Name);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
            }
        }

        static void InitIOCContainer(Container container)
        {
            container.Register<IWebAPIManager, WebAPIManager>();

            container.Verify();
        }
    }
}
