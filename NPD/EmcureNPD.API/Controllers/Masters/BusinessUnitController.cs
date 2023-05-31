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
    public class BusinessUnitController : ControllerBase
    {
        #region Properties

        private readonly IMasterBusinessUnitService _MasterBusinessUnitService;
        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public BusinessUnitController(IConfiguration configuration, IMasterBusinessUnitService MasterBusinessUnitService, IResponseHandler<dynamic> ObjectResponse)
        {
            _configuration = configuration;
            _MasterBusinessUnitService = MasterBusinessUnitService;
            _ObjectResponse = ObjectResponse;
        }

        #endregion Constructor

        #region API Methods

        /// <summary>
        /// Description - To Insert and Update BusinessUnit
        /// </summary>
        /// <param name="oBusinessUnit"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("InsertUpdateBusinessUnit")]
        public async Task<IActionResult> InsertUpdateBusinessUnit(MasterBusinessUnitEntity oBusinessUnit)
        {
            try
            {
                DBOperation oResponse = await _MasterBusinessUnitService.AddUpdateBusinessUnit(oBusinessUnit);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oBusinessUnit.BusinessUnitId > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else if(oResponse== DBOperation.AlreadyExist)
                { return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.AlreadyExist ? "Business Unit Name '<b>" + oBusinessUnit.BusinessUnitName+ "</b>' Already Exist" : "Bad request")); }
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get BusinessUnit By Id
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
        [HttpGet, Route("GetBusinessUnitById/{id}")]
        public async Task<IActionResult> GetBusinessUnitById([FromRoute] int id)
        {
            try
            {
                var oBusinessUnitEntity = await _MasterBusinessUnitService.GetById(id);
                if (oBusinessUnitEntity != null && oBusinessUnitEntity.BusinessUnitId > 0)
                    return _ObjectResponse.Create(oBusinessUnitEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All BusinessUnit
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
        [HttpGet, Route("GetAllBusinessUnit")]
        public async Task<IActionResult> GetAllBusinessUnit()
        {
            try
            {
                var oBusinessUnitList = await _MasterBusinessUnitService.GetAll();
                if (oBusinessUnitList != null)
                    return _ObjectResponse.Create(oBusinessUnitList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpGet, Route("GetActiveBusinessUnit")]
        public IActionResult GetActiveBusinessUnit()
        {
            try
            {
                var oBusinessUnitList = _MasterBusinessUnitService.GetActiveBusinessUnit();
                if (oBusinessUnitList != null)
                    return _ObjectResponse.Create(oBusinessUnitList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
		[HttpGet, Route("GetActiveEncryptedBusinessUnit")]
		public IActionResult GetActiveEncryptedBusinessUnit()
		{
			try
			{
				var oBusinessUnitList = _MasterBusinessUnitService.GetActiveEncryptedBusinessUnit();
				if (oBusinessUnitList != null)
					return _ObjectResponse.Create(oBusinessUnitList, (Int32)HttpStatusCode.OK);
				else
					return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
			}
			catch (Exception ex)
			{
				return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
			}
		}
		/// <summary>
		/// Description - To Delete a BusinessUnit by Id
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
		[HttpPost("DeleteBusinessUnit/{id}")]
        public async Task<IActionResult> DeleteBusinessUnit([FromRoute] int id)
        {
            try
            {
                DBOperation oResponse = await _MasterBusinessUnitService.DeleteBusinessUnit(id);
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

        /// <summary>
        /// Description - Get Region By Business Id
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
        [HttpPost("GetRegionByBusinessUnitId/{id}")]
        public async Task<IActionResult> GetRegionByBusinessUnitId([FromRoute] int id)
        {
            try
            {
                var oCountryEntity = await _MasterBusinessUnitService.GetRegionByBusinessUnitId(id);
                if (oCountryEntity != null && oCountryEntity.RegionId > 0)
                    return _ObjectResponse.Create(oCountryEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - Get Country By Business Id
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
        [HttpGet("GetCountryByBusinessUnitId/{BusinessUnitId}")]
        public async Task<IActionResult> GetCountryByBusinessUnitId([FromRoute] int BusinessUnitId)
        {
            try
            {
                var oCountryEntity = await _MasterBusinessUnitService.GetCountryByBusinessUnitId(BusinessUnitId);
                if (oCountryEntity != null)
                    return _ObjectResponse.Create(oCountryEntity, (Int32)HttpStatusCode.OK);
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