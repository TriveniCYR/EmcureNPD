using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.PBF
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class PBFController : ControllerBase
    {
        #region Properties

        private readonly IPBFService _PBFService;
        private readonly IPidfProductStrengthService _IPidfProductStrengthService;
        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IExceptionService _ExceptionService;

        #endregion Properties

        #region Constructor

        public PBFController(IPBFService PBFService, IResponseHandler<dynamic> ObjectResponse, IWebHostEnvironment webHostEnvironment, IPidfProductStrengthService IPidfProductStrengthService, IExceptionService exceptionService)
        {
            _PBFService = PBFService;
            _ObjectResponse = ObjectResponse;
            _webHostEnvironment = webHostEnvironment;
            _IPidfProductStrengthService = IPidfProductStrengthService;
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
        [HttpGet, Route("FillDropdown/{PIDFId}")]
        public async Task<IActionResult> FillDropdown(int PIDFId)
        {
            try
            {
                return _ObjectResponse.CreateData(await _PBFService.FillDropdown(PIDFId), (Int32)HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                await _ExceptionService.LogException(e);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }

        [HttpGet, Route("GetPbfFormDetails/{pidfId}/{bussnessId}/{strengthid}")]
        public async Task<IActionResult> GetPbfFormDetails([FromRoute] long pidfId, int bussnessId, int strengthid)
        {
            try
            {
                //pidfId = int.Parse(UtilityHelper.Decreypt(strpidfId));
                var oPIDFEntity = await _PBFService.GetPbfFormDetails(pidfId, bussnessId, strengthid);
                if (oPIDFEntity != null)
                    return _ObjectResponse.Create(oPIDFEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpPost]
        [Route("InsertUpdatePBFDetails")]
        public async Task<IActionResult> InsertUpdatePBFDetails(PBFFormEntity pbfEntity)
        {
            try
            {
                DBOperation oResponse = await _PBFService.AddUpdatePBFDetails(pbfEntity);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (pbfEntity.Pidfpbfid > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpGet, Route("PBFTabDetails/{PIDFId}/{BUId}")]
        public async Task<IActionResult> FillDropdown(int PIDFId, int BUId)
        {
            try
            {
                return _ObjectResponse.CreateData(await _PBFService.PBFAllTabDetails(PIDFId, BUId), (Int32)HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                await _ExceptionService.LogException(e);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }
    }
}