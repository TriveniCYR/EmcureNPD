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
    public class PackSizeController : ControllerBase
    {
        #region Properties

        private readonly IMasterPackSizeService _MasterPackSizeService;
        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public PackSizeController(IConfiguration configuration, IMasterPackSizeService MasterPackSizeService, IResponseHandler<dynamic> ObjectResponse)
        {
            _configuration = configuration;
            _MasterPackSizeService = MasterPackSizeService;
            _ObjectResponse = ObjectResponse;
        }

        #endregion Constructor

        #region API Methods

        /// <summary>
        /// Description - To Insert and Update PackSize
        /// </summary>
        /// <param name="oPackSize"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("InsertUpdatePackSize")]
        public async Task<IActionResult> InsertUpdatePackSize(MasterPackSizeEntity oPackSize)
        {
            try
            {
                DBOperation oResponse = await _MasterPackSizeService.AddUpdatePackSize(oPackSize);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oPackSize.PackSizeId > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get PackSize By Id
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
        [HttpGet, Route("GetPackSizeById/{id}")]
        public async Task<IActionResult> GetPackSizeById([FromRoute] int id)
        {
            try
            {
                var oPackSizeEntity = await _MasterPackSizeService.GetById(id);
                if (oPackSizeEntity != null && oPackSizeEntity.PackSizeId > 0)
                    return _ObjectResponse.Create(oPackSizeEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All PackSize
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
        [HttpGet, Route("GetAllPackSize")]
        public async Task<IActionResult> GetAllPackSize()
        {
            try
            {
                var oPackSizeList = await _MasterPackSizeService.GetAll();
                if (oPackSizeList != null)
                    return _ObjectResponse.Create(oPackSizeList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Delete a PackSize by Id
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
        [HttpPost("DeletePackSize/{id}")]
        public async Task<IActionResult> DeletePackSize([FromRoute] int id)
        {
            try
            {
                DBOperation oResponse = await _MasterPackSizeService.DeletePackSize(id);
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