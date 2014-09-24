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
    public class ActionController : ApiController
    {
        IActionConverter repository;

        public ActionController(IActionConverter repository)
        {
            this.repository = repository;
        }

        // GET api/action
        public List<ActionModel> Get()
        {
            return this.repository.Get();
        }

        // GET api/action/5
        public ActionModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/action
        public ActionModel Post([FromBody]ActionModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/action/5
        public bool Put(int id, [FromBody]ActionModel item)
        {
            item.Id = id;
            return this.repository.Update(item);
        }

        // DELETE api/action/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
