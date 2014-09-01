using System;

namespace SmartHome.Handler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SmartHomeServiceReference.SmartHomeServiceClient client = new SmartHomeServiceReference.SmartHomeServiceClient();

                do
                {
                    string command = Console.ReadLine().ToLower();
                    switch (command)
                    {
                        case "on":
                            client.Start();
                            break;
                        case "off":
                            client.Stop();
                            break;
                        case "restart":
                            client.Restart();
                            break;
                        case "exit":
                            client.Stop();
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
