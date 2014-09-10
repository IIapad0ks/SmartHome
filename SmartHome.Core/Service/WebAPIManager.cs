using SmartHome.Core.SmartHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Models;
using Newtonsoft.Json;

namespace SmartHome.Core.Service
{
    public class WebAPIManager : IWebAPIManager
    {
        private HttpClient client;
        public Uri Uri { get; set; }

        public WebAPIManager()
        {
            this.client = new HttpClient();
            this.Uri = new Uri("http://localhost:53605/api");
        }

        public List<T> Get<T>() where T : class, IModel
        {
            return this.client.GetAsync(String.Format("{0}/{1}/", this.Uri, this.GetControllerName<T>())).Result.Content.ReadAsAsync<IQueryable<T>>().Result.ToList();
        } 

        public T Get<T>(int id) where T : class, IModel
        {
            return this.client.GetAsync(String.Format("{0}/{1}/{2}", this.Uri, this.GetControllerName<T>(), id)).Result.Content.ReadAsAsync<T>().Result;
        }

        public T Save<T>(T item) where T : class, IModel
        {
            return this.client.PostAsJsonAsync<T>(String.Format("{0}/{1}/", this.Uri, this.GetControllerName<T>()), item).Result.Content.ReadAsAsync<T>().Result;
        }

        public bool Update<T>(T item) where T : class, IModel
        {
            return this.client.PutAsJsonAsync<T>(String.Format("{0}/{1}/{2}", this.Uri, this.GetControllerName<T>(), item.ID), item).Result.Content.ReadAsAsync<bool>().Result;
        }

        public bool Delete<T>(int id) where T : class, IModel
        {
            return this.client.DeleteAsync(String.Format("{0}/{1}/{2}", this.Uri, this.GetControllerName<T>(), id)).Result.Content.ReadAsAsync<bool>().Result;
        }

        public void SaveEvents(List<EventLogModel> eventList)
        {
            HttpResponseMessage response = this.client.PostAsJsonAsync<List<EventLogModel>>(String.Format("{0}/eventlog", this.Uri), eventList).Result;
        }

        public void SHCommandRequest(int id, SHCommamd command)
        {
            HttpResponseMessage response = this.client.GetAsync(String.Format("{0}/smarthome/{1}/{2}", this.Uri, id, (int)command)).Result;
        }

        private string GetControllerName<T>()
        {
            return typeof(T).Name.Replace("Model", "");
        }
    }
}
