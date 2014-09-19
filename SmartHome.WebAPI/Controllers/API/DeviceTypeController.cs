using SmartHome.Core.DBModelConverters;
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
    public class DeviceTypeController : ApiController
    {
        IDeviceTypeConverter repository;

        public DeviceTypeController(IDeviceTypeConverter repository)
        {
            this.repository = repository;
        }

        // GET api/devicetype
        public List<DeviceTypeModel> Get()
        {
            return this.repository.GetAll();
        }

        // GET api/devicetype/5
        public DeviceTypeModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/devicetype
        public DeviceTypeModel Post([FromBody]DeviceTypeModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/devicetype/5
        public bool Put(int id, [FromBody]DeviceTypeModel item)
        {
            item.ID = id;
            return this.repository.Update(item);
        }

        // DELETE api/devicetype/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
