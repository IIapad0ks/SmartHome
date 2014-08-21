using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using System.IO;
using System.Threading;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace SmartHome.Core
{
    public class SmartHomeHandler
    {
        private string configFilename;
        private string libDirname;
        private string[] allowInterfaces = new string[] { "IController", "ISensor", "ITrigger" };
        private CancellationTokenSource tokenSource;

        public List<IController> Controllers { get; set; }
        public List<ISensor> Sensors { get; set; }
        public List<ITrigger> Triggers { get; set; }

        public SmartHomeHandler(string configFilename, string libDirname)
        {
            this.configFilename = configFilename;
            this.libDirname = libDirname;

            this.Controllers = new List<IController>();
            this.Sensors = new List<ISensor>();
            this.Triggers = new List<ITrigger>();
        }

        public void Start()
        {
            Console.WriteLine("Starting...");

            this.Controllers.Clear();
            this.Sensors.Clear();
            this.Triggers.Clear();

            List<Type> allowTypes = this.GetAllTypes();
            this.InitHomeObjects(allowTypes);

            this.tokenSource = new CancellationTokenSource();
            ParallelOptions parallelOptions = new ParallelOptions { CancellationToken = this.tokenSource.Token};
            try
            {
                Parallel.ForEach(this.Sensors, parallelOptions, (sensor) =>
                {
                    parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                    new Timer(new TimerCallback(sensor.Check), null, 0, 1000);
                });
                Console.WriteLine("SmartHome is started.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("SmartHome is stopped.");
            }
        }   

        public void Stop()
        {
            this.tokenSource.Cancel();
        }

        public void Restart()
        {
            this.Stop();
            this.Start();
        }

        private List<Type> GetAllTypes()
        {
            List<Type> allowTypes = new List<Type>();

            string[] libFilenames = Directory.GetFileSystemEntries(this.libDirname, "SmartHome.*.dll");
            foreach (string libFilename in libFilenames)
            {
                Assembly lib = Assembly.LoadFrom(libFilename);
                var libAllowTypes = from type in lib.GetTypes()
                                    where type.GetInterfaces().Any(i => this.allowInterfaces.Any(ai => i.Name == ai))
                                    select type;

                allowTypes.AddRange(libAllowTypes);
            }

            return allowTypes;
        }

        private void InitHomeObjects(List<Type> allowTypes)
        {
            XDocument configDoc = XDocument.Load(this.configFilename);
            foreach (XElement elem in configDoc.Element("config").Elements())
            {
                Type currentType = (from type in allowTypes where type.Name == elem.Attribute("type").Value select type).First();
                switch (elem.Name.ToString())
                {
                    case "controller":
                        IController controller = (IController)Activator.CreateInstance(currentType);
                        controller.Name = elem.Attribute("name").Value;
                        this.Controllers.Add(controller);
                        break;
                    case "sensor":
                        Type[] t = currentType.GetInterfaces();
                        ISensor sensor = (ISensor)Activator.CreateInstance(currentType);
                        sensor.Name = elem.Attribute("name").Value;
                        this.Sensors.Add(sensor);
                        break;
                    case "trigger":
                        ITrigger trigger = (ITrigger)Activator.CreateInstance(currentType);
                        trigger.Controller = this.Controllers.First(c => c.Name == elem.Attribute("controller").Value);
                        trigger.Name = elem.Attribute("name").Value;
                        trigger.Condition = elem.Attribute("condition").Value;

                        Dictionary<string, string> elemParams = new Dictionary<string, string>();
                        foreach (XElement child in elem.Elements())
                        {
                            elemParams.Add(child.Name.ToString(), child.Attribute("value").Value);
                        }
                        trigger.Properties = elemParams;

                        this.Sensors.First(s => s.Name == elem.Attribute("sensor").Value).onChange += trigger.Invoke;
                        break;
                }
            }
        }

        public static MethodInfo CreateFunction(string function)
        {
            string code = @"
                using System;
            
                namespace UserFunctions
                {                
                    public class BinaryFunction
                    {                
                        public static bool Function(int value)
                        {
                            return func_xy;
                        }
                    }
                }
            ";

            string finalCode = code.Replace("func_xy", function);

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerResults results = provider.CompileAssemblyFromSource(new CompilerParameters(), finalCode);

            Type binaryFunction = results.CompiledAssembly.GetType("UserFunctions.BinaryFunction");
            return binaryFunction.GetMethod("Function");
        }

        public static int GetNewValue(int currentValue, int minValue, int maxValue, int minStep, int maxStep, ref bool isGrow)
        {
            int step = new Random().Next(minStep, maxStep);
            if ((isGrow && currentValue + step >= maxValue) || (!isGrow && currentValue - step <= minValue))
            {
                isGrow = !isGrow;
            }
            return isGrow ? currentValue + step : currentValue - step;   
        }
    }
}
