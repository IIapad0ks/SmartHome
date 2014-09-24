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
        IDeviceConverter repository;

        public DeviceController(IDeviceConverter repository)
        {
            this.repository = repository;
        }

        // GET api/device
        public List<DeviceModel> Get()
        {
            return this.repository.Get();
        }

        // GET api/device/5
        public DeviceModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // GET api/device/room/5
        [HttpGet]
        public List<DeviceModel> Room(int id)
        {
            return this.repository.Get(d => d.RoomId == id);
        }

        // GET api/device/device
        [HttpGet]
        public List<DeviceModel> Device()
        {
            return this.repository.Get(d => d.DeviceType.DeviceClassId == 1);
        }

        // GET api/device/sensor
        [HttpGet]
        public List<DeviceModel> Sensor()
        {
            return this.repository.Get(d => d.DeviceType.DeviceClassId == 2);
        }

        // POST api/device
        public DeviceModel Post([FromBody]DeviceModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/device/5
        public bool Put(int id, [FromBody]DeviceModel item)
        {
            item.Id = id;
            return this.repository.Update(item);
        }

        // DELETE api/device/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
