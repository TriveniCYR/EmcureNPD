using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Business.Core.ServiceImplementations;
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
    public class GanttSummaryTaskController : ControllerBase
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IGanttSummaryService _ganttSummaryService;
        
        public GanttSummaryTaskController(EmcureCERIDBContext context, IGanttSummaryService ganttSummaryService)
        {
            _db = context;
            this._ganttSummaryService = ganttSummaryService;
        }
        
        [HttpGet]
        public IEnumerable<GanttSummaryTaskData> Get(string strAction)
        {

            //int tempdrfid = Convert.ToInt32(DRFID);
            var result = _ganttSummaryService.GetAllProjectSummary(strAction);                         
            List<GanttSummaryTaskVM> objVM = new List<GanttSummaryTaskVM>();
            foreach (var ddata in result)
            {
                objVM.Add(new GanttSummaryTaskVM
                {
                    Id = (int)ddata.id,
                    Text = ddata.TaskName,
                    StartDate = Convert.ToDateTime(ddata.StartDate),
                    EndDate = Convert.ToDateTime(ddata.EndDate),
                    Duration = (int)ddata.TaskDuration,
                    SortOrder = 0,
                    //Progress = Math.Round(ddata.ProjectStatus,2),
                    Progress = Math.Round(ddata.TotalProgress, 2),
                    //ParentId = (int)ddata.ParentID,
                    //Owner = (int)ddata.EmpID,
                    Type = "task",//ddata.Type,
                    //Priority = Convert.ToString(ddata.PriorityID)

                });
            }
            return objVM.ToList().Select(t => (GanttSummaryTaskData)t);
        }
    }
}
