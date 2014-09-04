using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmartHome.Core.Repositories;

namespace SmartHome.WebAPI.Controllers
{
    public class ActionController : ApiController
    {
        IActionRepository repository;

        public ActionController(IActionRepository repository)
        {
            this.repository = repository;
        }

        // GET api/action
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/action/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/action
        public void Post([FromBody]string value)
        {
        }

        // PUT api/action/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/action/5
        public void Delete(int id)
        {
        }
    }
}
