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
    public class ObjectTypeController : ApiController
    {
        private SmartHomeDBEntities db = new SmartHomeDBEntities();

        // GET api/ObjectType
        public IEnumerable<ObjectType> GetObjectTypes()
        {
            var objecttypes = db.ObjectTypes;
            return objecttypes.AsEnumerable();
        }

        // GET api/ObjectType/5
        public ObjectType GetObjectType(int id)
        {
            ObjectType objecttype = db.ObjectTypes.Find(id);
            if (objecttype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return objecttype;
        }

        // PUT api/ObjectType/5
        public HttpResponseMessage PutObjectType(int id, ObjectType objecttype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != objecttype.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            if (db.ObjectTypes.Any(o => o.Name == objecttype.Name && o.Id != id))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(objecttype).State = EntityState.Modified;

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

        // POST api/ObjectType
        public HttpResponseMessage PostObjectType(ObjectType objecttype)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response;
                ObjectType existsObjectType = db.ObjectTypes.Where(o => o.Name == objecttype.Name && (o.ParentId == objecttype.ParentId || (o.ParentId == null && objecttype.ParentId == null))).FirstOrDefault();
                if (existsObjectType != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.Found, existsObjectType);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = existsObjectType.Id }));
                    return response;
                }

                db.ObjectTypes.Add(objecttype);
                db.SaveChanges();

                response = Request.CreateResponse(HttpStatusCode.Created, objecttype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = objecttype.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ObjectType/5
        public HttpResponseMessage DeleteObjectType(int id)
        {
            ObjectType objecttype = db.ObjectTypes.Find(id);
            if (objecttype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ObjectTypes.Remove(objecttype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, objecttype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}