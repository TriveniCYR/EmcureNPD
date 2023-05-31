using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Numerics;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class BatchSizeNumberController : ControllerBase
    {
        #region Properties

        private readonly IMasterBatchSizeNumberService _MasterBatchSizeNumberService;
        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public BatchSizeNumberController(IConfiguration configuration, IMasterBatchSizeNumberService MasterBatchSizeNumberService, IResponseHandler<dynamic> ObjectResponse)
        {
            _configuration = configuration;
            _MasterBatchSizeNumberService = MasterBatchSizeNumberService;
            _ObjectResponse = ObjectResponse;
        }

        #endregion Constructor

        #region API Methods

        /// <summary>
        /// Description - To Insert and Update BatchSizeNumber
        /// </summary>
        /// <param name="oBatchSizeNumber"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("InsertUpdateBatchSizeNumber")]
        public async Task<IActionResult> InsertUpdateBatchSizeNumber(MasterBatchSizeNumberEntity oBatchSizeNumber)
        {
            try
            {
                DBOperation oResponse = await _MasterBatchSizeNumberService.AddUpdateBatchSizeNumber(oBatchSizeNumber);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oBatchSizeNumber.BatchSizeNumberId > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else if (oResponse == DBOperation.AlreadyExist)
                { return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.AlreadyExist ? "BatchSize Number Name'<b>" + oBatchSizeNumber.BatchSizeNumberName + "</b>' Already Exist" : "Bad request")); }
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get BatchSizeNumber By Id
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
        [HttpGet, Route("GetBatchSizeNumberById/{id}")]
        public async Task<IActionResult> GetBatchSizeNumberById([FromRoute] int id)
        {
            try
            {
                var oBatchSizeNumberEntity = await _MasterBatchSizeNumberService.GetById(id);
                if (oBatchSizeNumberEntity != null && oBatchSizeNumberEntity.BatchSizeNumberId > 0)
                    return _ObjectResponse.Create(oBatchSizeNumberEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All BatchSizeNumber
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
        [HttpGet, Route("GetAllBatchSizeNumber")]
        public async Task<IActionResult> GetAllBatchSizeNumber()
        {
            try
            {
                var oBatchSizeNumberList = await _MasterBatchSizeNumberService.GetAll();
                if (oBatchSizeNumberList != null)
                    return _ObjectResponse.Create(oBatchSizeNumberList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Delete a BatchSizeNumber by Id
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
        [HttpPost("DeleteBatchSizeNumber/{id}")]
        public async Task<IActionResult> DeleteBatchSizeNumber([FromRoute] int id)
        {
            try
            {
                DBOperation oResponse = await _MasterBatchSizeNumberService.DeleteBatchSizeNumber(id);
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