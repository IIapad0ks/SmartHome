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
    public class RoomController : ApiController
    {
        IRoomConverter repository;

        public RoomController(IRoomConverter repository)
        {
            this.repository = repository;
        }

        // GET api/room
        public List<RoomModel> Get()
        {
            return this.repository.Get();
        }

        // GET api/room/5
        public RoomModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/room
        public RoomModel Post([FromBody]RoomModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/room/5
        public bool Put(int id, [FromBody]RoomModel item)
        {
            item.Id = id;
            return this.repository.Update(item);
        }

        // DELETE api/room/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
