using System;
using SmartHome.Core.SmartHome;
using System.ServiceProcess;

namespace SmartHome.Handler
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceController sc = new ServiceController("SHService");

            try
            {
                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                }
                Console.WriteLine("Wait for running service...");
                sc.WaitForStatus(ServiceControllerStatus.Running);
                Console.WriteLine("SHService run.");

                do
                {
                    string command = Console.ReadLine().ToLower();
                    switch (command)
                    {
                        case "on":
                            Console.WriteLine("Starting...");
                            sc.ExecuteCommand((int)SHCommamd.Start);
                            Console.WriteLine("SmartHome is started.");
                            break;
                        case "off":
                            Console.WriteLine("Stopping...");
                            sc.ExecuteCommand((int)SHCommamd.Stop);
                            Console.WriteLine("SmartHome is stopped.");
                            break;
                        case "restart":
                            Console.WriteLine("Restarting...");
                            sc.ExecuteCommand((int)SHCommamd.Restart);
                            Console.WriteLine("SmartHome is restarted.");
                            break;
                            /*
                        case "ison":
                            bool isOn = SIManager.Container.GetInstance<ISHConfigRepository>().Get(1).IsOn;
                            Console.WriteLine("SH is {0}", isOn ? "on" : "off");
                            break;
                             * */
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
            finally
            {
                sc.Stop();
            }
        }
    }
}
