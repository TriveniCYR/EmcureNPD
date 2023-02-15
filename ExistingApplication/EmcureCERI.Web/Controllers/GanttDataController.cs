using EmcureCERI.Business.Contract;
using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Hubs;
using EmcureCERI.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GanttDataController : ControllerBase
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env;
        private readonly IDRFService _DRF;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly ISMTPService _sMTPService;
        private readonly IEmailService _emailService;

        [Obsolete]
        public GanttDataController(EmcureCERIDBContext context, IConfiguration config, IHostingEnvironment env, IDRFService dRFService, IHubContext<NotificationHub> notificationHubContext, IEmailService emailService, ISMTPService sMTPService)
        {
            _db = context;
            _config = config;
            _env = env;
            _DRF = dRFService;
            _notificationHubContext = notificationHubContext;
            _emailService = emailService;
            _sMTPService = sMTPService;
        }
        // GET api/task
        // GET api/Link
        [HttpGet]
        [Obsolete]
        public async Task<GanttData> Get()
        {
            Int64 tempDrfID = Convert.ToInt32(HttpContext.Session.GetString("DrfID"));
            string tempAction = HttpContext.Session.GetString("Action");
            return new GanttData
            {
                data = await Task.Run(()=>  new TaskController(_config, _env,_db, _DRF, _notificationHubContext, _emailService, _sMTPService).Get(tempDrfID, tempAction)),
                //data = Task.Run(()=>) new  TaskController(_config, _env,_db, _DRF, _notificationHubContext, _emailService, _sMTPService).Get(tempDrfID),
                links = new LinkController(_db).Get(tempDrfID, tempAction),
                collections = new Dictionary<string, object>
                {
                    { "resources", new ResourceController(_db).Get() }
                }
            };
        }
    }
}
