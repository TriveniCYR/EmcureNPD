using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Models;
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
    public class ResourceController : ControllerBase
    {
        private readonly EmcureCERIDBContext _db;
        public ResourceController(EmcureCERIDBContext context)
        {
            _db = context;
        }
        // GET api/Link
        [HttpGet]
        public IEnumerable<GanttResourcesVM> Get()
        {            
            var result = (from p in _db.PrescriberDetails
                          join q in _db.AspNetUsers on p.AspNetUserId equals q.UserId
                          where q.IsEnabled == true
                          select new { p.AspNetUserId, p.FirstName,p.LastName }).ToList();


            List < GanttResourcesVM > objVM = new List<GanttResourcesVM>();
            foreach (var ddata in result)
            {               

                objVM.Add(new GanttResourcesVM
                {                    
                    Id = (int)ddata.AspNetUserId,
                    Name = ddata.FirstName + " " + ddata.LastName,
                    ParentId = 1//ddata.ParentId
                });
            }
            return objVM.ToList();
            
        }

        // GET api/Link/5
        [HttpGet("{id}")]
        public GanttResourcesVM Get(int id)
        {
            ////var result = _db.PrescriberDetails
            //            .Where(t=> t.AspNetUserId == id)
            //            .Select(t => t).ToList();
            var result = (from p in _db.PrescriberDetails
                          join q in _db.AspNetUsers on p.Id equals q.UserId
                          where p.AspNetUserId == id
                          select new { p.AspNetUserId, p.FirstName, p.LastName }).ToList();
            List<GanttResourcesVM> objVM = new List<GanttResourcesVM>();
            foreach (var ddata in result)
            {
                objVM.Add(new GanttResourcesVM
                {
                    Id = (int)ddata.AspNetUserId,
                    Name = ddata.FirstName + " " + ddata.LastName,
                    ParentId = 1//ddata.ParentId
                });
            }            
            return (GanttResourcesVM) objVM.Where(t => t.Id == id);            
        }

    }
}
