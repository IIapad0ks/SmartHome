using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SmartHome.WebAPI.Models;

namespace SmartHome.WebAPI.Controllers
{
    public class ApplianceController : ApiController
    {
        private SmartHomeDBEntities db = new SmartHomeDBEntities();

        // GET api/Appliance
        public IEnumerable<Appliance> GetAppliances()
        {
            var appliances = db.Appliances;
            return appliances.AsEnumerable();
        }

        // GET api/Appliance/5
        public Appliance GetAppliance(int id)
        {
            Appliance appliance = db.Appliances.Find(id);
            if (appliance == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return appliance;
        }

        // PUT api/Appliance/5
        public HttpResponseMessage PutAppliance(int id, Appliance appliance)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != appliance.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(appliance).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Appliance
        public HttpResponseMessage PostAppliance(Appliance appliance)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response;
                Appliance existsAppliance = db.Appliances.Where(o => o.Name == appliance.Name && o.TypeId == appliance.TypeId).FirstOrDefault();
                if (existsAppliance != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.Found, existsAppliance);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = existsAppliance.Id }));
                    return response;
                }
                
                db.Appliances.Add(appliance);
                db.SaveChanges();

                response = Request.CreateResponse(HttpStatusCode.Created, appliance);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = appliance.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Appliance/5
        public HttpResponseMessage DeleteAppliance(int id)
        {
            Appliance appliance = db.Appliances.Find(id);
            if (appliance == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Appliances.Remove(appliance);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, appliance);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}