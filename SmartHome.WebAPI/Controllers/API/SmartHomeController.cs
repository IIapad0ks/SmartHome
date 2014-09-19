using SmartHome.Core.Repositories;
using SmartHome.Core.Service;
using SmartHome.Core.SmartHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ServiceProcess;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.WebAPI.Controllers
{
    public class SmartHomeController : ApiController
    {
        private ISHServiceConverter repository;
        private ServiceController serviceController;
        private int waitServiceSeconds = 60;

        public SmartHomeController(ISHServiceConverter repository)
        {
            this.serviceController = new ServiceController();
            this.repository = repository;
        }

        //GET api/smarthome/start/5/1
        [Route("api/smarthome/{id:int}/{command:int}")]
        public void Get(int id, int command)
        {
            this.initSHService(id);
            this.serviceController.ExecuteCommand(command);
        }

        private void initSHService(int id)
        {
            this.serviceController.ServiceName = this.repository.Get(id).Name;
            if (this.serviceController.Status == ServiceControllerStatus.Stopped)
            {
                this.serviceController.Start();
            }
            this.serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(this.waitServiceSeconds));
        }
    }
}
