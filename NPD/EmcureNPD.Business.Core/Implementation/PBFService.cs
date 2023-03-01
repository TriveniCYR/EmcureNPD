using Microsoft.Extensions.Configuration;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Core.ServiceImplementations;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using AutoMapper.Configuration;
using System.Data.SqlClient;
using EmcureNPD.Utility.Enums;

namespace EmcureNPD.Business.Core.Implementation
{
    public class PBFService : IPBFService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;

        private readonly IMasterOralService _oralService;
        private readonly IMasterUnitofMeasurementService _unitofMeasurementService;
        private readonly IMasterDosageFormService _dosageFormService;
        private readonly IMasterPackagingTypeService _packagingTypeService;
        private readonly IMasterBusinessUnitService _businessUnitService;
        private readonly IMasterCountryService _countryService;
        private readonly IMasterAPISourcingService _APISourcingService;
        private readonly IPidfApiDetailsService _PidfApiDetailsService;
        private readonly IPidfProductStrengthService _pidfProductStrengthService;
        private readonly IMasterDIAService _masterDIAService;
        private readonly IMasterMarketExtensionService _masterMarketExtensionService;
        private readonly IMasterBERequirementService _masterBERequirementService;
        private readonly IMasterProductTypeService _masterProductTypeService;
        private readonly IMasterPlantService _masterPlantService;
        private readonly IMasterWorkflowService _masterWorkflowService;
        private readonly IMasterFormRNDDivisionService _masterFormRNDDivisionService;
        private readonly IMasterFormulationService _masterFormulationService;
        private readonly IMasterAnalyticalGLService _masterAnalyticalGLService;
        private readonly IPidfProductStrengthService _productStrengthService;
        private readonly IMasterTestTypeService _masterTestTypeService;
        private readonly IMasterTestLicenseService _masterTestLicenseService;

        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private IRepository<PidfApiIpd> _pidf_API_IPD_repository { get; set; }
        private IRepository<PidfApiRnD> _pidf_API_RnD_repository { get; set; }
        private IRepository<PidfApiCharter> _pidf_API_Charter_repository { get; set; }
        private IRepository<PidfApiCharterTimelineInMonth> _pidf_API_TimelineInMonth_repository { get; set; }
        private IRepository<PidfPbf> _pbfRepository { get; set; }

        private readonly IMasterAuditLogService _auditLogService;

        private IRepository<Pidf> _repository { get; set; }

        private IRepository<Pidfapidetail> _pidfApiRepository { get; set; }
        private IRepository<PidfPbfAnalytical> _pidfPbfAnalyticalRepository { get; set; }
        private IRepository<PidfPbfAnalyticalPrototype> _pidfPbfAnalyticalPrototypeRepository { get; set; }
        private IRepository<PidfPbfAnalyticalScaleUp> _pidfPbfAnalyticalScaleUpRepository { get; set; }
        private IRepository<PidfPbfAnalyticalExhibit> _pidfPbfAnalyticalExhibitRepository { get; set; }
        private IRepository<PidfPbfAnalyticalCost> _pidfPbfAnalyticalCostRepository { get; set; }
        private IRepository<PidfPbfClinical> _pidfPbfClinicalRepository { get; set; }
        private IRepository<PidfPbfClinicalPilotBioFasting> _pidfPbfClinicalPilotBioFastingRepository { get; set; }
        private IRepository<PidfPbfClinicalPilotBioFed> _pidfPbfClinicalPilotBioFedRepository { get; set; }
        private IRepository<PidfPbfClinicalPivotalBioFasting> _pidfPbfClinicalPivotalBioFastingRepository { get; set; }
        private IRepository<PidfPbfClinicalPivotalBioFed> _pidfPbfClinicalPivotalBioFedRepository { get; set; }
        private IRepository<PidfPbfClinicalCost> _pidfPbfClinicalCostRepository { get; set; }

        private IRepository<MasterDosage> _masterDosageRepository { get; set; }
        private IRepository<PidfproductStrength> _pidfProductStrength { get; set; }
        private readonly IHelper _helper;

        //Market Extension & In House

        public PBFService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IMasterOralService oralService, IMasterUnitofMeasurementService unitofMeasurementService,
            IMasterDosageFormService dosageFormService, IMasterPackagingTypeService packagingTypeService, IMasterBusinessUnitService businessUnitService,
            IMasterCountryService countryService, IMasterAPISourcingService masterAPISourcingService, IPidfApiDetailsService pidfApiDetailsService,
            IPidfProductStrengthService pidfProductStrengthService, IMasterDIAService masterDium, IMasterMarketExtensionService masterMarketExtensionService,
            IMasterBERequirementService masterBERequirementService, IMasterProductTypeService masterProductTypeService, IMasterPlantService masterPlantService,
            IMasterWorkflowService masterWorkflowService, IMasterFormRNDDivisionService masterFormRNDDivisionService, IMasterFormulationService masterFormulationService,
            IMasterAnalyticalGLService masterAnalyticalGLService, IPidfProductStrengthService productStrengthService, Microsoft.Extensions.Configuration.IConfiguration configuration,
            IMasterTestTypeService masterTestTypeService, IMasterTestLicenseService masterTestLicenseService, IMasterAuditLogService auditLogService, IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _oralService = oralService;
            _unitofMeasurementService = unitofMeasurementService;
            _dosageFormService = dosageFormService;
            _packagingTypeService = packagingTypeService;
            _businessUnitService = businessUnitService;
            _countryService = countryService;
            _APISourcingService = masterAPISourcingService;
            _PidfApiDetailsService = pidfApiDetailsService;
            _pidfProductStrengthService = pidfProductStrengthService;
            _repository = _unitOfWork.GetRepository<Pidf>();
            _pidfApiRepository = unitOfWork.GetRepository<Pidfapidetail>();
            _pidfProductStrength = unitOfWork.GetRepository<PidfproductStrength>();
            _pidf_API_IPD_repository = _unitOfWork.GetRepository<PidfApiIpd>();
            _pidf_API_RnD_repository = _unitOfWork.GetRepository<PidfApiRnD>();
            _pidf_API_Charter_repository= _unitOfWork.GetRepository<PidfApiCharter>();
            _pidf_API_TimelineInMonth_repository= _unitOfWork.GetRepository<PidfApiCharterTimelineInMonth>();
            _masterDIAService = masterDium;
            _masterMarketExtensionService = masterMarketExtensionService;
            _masterBERequirementService = masterBERequirementService;
            _masterProductTypeService = masterProductTypeService;
            _masterPlantService = masterPlantService;
            _masterWorkflowService = masterWorkflowService;
            _masterFormRNDDivisionService = masterFormRNDDivisionService;
            _masterFormulationService = masterFormulationService;
            _masterAnalyticalGLService = masterAnalyticalGLService;
            _pbfRepository = _unitOfWork.GetRepository<PidfPbf>();
            _productStrengthService = productStrengthService;
            _auditLogService = auditLogService;
            _configuration = configuration;
            _masterTestTypeService = masterTestTypeService;
            _masterTestLicenseService = masterTestLicenseService;
            _helper = helper;
            _pidfPbfAnalyticalRepository = _unitOfWork.GetRepository<PidfPbfAnalytical>();
            _pidfPbfAnalyticalPrototypeRepository = _unitOfWork.GetRepository<PidfPbfAnalyticalPrototype>();
            _pidfPbfAnalyticalScaleUpRepository = _unitOfWork.GetRepository<PidfPbfAnalyticalScaleUp>();
            _pidfPbfAnalyticalExhibitRepository = _unitOfWork.GetRepository<PidfPbfAnalyticalExhibit>();
        }
        public string FileValidation(IFormFile file)
        {
            PIDFMedicalViewModel fileUpload = new PIDFMedicalViewModel();
            fileUpload.FileSize = Convert.ToInt32(_configuration.GetSection("FileUploadSettings").GetSection("MaxFileSizeMb").Value);
            try
            {
                var supportedTypes = _configuration.GetSection("FileUploadSettings").GetSection("AllowedFileExtension").Value;
                var fileTypes = supportedTypes.Split(',');
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!fileTypes.Contains(fileExt))
                {
                    fileUpload.ErrorMessage = _configuration.GetSection("FileUploadSettings").GetSection("FileNotAllowedErrorMessage").Value;
                }
                else if (file.Length > (fileUpload.FileSize * 1024 * 1024))
                {
                    fileUpload.ErrorMessage = _configuration.GetSection("FileUploadSettings").GetSection("FileSizeExceedErrorMessage").Value;
                }
                else
                {
                    fileUpload.ErrorMessage = null;
                }
                return fileUpload.ErrorMessage;
            }
            catch (Exception ex)
            {
                fileUpload.ErrorMessage = "Upload Container Should Not Be Empty or Contact Admin";
                return fileUpload.ErrorMessage;
            }
        }
        public async Task FileUpload(IFormFile files, string path, string uniqueFileName)
        {
            if (files != null)
            {
                string uploadFolder = path;
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                var filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }

            }
        }
        //------------Start------API_IPD_Details_Form_Entity--------------------------
        #region API_IPD_Details_Form_Entity 
        public async Task<DBOperation> AddUpdateAPIIPD(IFormCollection _oAPIIPD_Form, string _webrootPath)
        {
            bool hasNewUploadFile = true;
            string uniqueFileName = "";
            string ErrorMsg = "";

            _oAPIIPD_Form.TryGetValue("Data", out StringValues Data);
            dynamic jsonObject = JsonConvert.DeserializeObject(Data);
            var path = Path.Combine(_webrootPath, "Uploads\\PIDF\\APIIPD");

            bool IskeepLastFile = (jsonObject.MarketDetailsFileName == null || jsonObject.MarketDetailsFileName == "") ? false : true;

            if ((_oAPIIPD_Form.Files == null || _oAPIIPD_Form.Files.Count <= 0))
                hasNewUploadFile = false;
            else
                IskeepLastFile = false;


            if (hasNewUploadFile)
            {
                var MarketCGIFile = _oAPIIPD_Form.Files[0];
                uniqueFileName = Path.GetFileNameWithoutExtension(MarketCGIFile.FileName)
                                + Guid.NewGuid().ToString().Substring(0, 4)
                                + Path.GetExtension(MarketCGIFile.FileName);

                var fullPath = path + "\\" + MarketCGIFile.FileName;
                var itmFileName = "APIIPD\\" + MarketCGIFile.FileName;


                ErrorMsg = FileValidation(MarketCGIFile);
            }

            PIDFAPIIPDFormEntity _oAPIIPD = new PIDFAPIIPDFormEntity();

            _oAPIIPD.APIIPDDetailsFormID = jsonObject.APIIPDDetailsFormID;
            _oAPIIPD.MarketDetailsFileName = uniqueFileName;
            _oAPIIPD.DrugsCategory = jsonObject.DrugsCategory;
            _oAPIIPD.ProductStrength = jsonObject.ProductStrength;
            _oAPIIPD.ProductTypeId = jsonObject.ProductTypeId;
            _oAPIIPD.Pidfid = jsonObject.Pidfid;
            _oAPIIPD.LoggedInUserId = jsonObject.LoggedInUserId;
            var _APIIPDDBEntity = new PidfApiIpd();
            if (_oAPIIPD.APIIPDDetailsFormID > 0) 
			{
				var lastApiIpd = _pidf_API_IPD_repository.GetAll().First(x => x.PidfApiIpdId == _oAPIIPD.APIIPDDetailsFormID);
                var OldObjAPIIPD = lastApiIpd;
                if (lastApiIpd != null) 
				{
					if (!IskeepLastFile)
					{
                        var exsistingFilePath = path + "\\" + lastApiIpd.MarketDetailsFileName;
                        if (System.IO.File.Exists(exsistingFilePath))
                        {
                            System.IO.File.Delete(exsistingFilePath);
                        }
                        if (ErrorMsg == null && hasNewUploadFile)
                        {
                            var MarketCGIFile = _oAPIIPD_Form.Files[0];
                            await FileUpload(MarketCGIFile, path, uniqueFileName);
                        }
                        lastApiIpd.MarketDetailsFileName = _oAPIIPD.MarketDetailsFileName;
                    }

                    lastApiIpd.DrugsCategory = _oAPIIPD.DrugsCategory;
                    lastApiIpd.ProductTypeId = _oAPIIPD.ProductTypeId;
                    lastApiIpd.ProductStrength = _oAPIIPD.ProductStrength;

                    lastApiIpd.Pidfid = long.Parse(_oAPIIPD.Pidfid);
                    lastApiIpd.ModifyBy = _oAPIIPD.LoggedInUserId;
                    lastApiIpd.ModifyDate = DateTime.Now;
                    _pidf_API_IPD_repository.UpdateAsync(lastApiIpd);

                //    var isSuccess = await _auditLogService.CreateAuditLog<PidfApiIpd>(Utility.Audit.AuditActionType.Update,
                //Utility.Enums.ModuleEnum.PBF, OldObjAPIIPD, lastApiIpd,0);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
			else
			{
                if (ErrorMsg == null && hasNewUploadFile)
                {
                    var MarketCGIFile = _oAPIIPD_Form.Files[0];
                    await FileUpload(MarketCGIFile, path, uniqueFileName);
                }

                _APIIPDDBEntity.DrugsCategory = _oAPIIPD.DrugsCategory;
                _APIIPDDBEntity.ProductTypeId = _oAPIIPD.ProductTypeId;
                _APIIPDDBEntity.ProductStrength = _oAPIIPD.ProductStrength;
                _APIIPDDBEntity.MarketDetailsFileName = _oAPIIPD.MarketDetailsFileName;
                _APIIPDDBEntity.Pidfid = long.Parse(_oAPIIPD.Pidfid);
                _APIIPDDBEntity.CreatedBy = _oAPIIPD.LoggedInUserId;
                _APIIPDDBEntity.CreatedDate = DateTime.Now;
                _pidf_API_IPD_repository.AddAsync(_APIIPDDBEntity);
            }
			await _unitOfWork.SaveChangesAsync();
            var _StatusID = (_oAPIIPD.SaveType == "Save") ? Master_PIDFStatus.APISubmitted : Master_PIDFStatus.APIInProgress;
            await _auditLogService.UpdatePIDFStatusCommon(long.Parse(_oAPIIPD.Pidfid), (int)_StatusID, _oAPIIPD.LoggedInUserId);
            return DBOperation.Success;
    }

        public async Task<PIDFAPIIPDFormEntity> GetAPIIPDFormData(long pidfId, string _webrootPath)
        {

            PIDFAPIIPDFormEntity _oApiIpdData = new PIDFAPIIPDFormEntity();
            var _oAPIIPD = await _pidf_API_IPD_repository.GetAsync(x => x.Pidfid == pidfId);
            if (_oAPIIPD != null)
            {
                //_oApiIpdData.FormulationQuantity = _oAPIIPD.FormulationQuantity;
                //_oApiIpdData.APIIPDDetailsFormID = _oAPIIPD.PidfApiIpdId;
                // _oApiIpdData.PlantQC = _oAPIIPD.PlantQc;
                //_oApiIpdData.Development = _oAPIIPD.Development;
                //_oApiIpdData.Total = _oAPIIPD.Total;
                //_oApiIpdData.Exhibit = _oAPIIPD.Exhibit;
                //_oApiIpdData.ScaleUp = _oAPIIPD.ScaleUp;
                string baseURL = _configuration.GetSection("Apiconfig").GetSection("EmcureNPDAPIUrl").Value; //https://localhost:44362/
                var path = Path.Combine(baseURL, "Uploads/PIDF/APIIPD");
                var fullPath = path + "/" + _oAPIIPD.MarketDetailsFileName;

                _oApiIpdData.DrugsCategory = _oAPIIPD.DrugsCategory;
                _oApiIpdData.ProductTypeId = (int)_oAPIIPD.ProductTypeId;
                _oApiIpdData.APIIPDDetailsFormID = _oAPIIPD.PidfApiIpdId;
                _oApiIpdData.ProductStrength = _oAPIIPD.ProductStrength;
                _oApiIpdData.MarketDetailsFileName = fullPath;
                _oApiIpdData.Pidfid = _oAPIIPD.Pidfid.ToString();
            }
            return _oApiIpdData;
        }
        //------------End------API_IPD_Details_Form_Entity--------------------------
        #endregion


        public async Task<PIDFAPICharterFormEntity>  GetAPICharterFormData(long pidfId)
        {
            PIDFAPICharterFormEntity _oCharterEntity = new PIDFAPICharterFormEntity();
            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFID", pidfId)
            };
            var dbresult = await _pidf_API_Charter_repository.GetDataSetBySP("stp_npd_GetPIDFAPICharterData", 
                System.Data.CommandType.StoredProcedure, osqlParameter);

            // dynamic _CharterObjects = new ExpandoObject();
            List<CharterObject> _CharterObjects = new List<CharterObject>();
            if (dbresult != null)
            {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                {
                     _CharterObjects = dbresult.Tables[0].DataTableToList<CharterObject>();
                    _oCharterEntity.TimelineInMonths = dbresult.Tables[1].DataTableToList<TimelineInMonths>();
                    _oCharterEntity.ManhourEstimates = dbresult.Tables[2].DataTableToList<ManhourEstimates>();
                    _oCharterEntity.AnalyticalDepartment = dbresult.Tables[3].DataTableToList<AnalyticalDepartment>();
                }
            }

            if (_CharterObjects.Count > 0)
            {
                _oCharterEntity.APIGroupLeader = _CharterObjects[0].APIGroupLeader;
                _oCharterEntity.ManHourRates = Convert.ToString(_CharterObjects[0].ManHourRates);
                _oCharterEntity.PIDFAPICharterFormID = _CharterObjects[0].PIDF_API_CharterId;
                _oCharterEntity.ProjectComplexityId = _CharterObjects[0].ProjectComplexityId;
            }

            return _oCharterEntity;
        }
        private List<DM> FillObjData<VM,DM>(List<VM> _vmObj)
        {
            var _objPidfApiCharterTimelineInMonth = new List<DM>();
            foreach (var obj in _vmObj)
            {
                var _objTimelineInMonths = _mapperFactory.Get<VM, DM>(obj);
                _objPidfApiCharterTimelineInMonth.Add(_objTimelineInMonths);
            }
            return _objPidfApiCharterTimelineInMonth;
        }
        public void RemoveChildDataAPICharter(long APICharterId)
        {
            PIDFAPICharterFormEntity _oCharterEntity = new PIDFAPICharterFormEntity();
            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFAPICharterId", APICharterId)
            };
            var dbresult =  _pidf_API_Charter_repository.GetDataSetBySP("stp_npd_RemoveChildDataAPICharter",
                System.Data.CommandType.StoredProcedure, osqlParameter);           
        }
        public async Task<DBOperation> AddUpdateAPICharter(PIDFAPICharterFormEntity _oAPICharter)
        {           
            var _objPidfApiCharterTimelineInMonth = FillObjData<TimelineInMonths, PidfApiCharterTimelineInMonth>(_oAPICharter.TimelineInMonths);
            var _objPidfApiCharterManhourEstimates = FillObjData<ManhourEstimates, PidfApiCharterManhourEstimate>(_oAPICharter.ManhourEstimates);
            var _objPidfApiCharterAnalyticalDepartment = FillObjData<AnalyticalDepartment, PidfApiCharterAnalyticalDepartment>(_oAPICharter.AnalyticalDepartment);

            if (_oAPICharter.PIDFAPICharterFormID > 0)
            {
                var lastApiCharter = _pidf_API_Charter_repository.GetAll().First(x => x.PidfApiCharterId == _oAPIRnD.PIDFAPICharterFormID);
                var OldObjAPICharter = lastApiCharter;
                if (lastApiCharter != null)
                {
                    RemoveChildDataAPICharter(_oAPICharter.PIDFAPICharterFormID); // Remove child table data
                    lastApiCharter.PidfApiCharterTimelineInMonths = _objPidfApiCharterTimelineInMonth;
                    lastApiCharter.PidfApiCharterManhourEstimates = _objPidfApiCharterManhourEstimates;
                    lastApiCharter.PidfApiCharterAnalyticalDepartments = _objPidfApiCharterAnalyticalDepartment;
                    


                    _oAPICharter.ManHourRates = (Convert.ToString(_oAPICharter.ManHourRates) == "" || _oAPICharter.ManHourRates == null) ? "0" : _oAPICharter.ManHourRates;
                    lastApiCharter.ManHourRates = int.Parse(_oAPICharter.ManHourRates);
                    lastApiCharter.ApigroupLeader = _oAPICharter.APIGroupLeader;
                    lastApiCharter.ProjectComplexityId = _oAPICharter.ProjectComplexityId;
                    
                    lastApiCharter.Pidfid = long.Parse(_oAPICharter.Pidfid);
                    lastApiCharter.ModifyBy = _oAPICharter.LoggedInUserId;
                    lastApiCharter.ModifyDate = DateTime.Now;
                    _pidf_API_Charter_repository.UpdateAsync(lastApiCharter);
                    //Implement AuditLog
                    //var isSuccess = await _auditLogService.CreateAuditLog<PidfApiCharter>(Utility.Audit.AuditActionType.Update,
                    //Utility.Enums.ModuleEnum.PBF, OldObjAPICharter, lastApiCharter, 0);
                }
                else
                {
                    return DBOperation.NotFound;
                }              
            }
            else
            {
                var _oDBApiCharter = new PidfApiCharter();

                _oAPIRnD.ManHourRates = (Convert.ToString(_oAPIRnD.ManHourRates) == "") ? "0" : _oAPIRnD.ManHourRates;
                _oDBApiCharter.ManHourRates = int.Parse(_oAPIRnD.ManHourRates);
                _oDBApiCharter.ApigroupLeader = _oAPIRnD.APIGroupLeader;
                _oDBApiCharter.ProjectComplexityId = _oAPIRnD.ProjectComplexityId;                
                _oDBApiCharter.Pidfid = long.Parse(_oAPIRnD.Pidfid);

                _oDBApiCharter.CreatedBy = _oAPIRnD.LoggedInUserId;
                _oDBApiCharter.CreatedDate = DateTime.Now;
                 _pidf_API_Charter_repository.AddAsync(_oDBApiCharter);
                //Implement PIDF staurs change
            }
            await _unitOfWork.SaveChangesAsync();
            var _StatusID = (_oAPICharter.SaveType == "Save") ? Master_PIDFStatus.APISubmitted : Master_PIDFStatus.APIInProgress;
            await _auditLogService.UpdatePIDFStatusCommon(long.Parse(_oAPICharter.Pidfid), (int)_StatusID, _oAPICharter.LoggedInUserId);
            return DBOperation.Success;
        }
        public async Task<PIDFAPIRnDFormEntity> GetAPIRnDFormData(long pidfId, string _webrootPath)
        {

            PIDFAPIRnDFormEntity _oApiRnDData = new PIDFAPIRnDFormEntity();
            var _oAPIRnD = await _pidf_API_RnD_repository.GetAsync(x => x.Pidfid == pidfId);
            var _oAPIIpd = await _pidf_API_IPD_repository.GetAsync(x => x.Pidfid == pidfId);
            if (_oAPIRnD != null)
            {
                _oApiRnDData.PIDFAPIRnDFormID = _oAPIRnD.PidfApiRnDId;
                _oApiRnDData.Pidfid = _oAPIRnD.Pidfid.ToString();

                _oApiRnDData.PlantQC = _oAPIRnD.PlantQc;
                _oApiRnDData.Development = _oAPIRnD.Development;
                _oApiRnDData.Total = _oAPIRnD.Total;
                _oApiRnDData.Exhibit = _oAPIRnD.Exhibit;
                _oApiRnDData.ScaleUp = _oAPIRnD.ScaleUp;
                _oApiRnDData.MarketID = _oAPIRnD.MarketExtenstionId;
                _oApiRnDData.SponsorBusinessPartner = _oAPIRnD.SponsorBusinessPartner;
                _oApiRnDData.APIMarketPrice = _oAPIRnD.ApimarketPrice;
                _oApiRnDData.APITargetRMC_CCPC = _oAPIRnD.ApitargetRmcCcpc;


            }
            if (_oAPIIpd != null)
            {
                string baseURL = _configuration.GetSection("Apiconfig").GetSection("EmcureNPDAPIUrl").Value;
                var path = Path.Combine(baseURL, "Uploads/PIDF/APIIPD");
                var fullPath = path + "/" + _oAPIIpd.MarketDetailsFileName;

                _oApiRnDData.DrugsCategory = _oAPIIpd.DrugsCategory;
                _oApiRnDData.ProductTypeId = (int)_oAPIIpd.ProductTypeId;
                _oApiRnDData.ProductStrength = _oAPIIpd.ProductStrength;
                _oApiRnDData.MarketDetailsFileName = fullPath;
                var _objProductType = await _masterProductTypeService.GetById((int)_oAPIIpd.ProductTypeId);
                if (_objProductType != null)
                    _oApiRnDData.ProductType = _objProductType.ProductTypeName;

            }
            return _oApiRnDData;
        }
        public async Task<DBOperation> AddUpdateAPIRnD(PIDFAPIRnDFormEntity _oAPIRnD)
        {
            //PIDFAPIIPDFormEntity _exsistingAPIRnD = new PIDFAPIIPDFormEntity();
            if (_oAPIRnD.PIDFAPIRnDFormID > 0)
            {
                var lastApiRnD = _pidf_API_RnD_repository.GetAll().First(x => x.PidfApiRnDId == _oAPIRnD.PIDFAPIRnDFormID);
                var OldObjAPiIPD = lastApiRnD;
                if (lastApiRnD != null)
                {                  
                    lastApiRnD.PlantQc = _oAPIRnD.PlantQC;
                    lastApiRnD.Development = _oAPIRnD.Development;
                    lastApiRnD.Total = _oAPIRnD.Total;
                    lastApiRnD.Exhibit = _oAPIRnD.Exhibit;
                    lastApiRnD.ScaleUp = _oAPIRnD.ScaleUp;
                    lastApiRnD.MarketExtenstionId= _oAPIRnD.MarketID;
                    lastApiRnD.SponsorBusinessPartner= _oAPIRnD.SponsorBusinessPartner;
                    lastApiRnD.ApimarketPrice= _oAPIRnD.APIMarketPrice;
                    lastApiRnD.ApitargetRmcCcpc= _oAPIRnD.APITargetRMC_CCPC;
                    lastApiRnD.Pidfid = long.Parse(_oAPIRnD.Pidfid);
                    lastApiRnD.ModifyBy = _oAPIRnD.LoggedInUserId;
                    lastApiRnD.ModifyDate = DateTime.Now;
                    _pidf_API_RnD_repository.UpdateAsync(lastApiRnD);

                    var isSuccess = await _auditLogService.CreateAuditLog<PidfApiRnD>(Utility.Audit.AuditActionType.Update,
                 Utility.Enums.ModuleEnum.PBF, OldObjAPiIPD, lastApiRnD, 0);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var _oDBApiRnd = new PidfApiRnD();

                _oDBApiRnd.PlantQc = _oAPIRnD.PlantQC;
                _oDBApiRnd.Development = _oAPIRnD.Development;
                _oDBApiRnd.Total = _oAPIRnD.Total;
                _oDBApiRnd.Exhibit = _oAPIRnD.Exhibit;
                _oDBApiRnd.ScaleUp = _oAPIRnD.ScaleUp;
                _oDBApiRnd.MarketExtenstionId = _oAPIRnD.MarketID;
                _oDBApiRnd.SponsorBusinessPartner = _oAPIRnD.SponsorBusinessPartner;
                _oDBApiRnd.ApimarketPrice = _oAPIRnD.APIMarketPrice;
                _oDBApiRnd.ApitargetRmcCcpc = _oAPIRnD.APITargetRMC_CCPC;
                _oDBApiRnd.Pidfid = long.Parse(_oAPIRnD.Pidfid);

                _oDBApiRnd.CreatedBy = _oAPIRnD.LoggedInUserId;
                _oDBApiRnd.CreatedDate = DateTime.Now;
                _pidf_API_RnD_repository.AddAsync(_oDBApiRnd);
            }
            await _unitOfWork.SaveChangesAsync();
            var _StatusID = (_oAPIRnD.SaveType == "Save") ? Master_PIDFStatus.APISubmitted : Master_PIDFStatus.APISubmitted;
            await _auditLogService.UpdatePIDFStatusCommon(long.Parse(_oAPIRnD.Pidfid), (int)_StatusID, _oAPIRnD.LoggedInUserId);
            return DBOperation.Success;
        }

        public async Task<dynamic> FillDropdown()
        {
            dynamic DropdownObjects = new ExpandoObject();

            DropdownObjects.MasterOrals = _oralService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterUnitofMeasurements = _unitofMeasurementService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterDosageForms = _dosageFormService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterPackagingTypes = _packagingTypeService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterBusinessUnits = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterCountrys = _countryService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MarketExtensions = _masterMarketExtensionService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.InHouses = new List<InHouseEntity> { new InHouseEntity { InHouseId = 1, InHouseName = "Yes" }, new InHouseEntity { InHouseId = 2, InHouseName = "No" } };
            DropdownObjects.MasterAPISourcing = _APISourcingService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterDIAs = _masterDIAService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterBERequirements = _masterBERequirementService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterProductType = _masterProductTypeService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterPlantService = _masterPlantService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterWorkflowService = _masterWorkflowService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterFormRNDDivisionService = _masterFormRNDDivisionService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterFormulationService = _masterFormulationService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterAnalyticalGLService = _masterAnalyticalGLService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterTestType = _masterTestTypeService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterTestLicense = _masterTestLicenseService.GetAll().Result.Where(xx => xx.IsActive).ToList();

            return DropdownObjects;
        }
        public async Task<DBOperation> AddUpdatePBF(PidfPbfEntity pbfEntity)
        {
            return DBOperation.Success;
        }
        // t		

        public async Task<PidfPbfEntity> GetPbfFormData(long pidfId, int buid, int? strengthid)
        {
            PidfPbfRnDEntity pidfPbfRnDEntity = new PidfPbfRnDEntity();
            Expression<Func<PidfPbf, bool>> expr;
            if (strengthid != null)
            {
                expr = u => u.Pidfid == pidfId;
            }
            else
            {
                expr = u => u.Pidfid == pidfId;
            }
            //var data = _mapperFactory.Get<Pidfipd, PIDFormEntity>(await _repository.GetAsync(id));

            dynamic objData = (dynamic)await _pbfRepository.FindAllAsync(expr);
            var data = new PidfPbfEntity();
            data.MasterBusinessUnitEntities = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            pidfPbfRnDEntity.MasterStrengthEntities = _productStrengthService.GetAll().Result.Where(x => x.Pidfid == pidfId).ToList();
            data.pidfPbfRndEntity = pidfPbfRnDEntity;
            data.BusinessUnitId = buid;
            data.Pidfid = pidfId;
            Pidf objPidf = await _repository.GetAsync(pidfId);
            data.StatusId = objPidf.StatusId;

            return data;
        }

        public async Task<PidfPbfAnalyticalEntity> GetPBFAnalyticalReadonlyData(long pidfid)
        {
            PidfPbfAnalyticalEntity PAE = new PidfPbfAnalyticalEntity();
            PAE.ProductStrength = _pidfProductStrengthService.GetAll().Result.Where(x => x.Pidfid == pidfid).ToList();
            var objpidf = _repository.GetAll().Where(x => x.Pidfid == pidfid).FirstOrDefault();
            PAE.ProjectName = objpidf.MoleculeName;
            PAE.SAPProjectProjectCode = "Need to discuss";
            PAE.ImprintingEmbossingCodes = "Need to discuss";
            return PAE;
        }
        public async Task<DBOperation> AddUpdatePBFDetails(PidfPbfEntity pbfEntity)
        {  //Dummy function to same PIDFPBF Data          
            PidfPbfEntity _objpidfpbf = new PidfPbfEntity();
            var loggedInUserId = _helper.GetLoggedInUser().UserId;
            //PIDFAPIIPDFormEntity _exsistingAPIRnD = new PIDFAPIIPDFormEntity();
            if (pbfEntity.Pidfpbfid > 0)
            {
                var objpidfpbfupdate = _pbfRepository.GetAll().First(x => x.Pidfpbfid == pbfEntity.Pidfpbfid);
                if (objpidfpbfupdate != null)
                {

                    objpidfpbfupdate.ModifyDate = DateTime.Now;
                    objpidfpbfupdate.ModifyBy = loggedInUserId;
                    _pbfRepository.UpdateAsync(objpidfpbfupdate);
                    return DBOperation.Success;
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var _objpidfpbfadd = new PidfPbf();
                _objpidfpbfadd.Pidfid = pbfEntity.Pidfid;
                _objpidfpbfadd.ProjectName = pbfEntity.ProjectName;
                _objpidfpbfadd.Market = pbfEntity.Market;
                _objpidfpbfadd.BusinessRelationable = pbfEntity.BusinessRelationable;
                _objpidfpbfadd.BerequirementId = pbfEntity.BerequirementId;
                _objpidfpbfadd.NumberOfApprovedAnda = pbfEntity.NumberOfApprovedAnda;
                _objpidfpbfadd.ProductTypeId = pbfEntity.ProductTypeId;
                _objpidfpbfadd.PlantId = pbfEntity.PlantId;
                _objpidfpbfadd.WorkflowId = pbfEntity.WorkflowId;
                _objpidfpbfadd.DosageId = pbfEntity.DosageId;
                _objpidfpbfadd.PatentStatus = pbfEntity.PatentStatus;
                _objpidfpbfadd.SponsorBusinessPartner = pbfEntity.SponsorBusinessPartner;
                _objpidfpbfadd.FormRnDdivisionId = pbfEntity.FormRnDdivisionId;
                //_objpidfpbfadd.Dosage = pbfEntity.Dosage;
                //_objpidfpbfadd.ManufacturingId = pbfEntity.;
                _objpidfpbfadd.CreatedBy = loggedInUserId;
                _objpidfpbfadd.CreatedDate = DateTime.Now;
                _objpidfpbfadd.ModifyDate = DateTime.Now;
                _pbfRepository.AddAsync(_objpidfpbfadd);

                await _unitOfWork.SaveChangesAsync();
                await SaveChildDetails(_objpidfpbfadd.Pidfpbfid, loggedInUserId, pbfEntity.pidfPbfAnalyticalEntity, pbfEntity.pidfPbfClinicalEntity, pbfEntity.pidfPbfRndEntity);

                return DBOperation.Success;




            }


        }
        // t
        private async Task<bool> SaveChildDetails(long Pidfpbfid, int loggedInUserId, PidfPbfAnalyticalEntity analyticalEntites, PidfPbfClinicalEntity clinicalEntites, PidfPbfRnDEntity rnDEntites)
        {
            try
            {

                #region Analytical

                if (analyticalEntites != null && analyticalEntites.AnalyticalPrototypeEntities.Count() > 0 && analyticalEntites.AnalyticalScaleUpEntities.Count() > 0 && analyticalEntites.AnalyticalExhibitEntities.Count() > 0)
                {
                    if (analyticalEntites.PBFAnalyticalID > 0)
                    {
                        var objpidfpbfupdate = _pidfPbfAnalyticalRepository.GetAll().First(x => x.PbfanalyticalId == analyticalEntites.PBFAnalyticalID);
                        if (objpidfpbfupdate != null)
                        {

                            var _objpidfpbfadd = new PidfPbfAnalytical();
                            _objpidfpbfadd.Pbfid = Pidfpbfid;
                            _objpidfpbfadd.Pidfid = analyticalEntites.AnalyticalPIDFID;
                            _objpidfpbfadd.BusinessUnitId = analyticalEntites.AnalyticalBusinessUnitId;
                            _objpidfpbfadd.TotalExpense = analyticalEntites.TotalExpenses;
                            _objpidfpbfadd.ProjectComplexity = analyticalEntites.ProjectComplexity;
                            _objpidfpbfadd.ProductTypeId = analyticalEntites.AnalyticalProductTypeId;
                            _objpidfpbfadd.TestLicenseAvailability = analyticalEntites.AnalyticalLicence;
                            _objpidfpbfadd.BudgetTimelineSubmissionDate = analyticalEntites.BudgetTimelineSubmissionDate;
                            _objpidfpbfadd.FormulationId = analyticalEntites.AnalyticalFormulationGLId;
                            _objpidfpbfadd.AnalyticalId = analyticalEntites.AnalyticalAnalyticalGLId;
                            _pidfPbfAnalyticalRepository.UpdateAsync(_objpidfpbfadd);
                            //return DBOperation.Success;
                        }
                        else
                        {
                            //return DBOperation.NotFound;
                        }


                    }
                    else
                    {
                        var _objpidfpbfadd = new PidfPbfAnalytical();
                        _objpidfpbfadd.Pbfid = Pidfpbfid;
                        _objpidfpbfadd.Pidfid = analyticalEntites.AnalyticalPIDFID;
                        _objpidfpbfadd.BusinessUnitId = analyticalEntites.AnalyticalBusinessUnitId;
                        _objpidfpbfadd.TotalExpense = analyticalEntites.TotalExpenses;
                        _objpidfpbfadd.ProjectComplexity = analyticalEntites.ProjectComplexity;
                        //_objpidfpbfadd.StrengthId = analyticalEntites.StrengthId;
                        _objpidfpbfadd.ProductTypeId = analyticalEntites.AnalyticalProductTypeId;
                        _objpidfpbfadd.TestLicenseAvailability = analyticalEntites.AnalyticalLicence;
                        _objpidfpbfadd.BudgetTimelineSubmissionDate = analyticalEntites.BudgetTimelineSubmissionDate;
                        _objpidfpbfadd.FormulationId = analyticalEntites.AnalyticalFormulationGLId;
                        _objpidfpbfadd.AnalyticalId = analyticalEntites.AnalyticalAnalyticalGLId;
                        _objpidfpbfadd.CreatedBy = loggedInUserId;
                        _objpidfpbfadd.CreatedDate = DateTime.Now;
                        _pidfPbfAnalyticalRepository.AddAsync(_objpidfpbfadd);
                        await _unitOfWork.SaveChangesAsync();

                        //Save Prototype Table
                        if (analyticalEntites.AnalyticalPrototypeEntities != null && analyticalEntites.AnalyticalPrototypeEntities.Count() > 0)
                        {
                            List<PidfPbfAnalyticalPrototype> _objAnalyticalprototype = new List<PidfPbfAnalyticalPrototype>();
                            foreach (var item in analyticalEntites.AnalyticalPrototypeEntities)
                            {

                                PidfPbfAnalyticalPrototype analyticalprototype = new PidfPbfAnalyticalPrototype();
                                analyticalprototype.PbfanalyticalId = _objpidfpbfadd.PbfanalyticalId;
                                analyticalprototype.StrengthId = analyticalEntites.StrengthId;
                                analyticalprototype.TestTypeId = item.TestTypeId;
                                analyticalprototype.Numberoftests = item.Numberoftests;
                                analyticalprototype.PrototypeDevelopment = item.PrototypeDevelopment;
                                analyticalprototype.Cost = item.Cost;
                                analyticalprototype.PrototypeCost = item.PrototypeCost;
                                analyticalprototype.CreatedDate = DateTime.Now;
                                analyticalprototype.CreatedBy = loggedInUserId;

                                analyticalprototype = _mapperFactory.Get<PidfPbfAnalyticalPrototypeEntity, PidfPbfAnalyticalPrototype>(item);
                                _objAnalyticalprototype.Add(analyticalprototype);
                            }
                            _pidfPbfAnalyticalPrototypeRepository.AddRangeAsync(_objAnalyticalprototype);
                            await _unitOfWork.SaveChangesAsync();
                        }
                        //Save ScaleUp Table
                        if (analyticalEntites.AnalyticalScaleUpEntities != null && analyticalEntites.AnalyticalScaleUpEntities.Count() > 0)
                        {
                            List<PidfPbfAnalyticalScaleUp> _objAnalyticalscaleup = new List<PidfPbfAnalyticalScaleUp>();
                            foreach (var item in analyticalEntites.AnalyticalScaleUpEntities)
                            {

                                PidfPbfAnalyticalScaleUp analyticalscaleup = new PidfPbfAnalyticalScaleUp();
                                analyticalscaleup.PbfanalyticalId = _objpidfpbfadd.PbfanalyticalId;
                                analyticalscaleup.StrengthId = analyticalEntites.StrengthId;
                                analyticalscaleup.TestTypeId = item.TestTypeId;
                                analyticalscaleup.Numberoftests = item.Numberoftests;
                                analyticalscaleup.PrototypeDevelopment = item.PrototypeDevelopment;
                                analyticalscaleup.Cost = item.Cost;
                                analyticalscaleup.PrototypeCost = item.PrototypeCost;
                                analyticalscaleup.CreatedDate = DateTime.Now;
                                analyticalscaleup.CreatedBy = loggedInUserId;
                                analyticalscaleup = _mapperFactory.Get<PidfPbfAnalyticalScaleUpEntity, PidfPbfAnalyticalScaleUp>(item);
                                _objAnalyticalscaleup.Add(analyticalscaleup);
                            }
                            _pidfPbfAnalyticalScaleUpRepository.AddRangeAsync(_objAnalyticalscaleup);
                            await _unitOfWork.SaveChangesAsync();
                        }
                        //Save Exhibit Table
                        if (analyticalEntites.AnalyticalExhibitEntities != null && analyticalEntites.AnalyticalExhibitEntities.Count() > 0)
                        {
                            List<PidfPbfAnalyticalExhibit> _objAnalyticalexhibit = new List<PidfPbfAnalyticalExhibit>();
                            foreach (var item in analyticalEntites.AnalyticalExhibitEntities)
                            {

                                PidfPbfAnalyticalExhibit analyticalexhibit = new PidfPbfAnalyticalExhibit();
                                analyticalexhibit.PbfanalyticalId = _objpidfpbfadd.PbfanalyticalId;
                                analyticalexhibit.StrengthId = analyticalEntites.StrengthId;
                                analyticalexhibit.TestTypeId = item.TestTypeId;
                                analyticalexhibit.Numberoftests = item.Numberoftests;
                                analyticalexhibit.PrototypeDevelopment = item.PrototypeDevelopment;
                                analyticalexhibit.Cost = item.Cost;
                                analyticalexhibit.PrototypeCost = item.PrototypeCost;
                                analyticalexhibit.CreatedDate = DateTime.Now;
                                analyticalexhibit.CreatedBy = loggedInUserId;
                                analyticalexhibit = _mapperFactory.Get<PidfPbfAnalyticalExhibitEntity, PidfPbfAnalyticalExhibit>(item);
                                _objAnalyticalexhibit.Add(analyticalexhibit);
                            }
                            _pidfPbfAnalyticalExhibitRepository.AddRangeAsync(_objAnalyticalexhibit);
                            await _unitOfWork.SaveChangesAsync();
                        }
                    }

                }


                #endregion

                #region Clinical

                if (clinicalEntites != null && clinicalEntites.pidfpbfClinicalpilotBioFastingEntity.Count() > 0 && clinicalEntites.pidfpbfClinicalPilotBioFedEntity.Count() > 0 && clinicalEntites.pidfpbfClinicalPivotalBioFastingEntity.Count() > 0 && clinicalEntites.pidfpbfClinicalPivotalBioFedEntity.Count() > 0)
                {
                    if (clinicalEntites.PBFClinicalID > 0)
                    {
                        var objpidfpbfupdate = _pidfPbfClinicalRepository.GetAll().First(x => x.PbfclinicalId == clinicalEntites.PBFClinicalID);
                        if (objpidfpbfupdate != null)
                        {

                            var _objpidfpbfupdate = new PidfPbfClinical();
                            _objpidfpbfupdate.Pbfid = Pidfpbfid;
                            _objpidfpbfupdate.Pidfid = clinicalEntites.ClinicalPIDFID;
                            _objpidfpbfupdate.BusinessUnitId = clinicalEntites.ClinicalBusinessUnitId;
                            _objpidfpbfupdate.TotalExpense = clinicalEntites.TotalExpenses;
                            _objpidfpbfupdate.ProjectComplexity = clinicalEntites.ProjectComplexity;
                            _objpidfpbfupdate.StrengthId = clinicalEntites.StrengthId;
                            _objpidfpbfupdate.ProductTypeId = clinicalEntites.ClinicalProductTypeId;
                            _objpidfpbfupdate.TestLicenseAvailability = clinicalEntites.ClinicalLicence;
                            _objpidfpbfupdate.BudgetTimelineSubmissionDate = clinicalEntites.BudgetTimelineSubmissionDate;
                            _objpidfpbfupdate.FormulationId = clinicalEntites.ClinicalFormulationGLId;
                            _objpidfpbfupdate.AnalyticalId = clinicalEntites.ClinicalAnalyticalGLId;
                            _pidfPbfClinicalRepository.UpdateAsync(_objpidfpbfupdate);
                            //return DBOperation.Success;
                        }
                        else
                        {
                            //return DBOperation.NotFound;
                        }


                    }
                    else
                    {
                        var _objpidfpbfadd = new PidfPbfClinical();
                        _objpidfpbfadd.Pbfid = Pidfpbfid;
                        _objpidfpbfadd.Pidfid = clinicalEntites.ClinicalPIDFID;
                        _objpidfpbfadd.BusinessUnitId = clinicalEntites.ClinicalBusinessUnitId;
                        _objpidfpbfadd.TotalExpense = clinicalEntites.TotalExpenses;
                        _objpidfpbfadd.ProjectComplexity = clinicalEntites.ProjectComplexity;
                        _objpidfpbfadd.StrengthId = clinicalEntites.StrengthId;
                        _objpidfpbfadd.ProductTypeId = clinicalEntites.ClinicalProductTypeId;
                        _objpidfpbfadd.TestLicenseAvailability = clinicalEntites.ClinicalLicence;
                        _objpidfpbfadd.BudgetTimelineSubmissionDate = clinicalEntites.BudgetTimelineSubmissionDate;
                        _objpidfpbfadd.FormulationId = clinicalEntites.ClinicalFormulationGLId;
                        _objpidfpbfadd.AnalyticalId = clinicalEntites.ClinicalAnalyticalGLId;
                        _objpidfpbfadd.CreatedBy = loggedInUserId;
                        _objpidfpbfadd.CreatedDate = DateTime.Now;
                        _pidfPbfClinicalRepository.AddAsync(_objpidfpbfadd);
                        await _unitOfWork.SaveChangesAsync();

                        //Save pilot Bio Fasting Table
                        if (clinicalEntites.pidfpbfClinicalpilotBioFastingEntity != null && clinicalEntites.pidfpbfClinicalpilotBioFastingEntity.Count() > 0)
                        {
                            List<PidfPbfClinicalPilotBioFasting> _objclinicalPilotBioFasting = new List<PidfPbfClinicalPilotBioFasting>();
                            foreach (var item in clinicalEntites.pidfpbfClinicalpilotBioFastingEntity)
                            {

                                PidfPbfClinicalPilotBioFasting clinicalPilotBioFasting = new PidfPbfClinicalPilotBioFasting();
                                clinicalPilotBioFasting.PbfclinicalId = _objpidfpbfadd.PbfclinicalId;
                                clinicalPilotBioFasting.StrengthId = analyticalEntites.StrengthId;
                                clinicalPilotBioFasting.Fasting = item.Fasting;
                                clinicalPilotBioFasting.NumberofVolunteers = item.NumberofVolunteers;
                                clinicalPilotBioFasting.ClinicalCostandVol = item.ClinicalCostandVol;
                                clinicalPilotBioFasting.DocCostandStudy = item.DocCostandStudy;
                                clinicalPilotBioFasting.TotalCost = item.TotalCost;
                                clinicalPilotBioFasting.CreatedDate = DateTime.Now;
                                clinicalPilotBioFasting.CreatedBy = loggedInUserId;

                                clinicalPilotBioFasting = _mapperFactory.Get<PidfPbfClinicalPilotBioFastingEntity, PidfPbfClinicalPilotBioFasting>(item);
                                _objclinicalPilotBioFasting.Add(clinicalPilotBioFasting);
                            }
                            _pidfPbfClinicalPilotBioFastingRepository.AddRangeAsync(_objclinicalPilotBioFasting);
                            await _unitOfWork.SaveChangesAsync();
                        }

                        //Save pilot Bio Fed Table
                        if (clinicalEntites.pidfpbfClinicalPilotBioFedEntity != null && clinicalEntites.pidfpbfClinicalPilotBioFedEntity.Count() > 0)
                        {
                            List<PidfPbfClinicalPilotBioFed> _objclinicalPilotBioFed = new List<PidfPbfClinicalPilotBioFed>();
                            foreach (var item in clinicalEntites.pidfpbfClinicalPilotBioFedEntity)
                            {

                                PidfPbfClinicalPilotBioFed clinicalPilotBioFed = new PidfPbfClinicalPilotBioFed();
                                clinicalPilotBioFed.PbfclinicalId = _objpidfpbfadd.PbfclinicalId;
                                clinicalPilotBioFed.StrengthId = analyticalEntites.StrengthId;
                                clinicalPilotBioFed.Fed = item.Fed;
                                clinicalPilotBioFed.NumberofVolunteers = item.NumberofVolunteers;
                                clinicalPilotBioFed.ClinicalCostandVol = item.ClinicalCostandVol;
                                clinicalPilotBioFed.DocCostandStudy = item.DocCostandStudy;
                                clinicalPilotBioFed.TotalCost = item.TotalCost;
                                clinicalPilotBioFed.CreatedDate = DateTime.Now;
                                clinicalPilotBioFed.CreatedBy = loggedInUserId;

                                clinicalPilotBioFed = _mapperFactory.Get<PidfPbfClinicalPilotBioFedEntity, PidfPbfClinicalPilotBioFed>(item);
                                _objclinicalPilotBioFed.Add(clinicalPilotBioFed);
                            }
                            _pidfPbfClinicalPilotBioFedRepository.AddRangeAsync(_objclinicalPilotBioFed);
                            await _unitOfWork.SaveChangesAsync();
                        }
                        //Save pivotal Bio Fasting Table
                        if (clinicalEntites.pidfpbfClinicalPivotalBioFastingEntity != null && clinicalEntites.pidfpbfClinicalPivotalBioFastingEntity.Count() > 0)
                        {
                            List<PidfPbfClinicalPivotalBioFasting> _objclinicalPivotalBioFasting = new List<PidfPbfClinicalPivotalBioFasting>();
                            foreach (var item in clinicalEntites.pidfpbfClinicalPivotalBioFastingEntity)
                            {

                                PidfPbfClinicalPivotalBioFasting clinicalPivotalBioFasting = new PidfPbfClinicalPivotalBioFasting();
                                clinicalPivotalBioFasting.PbfclinicalId = _objpidfpbfadd.PbfclinicalId;
                                clinicalPivotalBioFasting.StrengthId = analyticalEntites.StrengthId;
                                clinicalPivotalBioFasting.Fasting = item.Fasting;
                                clinicalPivotalBioFasting.NumberofVolunteers = item.NumberofVolunteers;
                                clinicalPivotalBioFasting.ClinicalCostandVol = item.ClinicalCostandVol;
                                clinicalPivotalBioFasting.DocCostandStudy = item.DocCostandStudy;
                                clinicalPivotalBioFasting.TotalCost = item.TotalCost;
                                clinicalPivotalBioFasting.CreatedDate = DateTime.Now;
                                clinicalPivotalBioFasting.CreatedBy = loggedInUserId;

                                clinicalPivotalBioFasting = _mapperFactory.Get<PidfPbfClinicalPivotalBioFastingEntity, PidfPbfClinicalPivotalBioFasting>(item);
                                _objclinicalPivotalBioFasting.Add(clinicalPivotalBioFasting);
                            }
                            _pidfPbfClinicalPivotalBioFastingRepository.AddRangeAsync(_objclinicalPivotalBioFasting);
                            await _unitOfWork.SaveChangesAsync();
                        }

                        //Save pivotal Bio Fed Table
                        if (clinicalEntites.pidfpbfClinicalPivotalBioFedEntity != null && clinicalEntites.pidfpbfClinicalPivotalBioFedEntity.Count() > 0)
                        {
                            List<PidfPbfClinicalPivotalBioFed> _objclinicalPivotalBioFed = new List<PidfPbfClinicalPivotalBioFed>();
                            foreach (var item in clinicalEntites.pidfpbfClinicalPivotalBioFedEntity)
                            {

                                PidfPbfClinicalPivotalBioFed clinicalPivotalBioFed = new PidfPbfClinicalPivotalBioFed();
                                clinicalPivotalBioFed.PbfclinicalId = _objpidfpbfadd.PbfclinicalId;
                                clinicalPivotalBioFed.StrengthId = analyticalEntites.StrengthId;
                                clinicalPivotalBioFed.Fed = item.Fed;
                                clinicalPivotalBioFed.NumberofVolunteers = item.NumberofVolunteers;
                                clinicalPivotalBioFed.ClinicalCostandVol = item.ClinicalCostandVol;
                                clinicalPivotalBioFed.DocCostandStudy = item.DocCostandStudy;
                                clinicalPivotalBioFed.TotalCost = item.TotalCost;
                                clinicalPivotalBioFed.CreatedDate = DateTime.Now;
                                clinicalPivotalBioFed.CreatedBy = loggedInUserId;

                                clinicalPivotalBioFed = _mapperFactory.Get<PidfPbfClinicalPivotalBioFedEntity, PidfPbfClinicalPivotalBioFed>(item);
                                _objclinicalPivotalBioFed.Add(clinicalPivotalBioFed);
                            }
                            _pidfPbfClinicalPivotalBioFedRepository.AddRangeAsync(_objclinicalPivotalBioFed);
                            await _unitOfWork.SaveChangesAsync();
                        }
                    }

                   
                }

                

                #endregion

                #region RnD

                #endregion


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
