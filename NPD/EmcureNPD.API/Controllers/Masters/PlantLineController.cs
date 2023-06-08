using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
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
	public class PlantLineController : ControllerBase
	{
		#region Properties

		private readonly IMasterPlantLine _MasterPlantLine;
		private readonly IConfiguration _configuration;
		private readonly IResponseHandler<dynamic> _ObjectResponse;

		#endregion Properties

		#region Constructor

		public PlantLineController(IConfiguration configuration, IMasterPlantLine masterPlantLine, IResponseHandler<dynamic> ObjectResponse)
		{
			_configuration = configuration;
			_MasterPlantLine = masterPlantLine;
			_ObjectResponse = ObjectResponse;
		}

		#endregion Constructor

		#region API Methods

		/// <summary>
		/// Description - To Insert and Update PlantLine
		/// </summary>
		/// <param name="oPlantLine"></param>
		/// <returns></returns>
		/// <response code="200">OK</response>
		/// <response code="400">Bad Request</response>
		/// <response code="403">Forbidden</response>
		/// <response code="404">Not Found</response>
		/// <response code="405">Method Not Allowed</response>
		/// <response code="500">Internal Server</response>
		[HttpPost]
		[Route("AddUpdatePlantLine")]
		public async Task<IActionResult> AddUpdatePlantLine(MasterPlantLineEntity oPlantLine)
		{
			try
			{
				DBOperation oResponse = await _MasterPlantLine.AddUpdatePlantLine(oPlantLine);
				if (oResponse == DBOperation.Success)
					return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oPlantLine.LineId > 0 ? "Updated Successfully" : "Inserted Successfully"));
				else if (oResponse == DBOperation.AlreadyExist)
				{ return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.AlreadyExist ? "Plant Line Name'<b>" + oPlantLine.LineName + "</b>' Already Exist" : "Bad request")); }
				else
					return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
			}
			catch (Exception ex)
			{
				return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
			}
		}

		/// <summary>
		/// Description - To Get Plant Line By Id 
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
		[HttpGet, Route("PlantLineById/{id}")]
		public async Task<IActionResult> GetById([FromRoute] long id)
		{
			try
			{
				var oPlantLineEntity = await _MasterPlantLine.GetById(id);
				if (oPlantLineEntity != null && oPlantLineEntity.LineId > 0)
					return _ObjectResponse.Create(oPlantLineEntity, (Int32)HttpStatusCode.OK);
				else
					return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
			}
			catch (Exception ex)
			{
				return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
			}
		}

		/// <summary>
		/// Description - To Get All Plant Line
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
		[HttpGet, Route("GetAllPlantLine")]
		public async Task<IActionResult> GetAllPlantLine()
		{
			try
			{
				var oPlantLineList = await _MasterPlantLine.GetAll();
				if (oPlantLineList != null)
					return _ObjectResponse.Create(oPlantLineList, (Int32)HttpStatusCode.OK);
				else
					return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
			}
			catch (Exception ex)
			{
				return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
			}
		}
		/// <summary>
		/// Description - To Get All Active Plants
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
		[HttpGet, Route("GetAllActivePlants")]
		public async Task<IActionResult> GetAllActivePlants()
		{
			try
			{
				var oPlantLineList = await _MasterPlantLine.GetAllActivePlants();
				if (oPlantLineList != null)
					return _ObjectResponse.Create(oPlantLineList, (Int32)HttpStatusCode.OK);
				else
					return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
			}
			catch (Exception ex)
			{
				return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
			}
		}

		/// <summary>
		/// Description - To Delete a Plant Line by Id
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
		[HttpPost("DeletePlantLine/{id}")]
		public async Task<IActionResult> DeleteCurrency([FromRoute] int id)
		{
			try
			{
				DBOperation oResponse = await _MasterPlantLine.DeletePlantLine(id);
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
