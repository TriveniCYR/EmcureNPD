using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.API.Filters;
using EmcureNPD.Business.Models;
using static EmcureNPD.Utility.Enums.GeneralEnum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


namespace EmcureNPD.API.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	[AuthorizeAttribute]
	public class APIController : ControllerBase
	{
		#region Properties

		private readonly IAPIService _APIService;
		private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion Properties

        #region Constructor

        public APIController(IAPIService APIService, IResponseHandler<dynamic> ObjectResponse, IWebHostEnvironment webHostEnvironment)
		{
			_APIService = APIService;
			_ObjectResponse = ObjectResponse;
            _webHostEnvironment= webHostEnvironment;
		}

		#endregion Constructor


        [HttpPost]
        [Consumes("multipart/form-data")]
        [Route("InsertUpdateAPIIPD")]
        public async Task<IActionResult> InsertUpdateAPIIPD([FromForm] IFormCollection oAPIIPD)
        {
            try
            {
                DBOperation oResponse = await _APIService.AddUpdateAPIIPD(oAPIIPD, _webHostEnvironment.WebRootPath);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK,"");
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }        

        [HttpGet, Route("GetAPIIPDFormData/{pidfId}")]
        public async Task<IActionResult> GetAPIIPDFormData([FromRoute] long pidfId)
        {
            try
            {
                var oPIDFEntity = await _APIService.GetAPIIPDFormData(pidfId, _webHostEnvironment.WebRootPath);
                if (oPIDFEntity != null)
                    return _ObjectResponse.Create(oPIDFEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpGet, Route("GetAPICharterSummaryFormData/{pidfId}")]
        public async Task<IActionResult> GetAPICharterSummaryFormData([FromRoute] long pidfId)
        {       
            try
            {
                var oPIDFEntity = await _APIService.GetAPICharterSummaryFormData(pidfId);
                if (oPIDFEntity != null)
                    return _ObjectResponse.Create(oPIDFEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpGet, Route("GetAPICharterFormData/{pidfId}/{IsCharter}")]
        public async Task<IActionResult> GetAPICharterFormData([FromRoute] long pidfId, [FromRoute] short IsCharter)
        {
            try
            {
                var oPIDFEntity = await _APIService.GetAPICharterFormData(pidfId, IsCharter);
                if (oPIDFEntity != null)
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
        [Route("InsertUpdateAPICharter")]
        public async Task<IActionResult> InsertUpdateAPICharter(PIDFAPICharterFormEntity oAPICharter)
        {
            try
            {
                DBOperation oResponse = await _APIService.AddUpdateAPICharter(oAPICharter);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, "Record Insert Successfully");
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpGet, Route("GetAPIRnDFormData/{pidfId}")]
        public async Task<IActionResult> GetAPIRnDFormData([FromRoute] long pidfId)
        {
            try
            {
                var oPIDFEntity = await _APIService.GetAPIRnDFormData(pidfId, _webHostEnvironment.WebRootPath);
                if (oPIDFEntity != null)
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
        [Route("InsertUpdateAPIRnD")]
        public async Task<IActionResult> InsertUpdateAPIRnD(PIDFAPIRnDFormEntity oAPIRnD)
        {
            try
            {
                DBOperation oResponse = await _APIService.AddUpdateAPIRnD(oAPIRnD);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, "Record Insert Successfully");
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
