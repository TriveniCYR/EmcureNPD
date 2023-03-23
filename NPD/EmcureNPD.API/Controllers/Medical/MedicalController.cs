using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.IPD
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicalController : ControllerBase
    {
        #region Properties

        private readonly IMedicalService _MedicalService;

        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<MedicalController> _logger;

        #endregion Properties

        #region Constructor

        public MedicalController(IMedicalService MedicalService, IResponseHandler<dynamic> ObjectResponse, IWebHostEnvironment webHostEnvironment, ILogger<MedicalController> logger)
        {
            _MedicalService = MedicalService;
            _ObjectResponse = ObjectResponse;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        #endregion Constructor
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
                if (files.Count > 0 && jsonObject.FileName.HasValues)
                {
					object[] myarray = jsonObject.FileName.ToObject<object[]>();
					int count = myarray.Count(s => s != null);
                    int totalCount = count + files.Count;
					model.FileName = new string[totalCount];
                    int i = 0;
                    for (i = 0; i < files.Count; i++)
                    {
                        var file = files[i];
                        model.FileName[i] = "Medical\\" + file.FileName;
                    }
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
                else if(files.Count > 0)
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
                DBOperation oResponse = await _MedicalService.Medical(model, files, path);
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
                    return _ObjectResponse.Create(false, (int)HttpStatusCode.BadRequest, oResponse == DBOperation.Error ? "Please select at least one file" : "Bad request");
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
                var oPIDFEntity = await _MedicalService.GetPIDFMedicalData(pidfId);
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

    }
}
