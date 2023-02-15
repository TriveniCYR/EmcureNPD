using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly EmcureCERIDBContext _db;
        public LinkController(EmcureCERIDBContext context)
        {
            _db = context;
        }
        // GET api/Link
        [HttpGet]
        public IEnumerable<GanttLinkData> Get(Int64 DRFID,string strAction)
        {
            int tempdrfid = Convert.ToInt32(DRFID);
            string tempAction = strAction;
            //var result = _db.Tbl_Gantt_Link.ToList().Select(t => t);
            var result = _db.Tbl_Gantt_Link
                            .AsNoTracking()
                           .Where(t => t.DRFID == tempdrfid && t.Action == tempAction)                        
                            .Select(t => t).ToList();
            List<GanttLinkVM> objVM = new List<GanttLinkVM>();
            foreach(var ddata in result)
            {
                objVM.Add(new GanttLinkVM { 
                     Id = ddata.Id, Type = ddata.Type, SourceTaskId = ddata.SourceTaskId,TargetTaskId=ddata.TargetTaskId
                });
            }            
            return objVM.ToList().Select(t => (GanttLinkData)t);            
        }

        // GET api/Link/5
        [HttpGet("{id}")]
        public GanttLinkData Get(int id)
        {
            var result = _db.Tbl_Gantt_Link.ToList().Select(t => t);
            List<GanttLinkVM> objVM = new List<GanttLinkVM>();
            foreach (var ddata in result)
            {
                objVM.Add(new GanttLinkVM
                {
                    Id = ddata.Id,
                    Type = ddata.Type,
                    SourceTaskId = ddata.SourceTaskId,
                    TargetTaskId = ddata.TargetTaskId
                });
            }            
            return (GanttLinkData)objVM.Where(t=> t.Id==id);            
        }

        // POST api/Link
        [HttpPost]
        public ObjectResult Post(GanttLinkData ganttLinkData )
        {
            Int64 tempdrfid = Convert.ToInt64(HttpContext.Session.GetString("DrfID"));
            string tempAction = HttpContext.Session.GetString("Action");
            Tbl_Gantt_Link tbl_Gantt_Link = new Tbl_Gantt_Link();
            tbl_Gantt_Link.Id = ganttLinkData.id;
            tbl_Gantt_Link.Action = tempAction;
            tbl_Gantt_Link.DRFID = tempdrfid;
            tbl_Gantt_Link.Type = ganttLinkData.type;
            tbl_Gantt_Link.SourceTaskId = ganttLinkData.source;
            tbl_Gantt_Link.TargetTaskId = ganttLinkData.target;
            
            _db.Tbl_Gantt_Link.Add(tbl_Gantt_Link);
            _db.SaveChanges();

            var targetResult = _db.Tbl_Master_ProjectTask_Mapping.AsNoTracking().Where(x=> x.Drfid == Convert.ToInt32(tempdrfid) && x.Action == "DRF" & x.ProjectTaskMappingID == ganttLinkData.target).FirstOrDefault();

            //Get source ProjectTaskMappingID 
            var sourceResult =  _db.Tbl_Master_ProjectTask_Mapping.AsNoTracking().Where(y=> y.Drfid == Convert.ToInt32(tempdrfid) && y.Action == "DRF" & y.ProjectTaskMappingID == ganttLinkData.source).FirstOrDefault();
            if ((targetResult != null) && (sourceResult != null))
            {
                Tbl_Master_ProjectTask_Mapping tbl_Master_ProjectTask_Mapping = _db.Tbl_Master_ProjectTask_Mapping.AsNoTracking().Where(d => d.Drfid == Convert.ToInt64(tempdrfid) && d.ProjectTaskMappingID == targetResult.ProjectTaskMappingID && d.Action == "DRF").FirstOrDefault();
                if (tbl_Master_ProjectTask_Mapping != null)
                {
                    try
                    {
                        tbl_Master_ProjectTask_Mapping.StartDate = Convert.ToDateTime(sourceResult.EndDate);
                        tbl_Master_ProjectTask_Mapping.EndDate = Convert.ToDateTime(sourceResult.EndDate).AddDays(tbl_Master_ProjectTask_Mapping.TaskDuration.Value);
                        tbl_Master_ProjectTask_Mapping.DeadLine = Convert.ToDateTime(sourceResult.EndDate).AddDays(tbl_Master_ProjectTask_Mapping.TaskDuration.Value);
                        tbl_Master_ProjectTask_Mapping.Planned_Start = Convert.ToDateTime(sourceResult.EndDate);
                        tbl_Master_ProjectTask_Mapping.Planned_End = Convert.ToDateTime(sourceResult.EndDate).AddDays(tbl_Master_ProjectTask_Mapping.TaskDuration.Value);
                        _db.Entry(tbl_Master_ProjectTask_Mapping).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }


                return Ok(new {tid = tbl_Gantt_Link.Id, action = "inserted"});
        }

        // PUT api/Link/5
        [HttpPut("{id}")]
        public ObjectResult Put(int id, GanttLinkData ganttLinkData)
        {
            Int64 tempdrfid = Convert.ToInt64(HttpContext.Session.GetString("DrfID"));
            string tempAction = HttpContext.Session.GetString("Action");
            Tbl_Gantt_Link tbl_Gantt_Link = new Tbl_Gantt_Link();
            tbl_Gantt_Link.Id = id;
            tbl_Gantt_Link.Action = tempAction;
            tbl_Gantt_Link.DRFID = tempdrfid;
            tbl_Gantt_Link.Type = ganttLinkData.type;
            tbl_Gantt_Link.SourceTaskId = ganttLinkData.source;
            tbl_Gantt_Link.TargetTaskId = ganttLinkData.target;
            
            _db.Entry(tbl_Gantt_Link).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(new {action = "updated"});
        }

        // DELETE api/Link/5
        [HttpDelete("{id}")]
        public ObjectResult DeleteLink(int id)
        {
            var Link = _db.Tbl_Gantt_Link.Find(id);
            if (Link != null)
            {
                _db.Tbl_Gantt_Link.Remove(Link);
                _db.SaveChanges();
            }

            return Ok(new {action = "deleted" });
        }
    }
}
