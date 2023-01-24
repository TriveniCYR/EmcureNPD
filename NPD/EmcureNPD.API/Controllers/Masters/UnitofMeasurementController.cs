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
    public class UnitofMeasurementController : ControllerBase
    {
        #region Properties

        private readonly IMasterUnitofMeasurementService _MasterUnitofMeasurementService;
        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public UnitofMeasurementController(IConfiguration configuration, IMasterUnitofMeasurementService MasterUnitofMeasurementService, IResponseHandler<dynamic> ObjectResponse)
        {
            _configuration = configuration;
            _MasterUnitofMeasurementService = MasterUnitofMeasurementService;
            _ObjectResponse = ObjectResponse;
        }

        #endregion Constructor

        #region API Methods

        /// <summary>
        /// Description - To Insert and Update UnitofMeasurement
        /// </summary>
        /// <param name="oUnitofMeasurement"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("InsertUpdateUnitofMeasurement")]
        public async Task<IActionResult> InsertUpdateUnitofMeasurement(MasterUnitofMeasurementEntity oUnitofMeasurement)
        {
            try
            {
                DBOperation oResponse = await _MasterUnitofMeasurementService.AddUpdateUnitofMeasurement(oUnitofMeasurement);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oUnitofMeasurement.UnitofMeasurementId > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get UnitofMeasurement By Id
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
        [HttpGet, Route("GetUnitofMeasurementById/{id}")]
        public async Task<IActionResult> GetUnitofMeasurementById([FromRoute] int id)
        {
            try
            {
                var oUnitofMeasurementEntity = await _MasterUnitofMeasurementService.GetById(id);
                if (oUnitofMeasurementEntity != null && oUnitofMeasurementEntity.UnitofMeasurementId > 0)
                    return _ObjectResponse.Create(oUnitofMeasurementEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All UnitofMeasurement
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
        [HttpGet, Route("GetAllUnitofMeasurement")]
        public async Task<IActionResult> GetAllUnitofMeasurement()
        {
            try
            {
                var oUnitofMeasurementList = await _MasterUnitofMeasurementService.GetAll();
                if (oUnitofMeasurementList != null)
                    return _ObjectResponse.Create(oUnitofMeasurementList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Delete a UnitofMeasurement by Id
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
        [HttpPost("DeleteUnitofMeasurement/{id}")]
        public async Task<IActionResult> DeleteUnitofMeasurement([FromRoute] int id)
        {
            try
            {
                DBOperation oResponse = await _MasterUnitofMeasurementService.DeleteUnitofMeasurement(id);
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
