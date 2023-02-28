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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using EmcureNPD.Utility.Utility;

namespace EmcureNPD.API.Controllers.PIDF
{
	[Route("api/[controller]")]
	[ApiController]
	public class PidfFinanceController : ControllerBase
	{
		#region Properties

		private readonly IPidfFinanceService _pidfFinanceService;

		private readonly IResponseHandler<dynamic> _ObjectResponse;
		private readonly IWebHostEnvironment _webHostEnvironment;
		#endregion Properties

		#region Constructor

		public PidfFinanceController(IPidfFinanceService pidfFinanceService, IResponseHandler<dynamic> ObjectResponse, IWebHostEnvironment webHostEnvironment)
		{
			_pidfFinanceService = pidfFinanceService;
			_ObjectResponse = ObjectResponse;
			_webHostEnvironment = webHostEnvironment;
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
				PidfFinanceEntity.Pidfid= UtilityHelper.Decreypt(Convert.ToString(PidfFinanceEntity.Pidfid));
				DBOperation oResponse = await _pidfFinanceService.AddUpdatePidfFinance(PidfFinanceEntity);
				if (oResponse == DBOperation.Success)
					return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (PidfFinanceEntity.PidffinaceId > 0 ? "Updated Successfully" : "Inserted Successfully"));
				else
					return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
			}
			catch (Exception ex)
			{
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
		public async Task<IActionResult> GetFinanceBatchSizeCoating(int PidffinaceId=0)
		{
			try
			{
				
				return _ObjectResponse.CreateData(await _pidfFinanceService.GetFinanceBatchSizeCoating(PidffinaceId), (Int32)HttpStatusCode.OK);
			}
			catch (Exception ex)
			{
				return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
			}
		}


	}
}
