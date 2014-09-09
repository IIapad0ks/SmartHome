using SmartHome.Core.Models;
using SmartHome.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartHome.WebAPI.Controllers
{
    public class SensorController : ApiController
    {
        private ISensorRepository repository;

        public SensorController(ISensorRepository repository)
        {
            this.repository = repository;
        }

        // GET api/sensor
        public IQueryable<SensorModel> Get()
        {
            return this.repository.GetAll();
        }

        // GET api/sensor/5
        public SensorModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/sensor
        public SensorModel Post([FromBody]SensorModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/sensor/5
        public bool Put(int id, [FromBody]SensorModel item)
        {
            item.ID = id;
            return this.repository.Update(item);
        }

        // DELETE api/sensor/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
