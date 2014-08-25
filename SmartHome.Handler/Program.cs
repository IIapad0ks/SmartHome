using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using System.Xml.Linq;

namespace SmartHome.Handler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SmartHomeHandler home = new SmartHomeHandler("SmartHome.xml", @"libs\");

                do
                {
                    string command = Console.ReadLine().ToLower();
                    switch (command)
                    {
                        case "on":
                            home.Start();
                            break;
                        case "off":
                            home.Stop();
                            break;
                        case "restart":
                            home.Restart();
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
    }
}
