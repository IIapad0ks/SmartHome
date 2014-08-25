using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using System.IO;
using System.Threading;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Xml;

namespace SmartHome.Core
{
    public class SmartHomeHandler
    {
        private string configFilename;
        private string libDirname;
        private string[] allowInterfaces = new string[] { "IController", "ISensor", "ITrigger" };
        private bool isOn = false;

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
            try
            {
                if (this.isOn)
                {
                    throw new SmartHomeStartException();
                }

                this.isOn = true;
                Console.WriteLine("Starting...");

                this.Controllers.Clear();
                this.Sensors.Clear();
                this.Triggers.Clear();

                List<Type> allowTypes = this.GetAllTypes();
                if (allowTypes.Count() == 0)
                {
                    throw new SmartHomeNotAllowTypesException();
                }

                this.InitHomeObjects(allowTypes);

                foreach (ISensor sensor in this.Sensors)
                {
                    sensor.Start();
                }
                Console.WriteLine("SmartHome is started.");
            }
            catch (SmartHomeStartException)
            {
                Console.WriteLine("SmartHome can't be started twice!");
            }
            catch (SmartHomeNotAllowTypesException)
            {
                Console.WriteLine("SmartHome is not found any appliance.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory {0} is not found. I can't load you appliances :'(", this.libDirname);
            }
            catch (SmartHomeNotPluginsException)
            {
                Console.WriteLine("I can't found any plugins with you appliance :'(");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("I can't found your configs ;'(");
            }
            catch (XmlException)
            {
                Console.WriteLine("Your configs are bad!");
            }
            catch (SmartHomeNotConfigException)
            {
                Console.WriteLine("Your config file don't have any configs!");
            }
        }   

        public void Stop()
        {
            try
            {
                if (!this.isOn)
                {
                    throw new SmartHomeStopException();
                }

                foreach (ISensor sensor in this.Sensors)
                {
                    sensor.Stop();
                }

                this.isOn = false;
                Console.WriteLine("SmartHome is stopped.");
            }
            catch (SmartHomeStopException)
            {
                Console.WriteLine("SmartHome can't be stopped. It's not working now :).");
            }
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
            if (libFilenames.Count() == 0)
            {
                throw new SmartHomeNotPluginsException();
            }

            foreach (string libFilename in libFilenames)
            {
                try
                {
                    Assembly lib = Assembly.LoadFrom(libFilename);
                    var libAllowTypes = from type in lib.GetTypes()
                                        where type.GetInterfaces().Any(i => this.allowInterfaces.Any(ai => i.Name == ai))
                                        select type;

                    allowTypes.AddRange(libAllowTypes);
                }
                catch (FileLoadException)
                {
                    Console.WriteLine("I can't load plugin {0} :'(", libFilename);
                }
                catch (ReflectionTypeLoadException)
                {
                    Console.WriteLine("Your plugin {0} is bad!", libFilename);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Your plugin {0} don't have any appliance!", libFilename);
                }
            }

            return allowTypes;
        }

        private void InitHomeObjects(List<Type> allowTypes)
        {
            XDocument configDoc = XDocument.Load(this.configFilename);
            List<XElement> configs = configDoc.Element("config").Elements().ToList();
            if (configs.Count() == 0)
            {
                throw new SmartHomeNotConfigException();
            }

            foreach (XElement elem in configs)
            {
                try
                {
                    if (elem.Attribute("type") == null)
                    {
                        throw new SmartHomeNotConfigTypeException();
                    }

                    var currentTypeSelectResult = from type in allowTypes where type.Name == elem.Attribute("type").Value select type;
                    if (currentTypeSelectResult == null)
                    {
                        throw new SmartHomeNotTypeException();
                    }

                    Type currentType = currentTypeSelectResult.First();

                    if (elem.Attribute("name") == null)
                    {
                        throw new SmartHomeNotConfigNameException();
                    }

                    switch (elem.Name.ToString())
                    {
                        case "controller":
                            IController controller = (IController)Activator.CreateInstance(currentType);
                            controller.Name = elem.Attribute("name").Value;
                            this.Controllers.Add(controller);
                            break;
                        case "sensor":
                            ISensor sensor = (ISensor)Activator.CreateInstance(currentType);
                            sensor.Name = elem.Attribute("name").Value;
                            this.Sensors.Add(sensor);
                            break;
                        case "trigger":
                            ITrigger trigger = (ITrigger)Activator.CreateInstance(currentType);

                            if (elem.Attribute("controller") == null)
                            {
                                throw new SmartHomeConfigException();
                            }

                            var searchControllerResult = from c in this.Controllers 
                                                         where c.Name == elem.Attribute("controller").Value 
                                                         select c;

                            if (searchControllerResult == null)
                            {
                                throw new SmartHomeConfigException();
                            }

                            trigger.Controller = searchControllerResult.First();
                            trigger.Name = elem.Attribute("name").Value;

                            if (elem.Attribute("condition") == null)
                            {
                                throw new SmartHomeConfigException();
                            }

                            trigger.Condition = elem.Attribute("condition").Value;

                            Dictionary<string, string> elemParams = new Dictionary<string, string>();
                            foreach (XElement child in elem.Elements())
                            {
                                if (child.Attribute("value") == null)
                                {
                                    throw new SmartHomeConfigException();
                                }

                                elemParams.Add(child.Name.ToString(), child.Attribute("value").Value);
                            }
                            trigger.Properties = elemParams;

                            if (elem.Attribute("sensor") == null)
                            {
                                throw new SmartHomeConfigException();
                            }

                            var searchSensorResult = from s in this.Sensors
                                                     where s.Name == elem.Attribute("sensor").Value
                                                     select s;

                            if (searchSensorResult == null)
                            {
                                throw new SmartHomeConfigException();
                            }

                            searchSensorResult.First().onChange += trigger.Invoke;
                            break;
                        default:
                            throw new SmartHomeNotAllowTypeException();
                    }
                }
                catch (SmartHomeNotConfigTypeException)
                {
                    Console.WriteLine("Your config {0} don't have type attribute!", elem.ToString());
                }
                catch (SmartHomeNotTypeException)
                {
                    Console.WriteLine("Your plugins don't have type {0}!", elem.Attribute("type"));
                }
                catch (SmartHomeNotAllowTypeException)
                {
                    Console.WriteLine("I don't support type of config {0} :'(", elem.ToString());
                }
                catch (InvalidCastException)
                {
                    Console.WriteLine("Your plugin don't have {0} with type {1}!", elem.Name.ToString(), elem.Attribute("type").Value);
                }
                catch (SmartHomeNotConfigNameException)
                {
                    Console.WriteLine("Your {0} don't have name attribute!", elem.Name.ToString());
                }
                catch (SmartHomeConfigException)
                {
                    Console.WriteLine("Your config {0} is bad!", elem.ToString());
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

    public class SmartHomeStartException : ApplicationException { }
    public class SmartHomeStopException : ApplicationException { }
    public class SmartHomeNotAllowTypesException : ApplicationException { }
    public class SmartHomeNotPluginsException : ApplicationException { }
    public class SmartHomeNotConfigException : ApplicationException { }
    public class SmartHomeNotConfigTypeException : ApplicationException { }
    public class SmartHomeNotTypeException : ApplicationException { }
    public class SmartHomeNotAllowTypeException : ApplicationException { }
    public class SmartHomeNotConfigNameException : ApplicationException { }
    public class SmartHomeConfigException : ApplicationException { }
}
