﻿using SmartHome.Core.Models;
using SmartHome.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartHome.WebAPI.Controllers
{
    public class TriggerDetailsController : ApiController
    {
        ITriggerDetailsRepository repository;

        public TriggerDetailsController(ITriggerDetailsRepository repository)
        {
            this.repository = repository;
        }

        // GET api/triggerdetails
        public IQueryable<TriggerDetailsModel> Get()
        {
            return this.repository.GetAll();
        }

        // GET api/triggerdetails/5
        public TriggerDetailsModel Get(int id)
        {
            return this.repository.Get(id);
        }

        // POST api/triggerdetails
        public TriggerDetailsModel Post([FromBody]TriggerDetailsModel item)
        {
            return this.repository.Add(item);
        }

        // PUT api/triggerdetails/5
        public bool Put(int id, [FromBody]TriggerDetailsModel item)
        {
            item.ID = id;
            return this.repository.Update(item);
        }

        // DELETE api/triggerdetails/5
        public bool Delete(int id)
        {
            return this.repository.Remove(id);
        }
    }
}
