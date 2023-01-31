using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.Implementation;
using EmcureNPD.API.Filters;
using EmcureNPD.Business.Models;
using static EmcureNPD.Utility.Enums.GeneralEnum;
using EmcureNPD.Data.DataAccess.Entity;

namespace EmcureNPD.API.Controllers.PBF
{
	[Route("api/[controller]")]
	[ApiController]
	[AuthorizeAttribute]
	public class PBFController : ControllerBase
	{
		#region Properties

		private readonly IPBFService _PBFService;

		private readonly IResponseHandler<dynamic> _ObjectResponse;

		#endregion Properties

		#region Constructor

		public PBFController(IPBFService PBFService, IResponseHandler<dynamic> ObjectResponse)
		{
			_PBFService = PBFService;
			_ObjectResponse = ObjectResponse;
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
		[HttpGet, Route("FillDropdown")]
		public async Task<IActionResult> FillDropdown()
		{
			try
			{
				return _ObjectResponse.CreateData(await _PBFService.FillDropdown(), (Int32)HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
			}
		}
		/// <summary>
		/// Description - To Insert and Update PIDF
		/// </summary>
		/// <param name="oPIDF"></param>
		/// <returns></returns>
		/// <response code="200">OK</response>
		/// <response code="400">Bad Request</response>
		/// <response code="403">Forbidden</response>
		/// <response code="404">Not Found</response>
		/// <response code="405">Method Not Allowed</response>
		/// <response code="500">Internal Server</response>
		[HttpPost]
		[Route("InsertUpdatePBF")]
		public async Task<IActionResult> InsertUpdatePBF(PidfPbfEntity pbfEntity)
		{
			try
			{
				DBOperation oResponse = await _PBFService.AddUpdatePBF(pbfEntity);
				if (oResponse == DBOperation.Success)
					return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (pbfEntity.Pidfpbfid > 0 ? "Updated Successfully" : "Inserted Successfully"));
				else
					return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
			}
			catch (Exception ex)
			{
				return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
			}
		}

		[HttpGet, Route("GetPbfFormData/{pidfId}/{bussnessId}/{strengthid}")]
		public async Task<IActionResult> GetPbfFormData([FromRoute] long pidfId, int bussnessId, int? strengthid)
		{
			try
			{
				//pidfId = int.Parse(UtilityHelper.Decreypt(strpidfId));
				var oPIDFEntity = await _PBFService.GetPbfFormData(pidfId, bussnessId, strengthid);
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
        [Route("InsertUpdateAPIIPD")]
        public async Task<IActionResult> InsertUpdateAPIIPD(PIDFAPIIPDFormEntity oAPIIPD)
        {
            try
            {
                DBOperation oResponse = await _PBFService.AddUpdateAPIIPD(oAPIIPD);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oAPIIPD.APIIPDDetailsFormID > 0 ? "Updated Successfully" : "Inserted Successfully"));
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
                var oPIDFEntity = await _PBFService.GetAPIIPDFormData(pidfId);
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
    }
}
