using SmartHome.Core.Models;
using SmartHome.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.WebAPI.Controllers
{
    public class SensorDetailsController : ApiController
    {
        ISensorConverter<SensorDetailsModel> repository;

        public SensorDetailsController(ISensorConverter<SensorDetailsModel> repository)
        {
            this.repository = repository;
        }

        // GET api/sensordetails
        public List<SensorDetailsModel> Get()
        {
            return this.repository.GetAll();
        }

        // GET api/sensordetails/5
        public SensorDetailsModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/sensordetails
        public SensorDetailsModel Post([FromBody]SensorDetailsModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/sensordetails/5
        public bool Put(int id, [FromBody]SensorDetailsModel item)
        {
            item.ID = id;
            return this.repository.Update(item);
        }

        // DELETE api/sensordetails/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
