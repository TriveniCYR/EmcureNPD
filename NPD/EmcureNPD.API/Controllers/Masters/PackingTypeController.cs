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
    public class PackingTypeController : ControllerBase
    {
        #region Properties

        private readonly IMasterPackingTypeService _MasterPackingTypeService;
        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public PackingTypeController(IConfiguration configuration, IMasterPackingTypeService MasterPackingTypeService, IResponseHandler<dynamic> ObjectResponse)
        {
            _configuration = configuration;
            _MasterPackingTypeService = MasterPackingTypeService;
            _ObjectResponse = ObjectResponse;
        }

        #endregion Constructor

        #region API Methods

        /// <summary>
        /// Description - To Insert and Update PackingType
        /// </summary>
        /// <param name="oPackingType"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("InsertUpdatePackingType")]
        public async Task<IActionResult> InsertUpdatePackingType(MasterPackingTypeEntity oPackingType)
        {
            try
            {
                DBOperation oResponse = await _MasterPackingTypeService.AddUpdatePackingType(oPackingType);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oPackingType.PackingTypeId > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get PackingType By Id
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
        [HttpGet, Route("GetPackingTypeById/{id}")]
        public async Task<IActionResult> GetPackingTypeById([FromRoute] int id)
        {
            try
            {
                var oPackingTypeEntity = await _MasterPackingTypeService.GetById(id);
                if (oPackingTypeEntity != null && oPackingTypeEntity.PackingTypeId > 0)
                    return _ObjectResponse.Create(oPackingTypeEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All PackingType
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
        [HttpGet, Route("GetAllPackingType")]
        public async Task<IActionResult> GetAllPackingType()
        {
            try
            {
                var oPackingTypeList = await _MasterPackingTypeService.GetAll();
                if (oPackingTypeList != null)
                    return _ObjectResponse.Create(oPackingTypeList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Delete a PackingType by Id
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
        [HttpPost("DeletePackingType/{id}")]
        public async Task<IActionResult> DeletePackingType([FromRoute] int id)
        {
            try
            {
                DBOperation oResponse = await _MasterPackingTypeService.DeletePackingType(id);
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