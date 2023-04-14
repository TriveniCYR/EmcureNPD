using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class RoleController : ControllerBase
    {

        #region Properties
        private readonly IMasterRoleService _MasterRoleService;
        private readonly IMasterModuleService _MasterModuleService;
        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IExceptionService _ExceptionService;

        #endregion

        #region Constructor
        public RoleController(IMasterRoleService MasterRoleService, IResponseHandler<dynamic> ObjectResponse, IMasterModuleService MasterModuleService, IExceptionService exceptionService)
        {
            _MasterRoleService = MasterRoleService;
            _ObjectResponse = ObjectResponse;
            _MasterModuleService = MasterModuleService;
            _ExceptionService = exceptionService;
        }
        #endregion

        #region API Methods

        /// <summary>
        /// Description - To Insert and Update Role
        /// </summary>
        /// <param name="oRole"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("InsertUpdateRole")]
        public async Task<IActionResult> InsertUpdateRole(MasterRoleEntity oRole)
        {
            try
            {
                DBOperation oResponse = await _MasterRoleService.AddUpdateRole(oRole);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oRole.RoleId > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get Role By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpGet, Route("GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            try
            {
                var oRoleEntity = await _MasterRoleService.GetById(id);
                oRoleEntity.MasterModules = await _MasterModuleService.GetByRoleId(id);

                if (oRoleEntity is not null && oRoleEntity.RoleId > 0)
                    return _ObjectResponse.Create(oRoleEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>

        [HttpGet, Route("GetAllRole")]
        public async Task<IActionResult> GetAllRole()
        {
            try
            {
                var oRoleList = await _MasterRoleService.GetAll();
                if (oRoleList != null)
                    return _ObjectResponse.Create(oRoleList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpGet, Route("GetAllActiveRole")]
        public async Task<IActionResult> GetAllActiveRole()
        {
            try
            {
                var oRoleList = await _MasterRoleService.GetActiveRole();
                if (oRoleList != null)
                    return _ObjectResponse.Create(oRoleList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        /// <summary>
        /// Description - To Delete a Role by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            try
            {
                DBOperation oResponse = await _MasterRoleService.DeleteRole(id);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, "Deleted Successfully");
                else if(oResponse == DBOperation.NotFound)
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.OK, "UserAssigned");
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get Permission Role By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpGet, Route("GetByPermisionRoleUsingRoleId/{id}")]
        public async Task<IActionResult> GetByPermisionRoleUsingRoleId([FromRoute] int id)
        {
            try
            {
               
                dynamic oRoleEntity = await _MasterModuleService.GetByPermisionRoleUsingRoleId(id);

                if (oRoleEntity is not null)
                    return _ObjectResponse.Create(oRoleEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        #endregion

    }
}
