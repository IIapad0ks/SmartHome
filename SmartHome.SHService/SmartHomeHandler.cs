using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Threading;
using SmartHome.Core.SmartHome;
using SmartHome.Core.Exceptions;
using SmartHome.Core.Models;
using SmartHome.Core.Service;

namespace SmartHome.Service
{
    public class SmartHomeHandler : ISmartHomeHandler, IDisposable
    {
        private int id;
        private string configFilename;
        private string libDirname;
        private string[] allowInterfaces = new string[] { "IController", "ISensor", "ITrigger" };
        private bool isOn = false;
        private int saveEventPeriod = 10000;
        private Timer saveEventTimer;

        private ISaveEventsManager eventsManager;
        private IWebAPIManager webAPIManager;

        public bool IsOn
        {
            get
            {
                return this.isOn;
            }
        }

        public List<IController> Controllers { get; set; }
        public List<ISensor> Sensors { get; set; }
        public List<ITrigger> Triggers { get; set; }

        public SmartHomeHandler(ISaveEventsManager saveEventsManager, IWebAPIManager webAPIManager)
        {
            this.eventsManager = saveEventsManager;
            this.webAPIManager = webAPIManager;

            this.id = 8;
            SHServiceModel shConfig = webAPIManager.Get<SHServiceModel>(this.id); 
            this.configFilename = shConfig.ConfigFilename;
            this.libDirname = shConfig.LibDirname;

            this.Controllers = new List<IController>();
            this.Sensors = new List<ISensor>();
            this.Triggers = new List<ITrigger>();
        }

        public bool Start()
        {
            try
            {
                if (this.isOn)
                {
                    throw new SmartHomeException("SmartHome can't be started twice!");
                }

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
                    sensor.TimerPeriod = 1000; //1 second
                    sensor.Start();
                }

                this.saveEventTimer = new Timer((object state) =>
                {
                    this.eventsManager.SaveEvents();
                }, null, this.saveEventPeriod, this.saveEventPeriod);

                this.isOn = true;
                this.UpdateState();
                Console.WriteLine("SmartHome is started.");

                return true;
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

            return false;
        }

        public bool Stop()
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

                if (this.saveEventTimer != null)
                {
                    this.saveEventTimer.Dispose();
                }

                this.eventsManager.SaveEvents();
                this.isOn = false;
                this.UpdateState();
                Console.WriteLine("SmartHome is stopped.");

                return true;
            }
            catch (SmartHomeException she)
            {
                Console.WriteLine(she.Message);
            }

            return false;
        }

        public bool Restart()
        {
            bool success = this.Stop();
            if (success)
            {
                success = this.Start();
            }

            return success;
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
                    IConfig config = (IConfig)Activator.CreateInstance(currentType);
                    config.Name = elem.Attribute("name").Value;
                    config.TypeID = this.webAPIManager.Save<DeviceTypeModel>(new DeviceTypeModel { Name = currentType.FullName }).ID;
                    config.onEvent += (object sender, SaveEventsManagerArgs args) =>
                    {
                        this.eventsManager.AddEvent((IConfig)sender, args.ActionName);
                    };

                    switch (elemName)
                    {
                        case "controller":
                            this.CheckConfigName<IController>(config.Name, this.Controllers, elemName);

                            IController controller = (IController)config;
                            controller.ID = this.webAPIManager.Save<DeviceModel>(new DeviceModel { Name = controller.Name, Type = new DeviceTypeModel { ID = controller.TypeID } }).ID;
                            this.Controllers.Add(controller);
                            break;
                        case "sensor":
                            this.CheckConfigName<ISensor>(config.Name, this.Sensors, elemName);

                            ISensor sensor = (ISensor)config;
                            sensor.ID = this.webAPIManager.Save<SmartHome.Core.Models.SensorModel>(new SmartHome.Core.Models.SensorModel { Name = sensor.Name, Type = new DeviceTypeModel { ID = sensor.TypeID } }).ID;
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

                            trigger.ID = this.webAPIManager.Save<SmartHome.Core.Models.TriggerModel>(new SmartHome.Core.Models.TriggerModel { Name = trigger.Name, Condition = trigger.Condition, Type = new DeviceTypeModel { ID = trigger.TypeID }, Device = new DeviceModel { ID = trigger.Controller.ID }, Sensor = new SmartHome.Core.Models.SensorModel { ID = triggerSensor.ID } }).ID;
                            this.Triggers.Add(trigger);
                            break;
                        default:
                            throw new SmartHomeConfigException(String.Format("I don't support config type {0} :'(", elemName));
                    }
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

        private void CheckConfigName<T>(string configName, List<T> configs, string configType = "config") where T : IConfig
        {
            if ((from c in configs where c.Name == configName select c).Count() == 1)
            {
                throw new SmartHomeConfigException(String.Format("Your config have more than one {1} with name {0}!", configName, configType));
            }
        }

        private void UpdateState()
        {
            //delete unused configs
            /*
            this.UpdateConfigs<DeviceModel, IController>(this.Controllers);
            this.UpdateConfigs<SensorModel, ISensor>(this.Sensors);
            this.UpdateConfigs<TriggerModel, ITrigger>(this.Triggers
             * */

            SHServiceModel sh = this.webAPIManager.Get<SHServiceModel>(this.id);
            sh.IsOn = this.IsOn;
            this.webAPIManager.Update<SHServiceModel>(sh);
        }

        private void UpdateConfigs<TModel, TConfig>(List<TConfig> configs) where TModel : class, IDeviceModel where TConfig : class, IConfig
        {
            List<TModel> allDevices = this.webAPIManager.Get<TModel>();
            List<TModel> deletedDevices = allDevices.Where(d => !configs.Any(c => c.ID == d.ID)).ToList();
            foreach (var deletedDevice in deletedDevices)
            {
                this.webAPIManager.Delete<TModel>(deletedDevice.ID);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
        
        private void Dispose(bool flag)
        {
            this.saveEventTimer.Dispose();
        }
    }
}
