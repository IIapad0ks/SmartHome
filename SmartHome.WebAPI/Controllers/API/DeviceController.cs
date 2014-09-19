using SmartHome.Core.DBModelConverters;
using SmartHome.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartHome.WebAPI.Controllers
{
    public class DeviceController : ApiController
    {
        IDeviceConverter<DeviceModel> repository;

        public DeviceController(IDeviceConverter<DeviceModel> repository)
        {
            this.repository = repository;
        }

        // GET api/device
        public List<DeviceModel> Get()
        {
            return this.repository.GetAll();
        }

        // GET api/device/5
        public DeviceModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/device
        public DeviceModel Post([FromBody]DeviceModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/device/5
        public bool Put(int id, [FromBody]DeviceModel item)
        {
            item.ID = id;
            return this.repository.Update(item);
        }

        // DELETE api/device/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
