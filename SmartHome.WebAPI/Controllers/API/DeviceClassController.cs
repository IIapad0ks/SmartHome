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
    public class DeviceClassController : ApiController
    {
        IDeviceClassConverter repository;

        public DeviceClassController(IDeviceClassConverter repository)
        {
            this.repository = repository;
        }

        // GET api/deviceclass
        public List<DeviceClassModel> Get()
        {
            return this.repository.Get();
        }

        // GET api/deviceclass/5
        public DeviceClassModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/deviceclass
        public DeviceClassModel Post([FromBody]DeviceClassModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/deviceclass/5
        public bool Put(int id, [FromBody]DeviceClassModel item)
        {
            item.Id = id;
            return this.repository.Update(item);
        }

        // DELETE api/deviceclass/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
