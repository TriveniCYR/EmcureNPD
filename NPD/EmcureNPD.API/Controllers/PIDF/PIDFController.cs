using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Implementation;
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
    public class PIDFController : ControllerBase
    {
        #region Properties

        private readonly IPIDFService _PIDFService;

        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public PIDFController(IPIDFService PIDFService, IResponseHandler<dynamic> ObjectResponse)
        {
            _PIDFService = PIDFService;
            _ObjectResponse = ObjectResponse;
        }

        #endregion Constructor


        /// <summary>
        /// Description - To Get All Formulation
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
        [HttpGet, Route("FillDropdown/{userId}")]
        public async Task<IActionResult> FillDropdown(int userid)
        {
            try
            {
                return _ObjectResponse.CreateData(await _PIDFService.FillDropdown(userid), (Int32)HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }

        /// <summary>
        /// Description - To Get All PIDFList
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
        [HttpPost, Route("GetAllPIDFList")]
        public async Task<IActionResult> GetAllPIDFList([FromForm] DataTableAjaxPostModel model)
        {
            try
            {
                return _ObjectResponse.CreateData(await _PIDFService.GetAllPIDFList(model), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        /// <summary>
        /// Description - To Get Commercial PIDF List
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
        [HttpPost, Route("GetCommonPIDFList")]
        public async Task<IActionResult> GetCommonPIDFList([FromForm] DataTableAjaxPostModel model, [FromQuery] string ScreenName)
        {
            try
            {
                return _ObjectResponse.CreateData(await _PIDFService.GetCommonPIDFList(model, ScreenName), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }        
        /// <summary>
        /// Description - To Insert and Update PIDF
        /// </summary>
        /// <param name="oPIDF"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("InsertUpdatePIDF")]
        public async Task<IActionResult> InsertUpdatePIDF(PIDFEntity oPIDF)
        {
            try
            {
                DBOperation oResponse = await _PIDFService.AddUpdatePIDF(oPIDF);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oPIDF.PIDFID > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get PIDF By Id
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
        [HttpGet, Route("GetPIDFById/{id}")]
        public async Task<IActionResult> GetPIDFById([FromRoute] int id)
        {
            try
            {
                var oPIDFEntity = await _PIDFService.GetById(id);
                if (oPIDFEntity != null && oPIDFEntity.PIDFID > 0)
                    return _ObjectResponse.Create(oPIDFEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpPost]
        [Route("ApproveRejectDeletePidf")]
        public async Task<IActionResult> ApproveRejectDeletePidf(EntryApproveRej oApprRej)
        {
            try
            {
                DBOperation oResponse = await _PIDFService.ApproveRejectDeletePidf(oApprRej);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, ("Save Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }        
        [HttpPost]
        [Route("CommonApproveRejectDeletePidf")]
        public async Task<IActionResult> CommonApproveRejectDeletePidf(EntryApproveRej oApprRej, string ScreenName)
        {
            try
            {
                DBOperation oResponse = await _PIDFService.CommonApproveRejectDeletePidf(oApprRej,ScreenName);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, ("Save Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
    }
}
