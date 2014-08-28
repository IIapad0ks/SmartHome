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
    public class EventLogController : ApiController
    {
        private SmartHomeDBEntities db = new SmartHomeDBEntities();

        // GET api/EventLog
        public IEnumerable<EventLog> GetEventLogs()
        {
            var eventlogs = db.EventLogs;
            return eventlogs.AsEnumerable();
        }

        // GET api/EventLog/5
        public EventLog GetEventLog(int id)
        {
            EventLog eventlog = db.EventLogs.Find(id);
            if (eventlog == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return eventlog;
        }

        // PUT api/EventLog/5
        public HttpResponseMessage PutEventLog(int id, EventLog eventlog)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != eventlog.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(eventlog).State = EntityState.Modified;

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

        // POST api/EventLog
        public HttpResponseMessage PostEventLog(EventLog eventlog)
        {
            if (ModelState.IsValid)
            {
                db.EventLogs.Add(eventlog);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, eventlog);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = eventlog.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/EventLog/5
        public HttpResponseMessage DeleteEventLog(int id)
        {
            EventLog eventlog = db.EventLogs.Find(id);
            if (eventlog == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.EventLogs.Remove(eventlog);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, eventlog);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}