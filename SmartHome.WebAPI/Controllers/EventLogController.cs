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
    public class EventLogController : ApiController
    {
        IEventLogRepository repository;

        public EventLogController(IEventLogRepository repository)
        {
            this.repository = repository;
        }

        // GET api/eventlog
        public IQueryable<EventLogModel> Get()
        {
            return this.repository.GetAll();
        }

        // GET api/eventlog/5
        public EventLogModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/eventlog
        public EventLogModel Post([FromBody]EventLogModel item)
        {
            return this.repository.Add(item);
        }

        // POST api/eventlogs
        [Route("api/eventlogs")]
        public void Post([FromBody]IQueryable<EventLogModel> items)
        {
            this.repository.Add(items);
        }

        // PUT api/eventlog/5
        public bool Put(int id, [FromBody]EventLogModel item)
        {
            item.ID = id;
            return this.repository.Update(item);
        }

        // DELETE api/eventlog/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
