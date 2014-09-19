using SmartHome.Core.DBModelConverters;
using SmartHome.Core.Models;
using SmartHome.Core.Repositories;
using SmartHome.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartHome.WebAPI.Controllers
{
    public class DeviceDetailsController : ApiController
    {
        IDeviceConverter<DeviceDetailsModel> repository;

        public DeviceDetailsController(IDeviceConverter<DeviceDetailsModel> repository)
        {
            this.repository = repository;
        }

        // GET api/devicedetails
        public List<DeviceDetailsModel> Get()
        {
            return this.repository.GetAll();
        }

        // GET api/devicedetails/5
        public DeviceDetailsModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/devicedetails
        public DeviceDetailsModel Post([FromBody]DeviceDetailsModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/devicedetails/5
        public bool Put(int id, [FromBody]DeviceDetailsModel item)
        {
            item.ID = id;
            return this.repository.Update(item);
        }

        // DELETE api/devicedetails/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
