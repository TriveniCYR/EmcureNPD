﻿using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Mvc;
using static EmcureNPD.Utility.Enums.GeneralEnum;
using System.Net;
using System.Threading.Tasks;
using System;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.API.Filters;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Business.Core.ServiceImplementations;

namespace EmcureNPD.API.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IResponseHandler<dynamic> _ObjectResponse;
        public ProjectController(IProjectService projectService, IResponseHandler<dynamic> ObjectResponse)
        {
            _projectService = projectService;
            _ObjectResponse = ObjectResponse;
        }
        [HttpGet, Route("GetDropdownsForAddTask")]
        public ActionResult GetDropdownsForAddTask()
        {
            var oResponse = _projectService.GetDropDownsForTask();
            return Ok(oResponse);

        }
        [HttpPost, Route("AddUpdateTaskDetails")]
        public async Task<IActionResult> AddUpdateTaskDetails(ProjectTaskEntity TaskAddModel)
        {
            try
            {
                DBOperation oResponse = await _projectService.AddUpdateTaskDetail(TaskAddModel);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (int)HttpStatusCode.OK, "Save Successfully");
                else
                    return _ObjectResponse.Create(false, (int)HttpStatusCode.BadRequest, oResponse == DBOperation.NotFound ? "Record not found" : "Bad request");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpGet, Route("GetTaskSubTask/{pidfId}")]
        public async Task<IActionResult> GetTaskSubTask([FromRoute] string pidfId)
        {
            try
            {
                var oTaskEntity = _projectService.GetTaskSubTaskList(long.Parse(UtilityHelper.Decreypt(pidfId))); ;
                if (oTaskEntity != null)
                    return _ObjectResponse.Create(oTaskEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
            //var result = _projectService.GetTaskSubTaskList(long.Parse(UtilityHelper.Decreypt(pidfId)));
            //return Ok(result);
        }
    }
}
