using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GanttSummaryDataController : ControllerBase
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IGanttSummaryService _ganttSummaryService;
        public GanttSummaryDataController(EmcureCERIDBContext context, IGanttSummaryService ganttSummaryService)
        {
            _db = context;
            this._ganttSummaryService = ganttSummaryService;
        }
        [Authorize(Roles = "Senior Project Manager,Regulatory Manager,Prescriber")]
        [HttpGet]
        public GanttSummaryData Get()
        {
            string strAction = HttpContext.Session.GetString("Action");
            return new GanttSummaryData
            {
                data = new GanttSummaryTaskController(_db, _ganttSummaryService).Get(strAction)                
            };
        }
    }
}
