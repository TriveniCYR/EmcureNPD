using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.IPD
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class IPDController : ControllerBase
    {
        #region Properties

        private readonly IIPDService _IPDService;

        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<IPDController> _logger;
        private readonly IExceptionService _ExceptionService;

        #endregion Properties

        #region Constructor

        public IPDController(IIPDService IPDService, IResponseHandler<dynamic> ObjectResponse, IWebHostEnvironment webHostEnvironment, ILogger<IPDController> logger, IExceptionService exceptionService)
        {
            _IPDService = IPDService;
            _ObjectResponse = ObjectResponse;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _ExceptionService = exceptionService;
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
        [HttpGet, Route("GetIPD")]
        public async Task<IActionResult> GetIPD()
        {
            var oFormulationList = await _IPDService.FillDropdown();

            if (oFormulationList != null)
                return _ObjectResponse.Create(oFormulationList, (int)HttpStatusCode.OK);
            else
                return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "No Records found");
        }

        /// <summary>
        /// Description - To Insert and Update IPD Form
        /// </summary>
        /// <param name="oIPD"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("SaveIPDForm")]
        public async Task<IActionResult> SaveIPDForm(IPDEntity ipdobj)
        {
            try
            {
                ipdobj.Comments = ipdobj.Comments == null ? "" : ipdobj.Comments;
                DBOperation oResponse = await _IPDService.AddUpdateIPD(ipdobj);
                if (oResponse == DBOperation.Success)
                {
                    if (ipdobj.SaveType == "A" || ipdobj.SaveType == "R")
                    {
                        EntryApproveRej objApprej = new EntryApproveRej();
                        objApprej.SaveType = ipdobj.SaveType;
                        ApprRejPidf objList = new ApprRejPidf();
                        objApprej.PidfIds = new List<ApprRejPidf>();
                        objList.pidfId = ipdobj.PIDFID;
                        objApprej.PidfIds.Add(objList);
                        oResponse = await _IPDService.ApproveRejectIpdPidf(objApprej);
                    }
                    return _ObjectResponse.Create(true, (int)HttpStatusCode.OK, ipdobj.IPDID > 0 ? "Updated Successfully" : "Inserted Successfully");
                }
                else
                    return _ObjectResponse.Create(false, (int)HttpStatusCode.BadRequest, oResponse == DBOperation.NotFound ? "Record not found" : "Bad request");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get IPD Form By Id
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
        [HttpGet, Route("GetIPDFormData/{pidfId}/{bussnessId}")]
        public async Task<IActionResult> GetIPDFormData([FromRoute] long pidfId, int bussnessId)
        {
            try
            {
                var oPIDFEntity = await _IPDService.GetIPDFormData(pidfId, bussnessId);
                if (oPIDFEntity != null)
                    return _ObjectResponse.Create(oPIDFEntity, (int)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All IPD PIDFList
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
        [HttpPost, Route("GetAllIPDPIDFList")]
        public async Task<IActionResult> GetAllIPDPIDFList([FromForm] DataTableAjaxPostModel model)
        {
            try
            {
                return _ObjectResponse.CreateData(await _IPDService.GetAllIPDPIDFList(model), (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All Region Base on User
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
        [HttpGet, Route("GetAllRegion/{userId}")]
        public async Task<IActionResult> GetAllRegion(int userId)
        {
            try
            {
                var oRegionList = await _IPDService.GetAllRegion(userId);
                if (oRegionList != null)
                    return _ObjectResponse.Create(oRegionList, (int)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpGet, Route("GetCountryRefByRegionIds/{regionIds}")]
        public async Task<IActionResult> GetCountryRefByRegionIds(string regionIds)
        {
            try
            {
                var oRegionList = await _IPDService.GetCountryRefByRegionIds(regionIds);
                if (oRegionList != null)
                    return _ObjectResponse.Create(oRegionList, (int)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpPost]
        [Route("ApproveRejectIpdPidf")]
        public async Task<IActionResult> ApproveRejectIpdPidf(EntryApproveRej oApprRej)
        {
            try
            {
                DBOperation oResponse = await _IPDService.ApproveRejectIpdPidf(oApprRej);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (int)HttpStatusCode.OK, "Save Successfully");
                else
                    return _ObjectResponse.Create(false, (int)HttpStatusCode.BadRequest, oResponse == DBOperation.NotFound ? "Record not found" : "Bad request");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpGet, Route("GetCountryByBussinessUnitIds/{BUId}")]
        public async Task<IActionResult> GetCountryByBussinessUnitIds(string BUId)
        {
            try
            {
                var oCountryList = await _IPDService.GetCountryByBussinessUnitIds(BUId);
                if (oCountryList != null)
                    return _ObjectResponse.Create(oCountryList, (int)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

    }
}