using SmartHome.Core.Models;
using SmartHome.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.WebAPI.Controllers
{
    public class SHServiceController : ApiController
    {
        ISHServiceConverter repository;

        public SHServiceController(ISHServiceConverter repository)
        {
            this.repository = repository;
        }

        // GET api/shservice
        public List<SHServiceModel> Get()
        {
            return this.repository.Get();
        }

        // GET api/shservice/5
        public SHServiceModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/shservice
        public SHServiceModel Post([FromBody]SHServiceModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/shservice
        public bool Put([FromBody]SHServiceModel item)
        {
            return this.repository.Update(item);
        }

        // DELETE api/shservice/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
