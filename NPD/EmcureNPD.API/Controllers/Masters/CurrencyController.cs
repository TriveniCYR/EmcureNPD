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
    public class CurrencyController : ControllerBase
    {
        #region Properties

        private readonly IMasterCurrencyService _MasterCurrencyService;
        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public CurrencyController(IConfiguration configuration, IMasterCurrencyService MasterCurrencyService, IResponseHandler<dynamic> ObjectResponse)
        {
            _configuration = configuration;
            _MasterCurrencyService = MasterCurrencyService;
            _ObjectResponse = ObjectResponse;
        }

        #endregion Constructor

        #region API Methods

        /// <summary>
        /// Description - To Insert and Update Currency
        /// </summary>
        /// <param name="oCurrency"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("InsertUpdateCurrency")]
        public async Task<IActionResult> InsertUpdateCurrency(MasterCurrencyEntity oCurrency)
        {
            try
            {
                DBOperation oResponse = await _MasterCurrencyService.AddUpdateCurrency(oCurrency);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oCurrency.CurrencyId > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get Currency By Id
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
        [HttpGet, Route("GetCurrencyById/{id}")]
        public async Task<IActionResult> GetCurrencyById([FromRoute] int id)
        {
            try
            {
                var oCurrencyEntity = await _MasterCurrencyService.GetById(id);
                if (oCurrencyEntity != null && oCurrencyEntity.CurrencyId > 0)
                    return _ObjectResponse.Create(oCurrencyEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All Currency
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
        [HttpGet, Route("GetAllCurrency")]
        public async Task<IActionResult> GetAllCurrency()
        {
            try
            {
                var oCurrencyList = await _MasterCurrencyService.GetAll();
                if (oCurrencyList != null)
                    return _ObjectResponse.Create(oCurrencyList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Delete a Currency by Id
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
        [HttpPost("DeleteCurrency/{id}")]
        public async Task<IActionResult> DeleteCurrency([FromRoute] int id)
        {
            try
            {
                DBOperation oResponse = await _MasterCurrencyService.DeleteCurrency(id);
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
        [HttpPost("GetCountryByCurrencyId/{id}")]
        public async Task<IActionResult> GetCountryByCurrencyId([FromRoute] int id)
        {
            try
            {
                var oCountryEntity = await _MasterCurrencyService.GetCountryByCurrencyId(id);
                if (oCountryEntity != null && oCountryEntity.CountryId > 0)
                    return _ObjectResponse.Create(oCountryEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpGet, Route("GetCurrencyByLoggedInUser")]
        public async Task<IActionResult> GetCurrencyByLoggedInUser()
        {
            try
            {
                var oCurrencyList = await _MasterCurrencyService.GetCurrencyByLoggedInUser();
                if (oCurrencyList != null)
                    return _ObjectResponse.Create(oCurrencyList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        #endregion API Methods
    }
}