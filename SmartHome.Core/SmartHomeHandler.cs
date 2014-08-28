using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Net.Http;
using System.Threading.Tasks;
using SmartHome.WebAPI.Models;
using System.Web;
using System.Net.Http.Headers;

namespace SmartHome.Core
{
    public class SmartHomeHandler
    {
        private const string WebAPIUri = "http://localhost:53605/";

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
                    throw new SmartHomeException("SmartHome can't be started twice!");
                }

                this.isOn = true;
                Console.WriteLine("Starting...");

                this.Controllers.Clear();
                this.Sensors.Clear();
                this.Triggers.Clear();

                List<Type> allowTypes = this.GetAllTypes();
                if (allowTypes.Count() == 0)
                {
                    throw new SmartHomeException("SmartHome is not found any appliance.");
                }

                this.InitHomeObjects(allowTypes);

                foreach (ISensor sensor in this.Sensors)
                {
                    sensor.StartAsync();
                }
                Console.WriteLine("SmartHome is started.");
            }
            catch (SmartHomeException she)
            {
                Console.WriteLine(she.Message);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory {0} is not found. I can't load you appliances :'(", this.libDirname);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("I can't found your configs ;'(");
            }
            catch (XmlException)
            {
                Console.WriteLine("Your configs are bad!");
            }
            catch (SmartHomeConfigException shce)
            {
                Console.WriteLine(shce.Message);
            }
        }   

        public void Stop()
        {
            try
            {
                if (!this.isOn)
                {
                    throw new SmartHomeException("SmartHome can't be stopped. It's not working now :).");
                }

                foreach (ISensor sensor in this.Sensors)
                {
                    sensor.Stop();
                }

                this.isOn = false;
                Console.WriteLine("SmartHome is stopped.");
            }
            catch (SmartHomeException she)
            {
                Console.WriteLine(she.Message);
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
                throw new SmartHomeException("I can't found any plugins with you appliance :'(");
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
                throw new SmartHomeConfigException("Your config file don't have any configs!");
            }

            foreach (XElement elem in configs)
            {
                string elemName = elem.Name.ToString();

                try
                {
                    if (elem.Attribute("type") == null)
                    {
                        throw new SmartHomeConfigException(String.Format("Your {0} don't have attribute {1}! {2}", elemName, "type", elem.ToString()));
                    }

                    string typeName = elem.Attribute("type").Value;
                    var currentTypeSelectResult = from type in allowTypes where type.Name == typeName select type;
                    if (currentTypeSelectResult == null)
                    {
                        throw new SmartHomePluginException(String.Format("Your plugins don't have type {0}!", typeName));
                    }
                    if (currentTypeSelectResult.Count() > 1)
                    {
                        throw new SmartHomePluginException(String.Format("Your plugins have more than one type {0}!", typeName));
                    }

                    if (elem.Attribute("name") == null)
                    {
                        throw new SmartHomeConfigException(String.Format("Your {0} don't have attribute {1}! {2}", elemName, "name", elem.ToString()));
                    }

                    Type currentType = currentTypeSelectResult.First();
                    int typeID = SmartHomeHandler.SaveType(currentType);

                    IConfig config = (IConfig)Activator.CreateInstance(currentType);
                    config.Name = elem.Attribute("name").Value;
                    switch (elemName)
                    {
                        case "controller":
                            this.CheckConfigName<IController>(config.Name, this.Controllers, elemName);
                            IController controller = (IController)config;
                            this.Controllers.Add(controller);
                            break;
                        case "sensor":
                            this.CheckConfigName<ISensor>(config.Name, this.Sensors, elemName);
                            ISensor sensor = (ISensor)config;
                            this.Sensors.Add(sensor);
                            break;
                        case "trigger":
                            this.CheckConfigName<ITrigger>(config.Name, this.Triggers, elemName);
                            ITrigger trigger = (ITrigger)config;

                            if (elem.Attribute("controller") == null)
                            {
                                throw new SmartHomeConfigException(String.Format("Your {0} don't have attribute {1}! {2}", elemName, "controller", elem.ToString()));
                            }

                            string controllerName = elem.Attribute("controller").Value;
                            var searchControllerResult = from c in this.Controllers
                                                         where c.Name == controllerName
                                                         select c;
                            if (!searchControllerResult.Any())
                            {
                                throw new SmartHomeConfigException(String.Format("Your configs don't have {0} with name {1}! {2}", "controller", controllerName, elem.ToString()));
                            }

                            trigger.Controller = searchControllerResult.First();

                            if (elem.Attribute("condition") == null)
                            {
                                throw new SmartHomeConfigException(String.Format("Your {0} don't have attribute {1}! {2}", elemName, "condition", elem.ToString()));
                            }

                            trigger.Condition = elem.Attribute("condition").Value;

                            Dictionary<string, string> elemParams = new Dictionary<string, string>();
                            foreach (XElement child in elem.Elements())
                            {
                                string key = child.Name.ToString();
                                if (child.Attribute("value") == null)
                                {
                                    throw new SmartHomeConfigException(String.Format("Your {0} parameter {1} don't have attribute {2}! {3}", elemName, key, "value", elem.ToString()));
                                }

                                if (elemParams.Any(p => p.Key == key))
                                {
                                    throw new SmartHomeConfigException(String.Format("Your {0} have more than one parameter {1}! {2}", elemName, key, elem.ToString()));
                                }

                                elemParams.Add(key, child.Attribute("value").Value);
                            }
                            trigger.Properties = elemParams;

                            if (elem.Attribute("sensor") == null)
                            {
                                throw new SmartHomeConfigException(String.Format("Your {0} don't have attribute {1}! {2}", elemName, "sensor", elem.ToString()));
                            }

                            string sensorName = elem.Attribute("sensor").Value;
                            var searchSensorResult = from s in this.Sensors
                                                     where s.Name == sensorName
                                                     select s;
                            if (!searchSensorResult.Any())
                            {
                                throw new SmartHomeConfigException(String.Format("Your configs don't have {0} with name {1}! {2}", "sensor", sensorName, elem.ToString()));
                            }

                            ISensor triggerSensor = searchSensorResult.First();
                            triggerSensor.onChange += trigger.Invoke;
                            triggerSensor.onChange += (object sender, EventArgs args) =>
                            {
                                SmartHomeHandler.SaveConfigEvent((IConfig)sender, "change");
                            };

                            this.Triggers.Add(trigger);
                            break;
                        default:
                            throw new SmartHomeConfigException(String.Format("I don't support config type {0} :'(", elemName));
                    }

                    config.ID = SmartHomeHandler.SaveConfig(config, typeID);
                }
                catch (SmartHomePluginException shpe)
                {
                    Console.WriteLine(shpe.Message);
                }
                catch (InvalidCastException)
                {
                    Console.WriteLine("Your plugin don't have {0} with type {1}!", elemName, elem.Attribute("type").Value);
                }
                catch (SmartHomeConfigException shce)
                {
                    Console.WriteLine(shce.Message);
                }
            }
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

        private void CheckConfigName<T>(string configName, List<T> configs, string configType = "config") where T : IConfig
        {
            if ((from c in configs where c.Name == configName select c).Count() == 1)
            {
                throw new SmartHomeConfigException(String.Format("Your config have more than one {1} with name {0}!", configName, configType));
            }
        }

        public static int SaveType(Type type)
        {
            int? id = null;
            if (type.BaseType != null && type.BaseType.GetInterfaces().Any(i => i.Name == "IConfig"))
            {
                id = SmartHomeHandler.SaveType(type.BaseType);
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(SmartHomeHandler.WebAPIUri);
                HttpResponseMessage response = client.PostAsJsonAsync("api/ObjectType", new ObjectType() { Name = type.FullName, ParentId = id }).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new SmartHomeConfigException(String.Format("I can't save or find your config type {0} :'(", type.Name));
                }

                id = response.Content.ReadAsAsync<ObjectType>().Result.Id;
            }

            return (int)id;
        }

        public static int SaveConfig(IConfig config, int typeID)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(SmartHomeHandler.WebAPIUri);
            HttpResponseMessage response = client.PostAsJsonAsync("api/Appliance", new Appliance() { Name = config.Name, TypeId = typeID }).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new SmartHomeConfigException(String.Format("I can't save your config {0} :'(", config.Name));
            }

            return response.Content.ReadAsAsync<Appliance>().Result.Id;
        }

        public static void SaveConfigEvent(IConfig config, string action)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(SmartHomeHandler.WebAPIUri);
                    HttpResponseMessage response = client.PostAsJsonAsync("api/EventLog", new EventLog() { ObjectId = config.ID, Action = action, Datetime = DateTime.Now, ObjectState = config.WriteXml() }).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new SmartHomeConfigException(String.Format("I can't save '{1}' config event: action - {0}!  :'(", action, config.Name));
                    }
                }
            }
            catch (SmartHomeConfigException shce)
            {
                Console.WriteLine(shce.Message);
            }
        }
    }
}
