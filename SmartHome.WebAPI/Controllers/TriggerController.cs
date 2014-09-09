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
    public class TriggerController : ApiController
    {
        ITriggerRepository repository;

        public TriggerController(ITriggerRepository repository)
        {
            this.repository = repository;
        }

        // GET api/trigger
        public IQueryable<TriggerModel> Get()
        {
            return this.repository.GetAll();
        }

        // GET api/trigger/5
        public TriggerModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/trigger
        public TriggerModel Post([FromBody]TriggerModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/trigger/5
        public bool Put(int id, [FromBody]TriggerModel item)
        {
            item.ID = id;
            return this.repository.Update(item);
        }

        // DELETE api/trigger/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
