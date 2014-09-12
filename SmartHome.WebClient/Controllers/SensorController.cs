﻿using SmartHome.Core.Models;
using SmartHome.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartHome.WebClient.Controllers
{
    public class SensorController : Controller
    {
        private IWebAPIManager webAPIManager;

        public SensorController(IWebAPIManager webAPIManager)
        {
            this.webAPIManager = webAPIManager;
        }

        //
        // GET: /Sensor/
        public ActionResult Index()
        {
            return View(this.webAPIManager.Get<SensorModel>());
        }

        // GET: /sensor/details/5
        public ActionResult Details(int id)
        {
            return View(this.webAPIManager.Get<SensorDetailsModel>(id));
        }
	}
}