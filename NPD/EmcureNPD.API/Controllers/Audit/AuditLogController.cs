using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Implementation;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EmcureNPD.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuditLogController : ControllerBase
    {
        #region Properties

        private readonly IMasterAuditLogService _MasterAuditLogService;

        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public AuditLogController(IMasterAuditLogService AuditLogService, IResponseHandler<dynamic> ObjectResponse)
        {

            _MasterAuditLogService = AuditLogService;
            _ObjectResponse = ObjectResponse;
        }
        #endregion Constructor

        /// <summary>
        /// Description - To Get Formulation By Id
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
        [HttpGet, Route("GetAuditLogById/{id}/{moduleId}")]
        public async Task<IActionResult> GetAuditLogById([FromRoute] int id, [FromRoute] int moduleId)
        {
            try
            {

                var oAuditLogEntity = await _MasterAuditLogService.GetByModuleId(id, moduleId);
                if (oAuditLogEntity != null)
                    return _ObjectResponse.Create(oAuditLogEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All AuditLog
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
        [HttpPost, Route("GetAllAuditLog")]
        public async Task<IActionResult> GetAllAuditLog([FromForm] DataTableAjaxPostModel model)
        {
            try
            {                
                return _ObjectResponse.CreateData(await _MasterAuditLogService.GetAll(model), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
    }
}