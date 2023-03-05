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
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.IPD
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IPDController : ControllerBase
    {
        #region Properties

        private readonly IIPDService _IPDService;

        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<IPDController> _logger;

        #endregion Properties

        #region Constructor

        public IPDController(IIPDService IPDService, IResponseHandler<dynamic> ObjectResponse, IWebHostEnvironment webHostEnvironment, ILogger<IPDController> logger)
        {
            _IPDService = IPDService;
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
        [HttpGet, Route("GetIPD")]
        public async Task<IActionResult> GetIPD()
        {
            var oFormulationList = await _IPDService.FillDropdown();

            if (oFormulationList != null)
                return _ObjectResponse.Create(oFormulationList, (int)HttpStatusCode.OK);
            else
                return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "No Records found");
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
        public async Task<IActionResult> SaveIPDForm(IPDEntity ipdobj)
        {
            try
            {
                DBOperation oResponse = await _IPDService.AddUpdateIPD(ipdobj);
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
                        oResponse = await _IPDService.ApproveRejectIpdPidf(objApprej);
                    }
                    return _ObjectResponse.Create(true, (int)HttpStatusCode.OK, ipdobj.IPDID > 0 ? "Updated Successfully" : "Inserted Successfully");
                }
                else
                    return _ObjectResponse.Create(false, (int)HttpStatusCode.BadRequest, oResponse == DBOperation.NotFound ? "Record not found" : "Bad request");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
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

                var oPIDFEntity = await _IPDService.GetIPDFormData(pidfId, bussnessId);
                if (oPIDFEntity != null)
                    return _ObjectResponse.Create(oPIDFEntity, (int)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
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
                return _ObjectResponse.CreateData(await _IPDService.GetAllIPDPIDFList(model), (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
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
                var oRegionList = await _IPDService.GetAllRegion(userId);
                if (oRegionList != null)
                    return _ObjectResponse.Create(oRegionList, (int)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpGet, Route("GetCountryRefByRegionIds/{regionIds}")]
        public async Task<IActionResult> GetCountryRefByRegionIds(string regionIds)
        {
            try
            {
                var oRegionList = await _IPDService.GetCountryRefByRegionIds(regionIds);
                if (oRegionList != null)
                    return _ObjectResponse.Create(oRegionList, (int)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpPost]
        [Route("ApproveRejectIpdPidf")]
        public async Task<IActionResult> ApproveRejectIpdPidf(EntryApproveRej oApprRej)
        {
            try
            {
                DBOperation oResponse = await _IPDService.ApproveRejectIpdPidf(oApprRej);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (int)HttpStatusCode.OK, "Save Successfully");
                else
                    return _ObjectResponse.Create(false, (int)HttpStatusCode.BadRequest, oResponse == DBOperation.NotFound ? "Record not found" : "Bad request");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
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
                }
                else if (jsonObject.FileName.HasValues)
                {
                    object[] myarray = jsonObject.FileName.ToObject<object[]>();
                    int count = myarray.Count(s => s != null);
                    model.FileName = new string[count];
                    int i = 0;
                    foreach (var item in myarray)
                    {
                        if (item != null)
                        {
                            var file = item.ToString();
                            model.FileName[i] = "Medical\\" + file;
                            i++;
                        }
                    }
                }
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads\\PIDF\\Medical");
                DBOperation oResponse = await _IPDService.Medical(model, files, path);
                if (oResponse == DBOperation.Success)
                {
                    _logger.LogInformation("IPDService db operation success and PIDMedicalForm controller completed");
                    return _ObjectResponse.Create(true, (int)HttpStatusCode.OK, "Save Successfully");
                }
                else if (oResponse == DBOperation.InvalidFile)
                {
                    _logger.LogInformation("IPDService db operation failed and PIDMedicalForm controller ended");
                    return _ObjectResponse.Create(false, (int)HttpStatusCode.BadRequest, oResponse == DBOperation.InvalidFile ? "File not supported" : "Bad request");
                }
                else
                {
                    _logger.LogInformation("IPDService db operation failed and PIDMedicalForm controller ended");
                    return _ObjectResponse.Create(false, (int)HttpStatusCode.BadRequest, oResponse == DBOperation.NotFound ? "Record not found" : "Bad request");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception occured in PIDMedicalForm controller ended");
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpGet, Route("GetPIDFMedicalFormData/{pidfId}")]
        public async Task<IActionResult> GetPIDFMedicalFormData([FromRoute] long pidfId)
        {
            try
            {
                _logger.LogInformation("GetPIDFMedicalFormData controller started");
                var oPIDFEntity = await _IPDService.GetPIDFMedicalData(pidfId);
                if (oPIDFEntity != null)
                {
                    _logger.LogInformation("_IPDService GetPIDFMedicalData succeeded and GetPIDFMedicalFormData controller completed");
                    return _ObjectResponse.Create(oPIDFEntity, (int)HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogInformation("_IPDService GetPIDFMedicalData failed and GetPIDFMedicalFormData controller ended");
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "Record not found");
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception occured in GetPIDFMedicalFormData controller ended");
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpGet, Route("GetDropdownsForAddDRFTask")]

        public ActionResult GetDropdownsForAddDRFTask()
        {
            var oResponse = _IPDService.GetDropDownsForTask();
            return Ok(oResponse);

        }
    }
}
