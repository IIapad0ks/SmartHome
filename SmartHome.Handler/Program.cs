using System;
using SmartHome.Core.Service;
using SmartHome.Core.Entities;
using SmartHome.Core.Repositories;
using SmartHome.Core.SmartHome;
using SmartHome.DBModelConverter.Repositories;
using SmartHome.Data.Repositories;
using SmartHome.Core;
using System.Data.Entity;
using SimpleInjector;
using SimpleInjector.Extensions;
using SmartHome.Service;

namespace SmartHome.Handler
{
    class Program
    {
        static void Main(string[] args)
        {
            string configFilename = @"E:\Bohdan\dotnet_workspace\SmartHome\SmartHome.Service\bin\SmartHome.xml";
            string libDirectory = @"E:\Bohdan\dotnet_workspace\SmartHome\SmartHome.Service\bin\libs\";

            try
            {
                SIManager.Container.Register<ISmartHomeHandler>(() => new SmartHomeHandler(configFilename, libDirectory));
                SIManager.Container.Register<DbContext, SmartHomeDBEntities>();
                SIManager.Container.RegisterOpenGeneric(typeof(ISHRepository<>), typeof(SmartHomeRepository<>));
                SIManager.AddAssembly(typeof(EventLogRepository).Assembly);
                SIManager.Container.Register<ISaveEventsManager, SaveEventsManager>();
                SIManager.Container.Verify();

                SmartHomeServiceReference.SmartHomeServiceClient client = new SmartHomeServiceReference.SmartHomeServiceClient();

                do
                {
                    string command = Console.ReadLine().ToLower();
                    switch (command)
                    {
                        case "on":
                            Console.WriteLine("Starting...");
                            if (client.Start())
                            {
                                Console.WriteLine("SH is started.");
                            }
                            else
                            {
                                Console.WriteLine("SH start error!");
                            }
                            break;
                        case "off":
                            Console.WriteLine("Stopping...");
                            if (client.Stop())
                            {
                                Console.WriteLine("SH is stopped");
                            }
                            else
                            {
                                Console.WriteLine("SH stop error!");
                            }
                            break;
                        case "restart":
                            Console.WriteLine("Restarting...");
                            if (client.Restart())
                            {
                                Console.WriteLine("SH is restarted.");
                            }
                            else
                            {
                                Console.WriteLine("SH restart error!");
                            }
                            break;
                        case "isOn":
                            Console.WriteLine("SH is {0}", client.IsOn() ? "on" : "off");
                            break;
                        case "exit":
                            char ch;
                            Console.WriteLine("Exit...");
                            if (!client.Stop())
                            {
                                Console.WriteLine("SH stop error! Exit? y/n");
                                ch = Console.ReadLine().ToLower()[0];
                            }
                            else
                            {
                                return;
                            }

                            if (ch == 'y')
                            {
                                return;
                            }

                            break;
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
    }
}
