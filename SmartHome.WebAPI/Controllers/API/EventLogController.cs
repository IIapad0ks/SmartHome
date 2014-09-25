using SmartHome.Core.Models;
using SmartHome.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.WebAPI.Controllers
{
    public class EventLogController : ApiController
    {
        IEventLogConverter repository;

        public EventLogController(IEventLogConverter repository)
        {
            this.repository = repository;
        }

        // GET api/eventlog
        public List<EventLogModel> Get()
        {
            return this.repository.Get();
        }

        // GET api/eventlog/5
        public EventLogModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/eventlog
        public void Post([FromBody]List<EventLogModel> item)
        {
            this.repository.Add(item);
        }

        // PUT api/eventlog
        public bool Put([FromBody]EventLogModel item)
        {
            return this.repository.Update(item);
        }

        // DELETE api/eventlog/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
