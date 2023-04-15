using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class DashboardController : ControllerBase
    {
        #region Properties

        private readonly IMasterDIAService _MasterDIAService;
        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public DashboardController(IConfiguration configuration, IMasterDIAService MasterDIAService, IResponseHandler<dynamic> ObjectResponse)
        {
            _configuration = configuration;
            _MasterDIAService = MasterDIAService;
            _ObjectResponse = ObjectResponse;
        }

        #endregion Constructor

        #region API Methods

        /// <summary>
        /// Description - To Insert and Update DIA
        /// </summary>
        /// <param name="oDIA"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("InsertUpdateDIA")]
        public async Task<IActionResult> InsertUpdateDIA(MasterDIAEntity oDIA)
        {
            try
            {
                DBOperation oResponse = await _MasterDIAService.AddUpdateDIA(oDIA);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oDIA.DIAId > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get DIA By Id
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
        [HttpGet, Route("GetDIAById/{id}")]
        public async Task<IActionResult> GetDIAById([FromRoute] int id)
        {
            try
            {
                var oDIAEntity = await _MasterDIAService.GetById(id);
                if (oDIAEntity != null && oDIAEntity.DIAId > 0)
                    return _ObjectResponse.Create(oDIAEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All DIA
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
        [HttpGet, Route("GetAllDIA")]
        public async Task<IActionResult> GetAllDIA()
        {
            try
            {
                var oDIAList = await _MasterDIAService.GetAll();
                if (oDIAList != null)
                    return _ObjectResponse.Create(oDIAList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Delete a DIA by Id
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
        [HttpPost("DeleteDIA/{id}")]
        public async Task<IActionResult> DeleteDIA([FromRoute] int id)
        {
            try
            {
                DBOperation oResponse = await _MasterDIAService.DeleteDIA(id);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, "Deleted Successfully");
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        #endregion API Methods
    }
}