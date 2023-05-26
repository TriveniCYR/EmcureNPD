using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.PIDF
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class PidfFinanceController : ControllerBase
    {
        #region Properties

        private readonly IPidfFinanceService _pidfFinanceService;
        private readonly IPidfProductStrengthService _pidfProductStrengthService;
        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IExceptionService _ExceptionService;

        #endregion Properties

        #region Constructor

        public PidfFinanceController(IPidfFinanceService pidfFinanceService, IResponseHandler<dynamic> ObjectResponse, IWebHostEnvironment webHostEnvironment, IExceptionService exceptionService, IPidfProductStrengthService pidfProductStrengthService)
        {
            _pidfFinanceService = pidfFinanceService;
            _ObjectResponse = ObjectResponse;
            _webHostEnvironment = webHostEnvironment;
            _ExceptionService = exceptionService;
            _pidfProductStrengthService = pidfProductStrengthService;
        }

        #endregion Constructor

        /// <summary>
        /// Description - To Insert and Update PidfFinance
        /// </summary>
        /// <param name="oPidfFinance"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("AddUpdatePidfFinance")]
        public async Task<IActionResult> AddUpdatePidfFinance(FinanceModel PidfFinanceEntity)
        {
            try
            {
                PidfFinanceEntity.Pidfid = UtilityHelper.Decreypt(Convert.ToString(PidfFinanceEntity.Pidfid));
                DBOperation oResponse = await _pidfFinanceService.AddUpdatePidfFinance(PidfFinanceEntity);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (PidfFinanceEntity.PidffinaceId > 0 ? "Updated Successfully" : "Inserted Successfully"));
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
        /// Description - To Get PidfFinance
        /// </summary>
        /// <param name="oGetPidfFinance"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response> 
        [HttpGet]
        [Route("GetPidfFinance/{Pidfid}")]
        public async Task<IActionResult> GetPidfFinance(string Pidfid)
        {
            try
            {
                Pidfid = UtilityHelper.Decreypt(Convert.ToString(Pidfid));
                return _ObjectResponse.CreateData(await _pidfFinanceService.GetPidfFinance(Convert.ToInt32(Pidfid)), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }

        /// <summary>
        /// Description - To Get GetFinanceBatchSizeCoating
        /// </summary>
        /// <param name="oGetFinanceBatchSizeCoating"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response> 
        [HttpGet]
        [Route("GetFinanceBatchSizeCoating/{PidffinaceId}")]
        public async Task<IActionResult> GetFinanceBatchSizeCoating(int PidffinaceId = 0)
        {
            try
            {
                return _ObjectResponse.CreateData(await _pidfFinanceService.GetFinanceBatchSizeCoating(PidffinaceId), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }
        [HttpGet]
        [Route("GetManagmentApprovalBatchSizeCoating/{PidffinaceId}")]
        public async Task<IActionResult> GetManagmentApprovalBatchSizeCoating(int PidffinaceId = 0)
        {
            try
            {
                return _ObjectResponse.CreateData(await _pidfFinanceService.GetManagmentApprovalBatchSizeCoating(PidffinaceId), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }
        /// <summary>
        /// Description - To Get GetStrengthByPIDFId
        /// </summary>
        /// <param name="oGetStrengthByPIDFId"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpGet]
        [Route("GetStrengthByPIDFId/{Pidfid}")]
        public async Task<IActionResult> GetStrengthByPIDFId(string Pidfid)
        {
            try
            {
                Pidfid = UtilityHelper.Decreypt(Convert.ToString(Pidfid));
                return _ObjectResponse.CreateData(await _pidfProductStrengthService.GetStrengthByPIDFId(Convert.ToInt32(Pidfid)), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }
        /// <summary>
        /// Description - To Get GetFinaceProjectionYear
        /// </summary>
        /// <param name="oGetFinaceProjectionYear"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpGet]
        [Route("GetFinaceProjectionYear/{monthTobeDeduct}")]
        public async Task<IActionResult> GetFinaceProjectionYear(int monthTobeDeduct)
        {
            try
            {
               
                return _ObjectResponse.CreateData(await _pidfFinanceService.GetFinaceProjectionYear(monthTobeDeduct), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }

        /// <summary>
        /// Description - To Get GetStrengthByPIDFAnddBuId
        /// </summary>
        /// <param name="oGetStrengthByPIDFAnddBuId"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpGet]
        [Route("GetStrengthByPIDFAnddBuId/{pidfid}/{buid}")]
        public async Task<IActionResult> GetStrengthByPIDFAnddBuId(string pidfid, string buid)
        {
            try
            {
                pidfid = UtilityHelper.Decreypt(Convert.ToString(pidfid));
                buid = UtilityHelper.Decreypt(Convert.ToString(buid));
                return _ObjectResponse.CreateData(await _pidfProductStrengthService.GetStrengthByPIDFAnddBuId(Convert.ToInt32(pidfid), Convert.ToInt32(buid)), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }
        /// <summary>
        /// Description - To Get GetPackSizeByStrengthId
        /// </summary>
        /// <param name="oGetPackSizeByStrengthId"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpGet]
        [Route("GetPackSizeByStrengthId/{pidfid}/{buid}/{StrengthId}")]
        public async Task<IActionResult> GetPackSizeByStrengthId(string pidfid, string buid,int StrengthId)
        {
            try
            {
                pidfid = UtilityHelper.Decreypt(Convert.ToString(pidfid));
                buid = UtilityHelper.Decreypt(Convert.ToString(buid));
                return _ObjectResponse.CreateData(await _pidfFinanceService.GetPackSizeByStrengthId(Convert.ToInt32(pidfid), Convert.ToInt32(buid), StrengthId), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }
        /// <summary>
        /// Description - To Get GetPackSizeByStrengthId
        /// </summary>
        /// <param name="oGetPackSizeByStrengthId"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpGet]
        [Route("GetSUIMSVolumeYearWiseByPackSize/{pidfid}/{buid}/{StrengthId}/{PackSize}")]
        public async Task<IActionResult> GetSUIMSVolumeYearWiseByPackSize(string pidfid, string buid, int StrengthId,int PackSize)
        {
            try
            {
                pidfid = UtilityHelper.Decreypt(Convert.ToString(pidfid));
                buid = UtilityHelper.Decreypt(Convert.ToString(buid));
                return _ObjectResponse.CreateData(await _pidfFinanceService.GetSUIMSVolumeYearWiseByPackSize(Convert.ToInt32(pidfid), Convert.ToInt32(buid), StrengthId, PackSize), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }
    }
}