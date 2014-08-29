using SmartHome.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core
{
    public static class WebAPIManager
    {
        private const string WebAPIUri = "http://localhost:53605/";
        public static List<EventLog> Events { get; set; }

        static WebAPIManager()
        {
            Events = new List<EventLog>();
        }

        public static int SaveType(Type type)
        {
            int? id = null;
            if (type.BaseType != null && type.BaseType.GetInterfaces().Any(i => i.Name == "IConfig"))
            {
                id = SaveType(type.BaseType);
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebAPIUri);
                HttpResponseMessage response = client.PostAsJsonAsync("api/ObjectType", new ObjectType() { Name = type.FullName, ParentId = id }).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new SmartHomeWebAPIException(String.Format("I can't save or find your config type {0} :'(", type.Name));
                }

                id = response.Content.ReadAsAsync<ObjectType>().Result.Id;
            }

            return (int)id;
        }

        public static int SaveConfig(IConfig config, int typeID)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebAPIUri);
            HttpResponseMessage response = client.PostAsJsonAsync("api/Appliance", new Appliance() { Name = config.Name, TypeId = typeID }).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new SmartHomeWebAPIException(String.Format("I can't save your config {0} :'(", config.Name));
            }

            return response.Content.ReadAsAsync<Appliance>().Result.Id;
        }

        public static void AddEvent(IConfig config, string action)
        {
            Events.Add(new EventLog { ObjectId = config.ID, ObjectState = config.WriteXml(), Action = action, Datetime = DateTime.Now });
        }

        public static void SaveEvents()
        {
            try
            {
                if (Events.Count == 0)
                {
                    throw new SmartHomeWebAPIException(String.Format("I don't have any event for save :'("));
                }

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WebAPIUri);
                    HttpResponseMessage response = client.PostAsJsonAsync("api/EventLog", Events).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new SmartHomeWebAPIException(String.Format("I can't save config events :'("));
                    }
                }

                Console.WriteLine(String.Format("Events are saved! Count: {0}", Events.Count));
                Events.Clear();
            }
            catch (SmartHomeConfigException shce)
            {
                Console.WriteLine(shce.Message);
            }
            catch (SmartHomeWebAPIException shwe)
            {
                Console.WriteLine(shwe.Message);
            }
        }
    }
}
