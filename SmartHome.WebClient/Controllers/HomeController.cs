using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartHome.Core.Models;
using SmartHome.Core.Service;
using SH = SmartHome.Core.SmartHome;
using System.Web.Routing;
using System.IO;

namespace SmartHome.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private int shID = 8;
        private IWebAPIManager webAPIManager;

        public HomeController(IWebAPIManager webAPIManager)
        {
            this.webAPIManager = webAPIManager;
        }

        // GET: /Home/
        public ActionResult Index()
        {
            return View(this.webAPIManager.Get<SHServiceModel>(this.shID));
        }

        // GET: /home/edit
        public ActionResult Settings()
        {
            return View(this.webAPIManager.Get<SHServiceModel>(this.shID));
        }

        // GET: /home/edit
        [HttpPost]
        public ActionResult Settings(SHServiceModel shService)
        {
            this.webAPIManager.Update<SHServiceModel>(shService);
            return View(this.webAPIManager.Get<SHServiceModel>(this.shID));
        }

        // GET: /home/start
        public ActionResult Start()
        {
            this.webAPIManager.SHCommandRequest(this.shID, SH.SHCommamd.Start);
            return RedirectToAction(this.GetReferrerRouteData().Values["action"].ToString());
        }

        // GET: /home/stop
        public ActionResult Stop()
        {
            this.webAPIManager.SHCommandRequest(this.shID, SH.SHCommamd.Stop);
            return RedirectToAction(this.GetReferrerRouteData().Values["action"].ToString());
        }

        // GET: /home/restart
        public ActionResult Restart()
        {
            this.webAPIManager.SHCommandRequest(this.shID, SH.SHCommamd.Restart);
            return RedirectToAction(this.GetReferrerRouteData().Values["action"].ToString());
        }

        private RouteData GetReferrerRouteData()
        {
            var fullUrl = this.Request.UrlReferrer.ToString();
            string url = fullUrl;

            var request = new HttpRequest(null, url, null);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(request, response);

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            return routeData;
        }
    }
}
