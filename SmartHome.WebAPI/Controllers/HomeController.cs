using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartHome.WebAPI.Models;

namespace SmartHome.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private SmartHomeDBEntities db = new SmartHomeDBEntities();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            List<HomeModel> devices = new List<HomeModel>();
            List<Appliance> appliances = db.Appliances.ToList();
            foreach (Appliance appl in appliances)
            {
                HomeModel device = new HomeModel();
                device.Device = appl;
                device.Type = db.ObjectTypes.Find(device.Device.TypeId);
                device.Events = db.EventLogs.Where(e => e.ObjectId == device.Device.Id).ToList();
                devices.Add(device);
            }

            return View(devices);
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            HomeModel device = new HomeModel();
            device.Device = db.Appliances.Find(id);
            device.Type = db.ObjectTypes.Find(device.Device.TypeId);
            device.Events = db.EventLogs.Where(e => e.ObjectId == device.Device.Id).ToList();

            return View(device);
        }

        //
        // GET: /Home/Start/5

        public ActionResult Start()
        {
            SmartHomeServiceReference.SmartHomeServiceClient client = new SmartHomeServiceReference.SmartHomeServiceClient();
            client.StartAsync();
            return this.Index();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}