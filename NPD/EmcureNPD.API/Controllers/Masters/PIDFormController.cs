using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Implementation;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ServiceImplementations;
using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class PIDFormController : ControllerBase
    {
        #region Properties

        private readonly IPIDFormService _PIDFormService;

        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<PIDFormController> _logger;

        #endregion Properties

        #region Constructor

        public PIDFormController(IPIDFormService PIDFormService, IResponseHandler<dynamic> ObjectResponse, IWebHostEnvironment webHostEnvironment, ILogger<PIDFormController> logger)
        {
            _PIDFormService = PIDFormService;
            _ObjectResponse = ObjectResponse;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
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
        [HttpGet, Route("GetPIDForm")]
        public async Task<IActionResult> GetPIDForm()
        {
            var oFormulationList = await _PIDFormService.FillDropdown();

            if (oFormulationList != null)
                return _ObjectResponse.Create(oFormulationList, (Int32)HttpStatusCode.OK);
            else
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
        }
        /// <summary>
        /// Description - To Insert and Update IPD Form
        /// </summary>
        /// <param name="oIPD"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("SaveIPDForm")]
        public async Task<IActionResult> SaveIPDForm(PIDFormEntity ipdobj)
        {
            try
            {
                DBOperation oResponse = await _PIDFormService.AddUpdateIPD(ipdobj);
                if (oResponse == DBOperation.Success)
                {
                    if (ipdobj.SaveType == "A" || ipdobj.SaveType == "R")
                    {
                        EntryApproveRej objApprej = new EntryApproveRej();
                        objApprej.SaveType = ipdobj.SaveType;
                        ApprRejPidf objList = new ApprRejPidf();
                        objApprej.PidfIds = new List<ApprRejPidf>();
                        objList.pidfId = ipdobj.PIDFID;
                        objApprej.PidfIds.Add(objList);
                        oResponse = await _PIDFormService.ApproveRejectIpdPidf(objApprej);
                    }
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (ipdobj.IPDID > 0 ? "Updated Successfully" : "Inserted Successfully"));
                }
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get IPD Form By Id
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
        [HttpGet, Route("GetIPDFormData/{pidfId}/{bussnessId}")]
        public async Task<IActionResult> GetIPDFormData([FromRoute] long pidfId, int bussnessId)
        {
            try
            {

                var oPIDFEntity = await _PIDFormService.GetIPDFormData(pidfId, bussnessId);
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
        /// <summary>
        /// Description - To Get All IPD PIDFList
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
        [HttpPost, Route("GetAllIPDPIDFList")]
        public async Task<IActionResult> GetAllIPDPIDFList([FromForm] DataTableAjaxPostModel model)
        {
            try
            {
                return _ObjectResponse.CreateData(await _PIDFormService.GetAllIPDPIDFList(model), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All Region Base on User
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
        [HttpGet, Route("GetAllRegion/{userId}")]
        public async Task<IActionResult> GetAllRegion(int userId)
        {
            try
            {
                var oRegionList = await _PIDFormService.GetAllRegion(userId);
                if (oRegionList != null)
                    return _ObjectResponse.Create(oRegionList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpGet, Route("GetCountryRefByRegionIds/{regionIds}")]
        public async Task<IActionResult> GetCountryRefByRegionIds(string regionIds)
        {
            try
            {
                var oRegionList = await _PIDFormService.GetCountryRefByRegionIds(regionIds);
                if (oRegionList != null)
                    return _ObjectResponse.Create(oRegionList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpPost]
        [Route("ApproveRejectIpdPidf")]
        public async Task<IActionResult> ApproveRejectIpdPidf(EntryApproveRej oApprRej)
        {
            try
            {
                DBOperation oResponse = await _PIDFormService.ApproveRejectIpdPidf(oApprRej);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, ("Save Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpPost]
        [Route("PIDMedicalForm")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PIDMedicalForm([FromForm] IFormCollection medicalModel)
        {
            try
            {
                _logger.LogInformation("PIDMedicalForm controller started");
                medicalModel.TryGetValue("Data", out StringValues Data);
                dynamic jsonObject = JsonConvert.DeserializeObject(Data);
                PIDFMedicalViewModel model = new PIDFMedicalViewModel();
                model.Pidfid = jsonObject.Pidfid;
                model.PidfmedicalId = jsonObject.PidfmedicalId;
                model.PidfmedicalFileId = jsonObject.PidfmedicalFileId;
                model.MedicalOpinion = jsonObject.MedicalOpinion;
                model.Remark = jsonObject.Remark;
                model.CreatedBy = jsonObject.CreatedBy;
                var files = medicalModel.Files;
                if (files.Count > 0)
                {
                    model.FileName = new string[files.Count];
                    for (int i = 0; i < files.Count; i++)
                    {
                        var file = files[i];
                        model.FileName[i] = "Medical\\" + file.FileName;
                    }
                    var path = _webHostEnvironment.WebRootPath;
                    var uploadedFile = _PIDFormService.FileUpload(files, path);
                }
                DBOperation oResponse = await _PIDFormService.Medical(model);
                if (oResponse == DBOperation.Success)
				{
                    _logger.LogInformation("PIDFormService db operation success and PIDMedicalForm controller completed");
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, ("Save Successfully"));
                }
				else
				{
                    _logger.LogInformation("PIDFormService db operation failed and PIDMedicalForm controller ended");
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception occured in PIDMedicalForm controller ended");
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpGet, Route("GetPIDFMedicalFormData/{pidfId}")]
        public async Task<IActionResult> GetPIDFMedicalFormData([FromRoute] long pidfId)
        {
            try
            {
                _logger.LogInformation("GetPIDFMedicalFormData controller started");
                var oPIDFEntity = await _PIDFormService.GetPIDFMedicalData(pidfId);
                if (oPIDFEntity != null)
				{
                    _logger.LogInformation("_PIDFormService GetPIDFMedicalData succeeded and GetPIDFMedicalFormData controller completed");
                    return _ObjectResponse.Create(oPIDFEntity, (Int32)HttpStatusCode.OK);
                }
				else
				{
                    _logger.LogInformation("_PIDFormService GetPIDFMedicalData failed and GetPIDFMedicalFormData controller ended");
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "Record not found");
                }
                    
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception occured in GetPIDFMedicalFormData controller ended");
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

    }
}
