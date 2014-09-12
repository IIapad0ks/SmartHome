using SmartHome.Core.Models;
using SmartHome.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartHome.WebClient.Controllers
{
    public class DeviceController : Controller
    {
        private IWebAPIManager webAPIManager;

        public DeviceController(IWebAPIManager webAPIManager)
        {
            this.webAPIManager = webAPIManager;
        }
        //
        // GET: /Device/
        public ActionResult Index()
        {
            return View(this.webAPIManager.Get<DeviceModel>());
        }

        // GET: /device/5
        public ActionResult Details(int id)
        {
            return View(this.webAPIManager.Get<DeviceDetailsModel>(id));
        }
	}
}