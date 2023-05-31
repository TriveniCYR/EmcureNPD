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
    public class FormulationController : ControllerBase
    {
        #region Properties

        private readonly IMasterFormulationService _MasterFormulationService;
        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public FormulationController(IConfiguration configuration, IMasterFormulationService MasterFormulationService, IResponseHandler<dynamic> ObjectResponse)
        {
            _configuration = configuration;
            _MasterFormulationService = MasterFormulationService;
            _ObjectResponse = ObjectResponse;
        }

        #endregion Constructor

        #region API Methods

        /// <summary>
        /// Description - To Insert and Update Formulation
        /// </summary>
        /// <param name="oFormulation"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("InsertUpdateFormulation")]
        public async Task<IActionResult> InsertUpdateFormulation(MasterFormulationEntity oFormulation)
        {
            try
            {
                DBOperation oResponse = await _MasterFormulationService.AddUpdateFormulation(oFormulation);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (oFormulation.FormulationId > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else if (oResponse == DBOperation.AlreadyExist)
                { return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.AlreadyExist ? "Formulation Name'<b>" + oFormulation.FormulationName + "</b>' Already Exist" : "Bad request")); }
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get Formulation By Id
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
        [HttpGet, Route("GetFormulationById/{id}")]
        public async Task<IActionResult> GetFormulationById([FromRoute] int id)
        {
            try
            {
                var oFormulationEntity = await _MasterFormulationService.GetById(id);
                if (oFormulationEntity != null && oFormulationEntity.FormulationId > 0)
                    return _ObjectResponse.Create(oFormulationEntity, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

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
        [HttpGet, Route("GetAllFormulation")]
        public async Task<IActionResult> GetAllFormulation()
        {
            var oFormulationList = await _MasterFormulationService.GetAll();
            if (oFormulationList != null)
                return _ObjectResponse.Create(oFormulationList, (Int32)HttpStatusCode.OK);
            else
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
        }

        /// <summary>
        /// Description - To Delete a Formulation by Id
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
        [HttpPost("DeleteFormulation/{id}")]
        public async Task<IActionResult> DeleteFormulation([FromRoute] int id)
        {
            try
            {
                DBOperation oResponse = await _MasterFormulationService.DeleteFormulation(id);
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