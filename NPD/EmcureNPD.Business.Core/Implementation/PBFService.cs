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
using System.Data;
using System.Reflection;

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
        private IRepository<PidfPbfAnalyticalCost> _PidfPbfAnalyticalCostsRepository { get; set; }
        private IRepository<PidfPbfClinical> _pidfPbfClinicalRepository { get; set; }
        private IRepository<PidfPbfClinicalPilotBioFasting> _pidfPbfClinicalPilotBioFastingRepository { get; set; }
        private IRepository<PidfPbfClinicalPilotBioFed> _pidfPbfClinicalPilotBioFedRepository { get; set; }
        private IRepository<PidfPbfClinicalPivotalBioFasting> _pidfPbfClinicalPivotalBioFastingRepository { get; set; }
        private IRepository<PidfPbfClinicalPivotalBioFed> _pidfPbfClinicalPivotalBioFedRepository { get; set; }
        private IRepository<PidfPbfClinicalCost> _pidfPbfClinicalCostRepository { get; set; }

        private IRepository<MasterDosage> _masterDosageRepository { get; set; }
        private IRepository<PidfproductStrength> _pidfProductStrength { get; set; }
        private readonly IHelper _helper;
        private IRepository<PidfPbfGeneral> _pidfPbfGeneralRepository { get; set; }
        private IRepository<MasterFilingType> _masterFillingTypeRepository { get; set; }
        private IRepository<PidfPbfMarketMapping> _pidfPbfMarketMappingRepository { get; set; }
        private IRepository<PidfPbfGeneralStrength> _pidfPbfGeneralStrengthRepository { get; set; }
        private IRepository<PidfPbfRnD> _pidfPbfRnDRepository { get; set; }
        private IRepository<PidfPbfRnDExicipientPrototype> _pidfPbfRnDExicipientPrototype { get; set; }
        private IRepository<PidfPbfRnDExicipientRequirement> _pidfPbfRnDExicipientRequirementRepository { get; set; }
        private IRepository<PidfPbfRnDPackagingMaterial> _pidfPbfRnDPackagingMaterialRepository { get; set; }
        private IRepository<PidfPbfAnalyticalCostStrengthMapping> _pidfPbfAnalyticalCostStrengthMappingRepository { get; set; }
        private IRepository<PidfPbfRnDMaster> _pidfPbfRnDMasterRepository { get; set; }
        private IRepository<PidfPbfRndBatchSize> _pidfPbfRndBatchSizeRepository { get; set; }
        private IRepository<PidfPbfRnDApirequirement> _pidfPbfRndApirequirementRepository { get; set; }
        private IRepository<PidfPbfRnDToolingChangepart> _pidfPbfRndToolingChangePartCostRepository { get; set; }
        private IRepository<PidfPbfRnDCapexMiscellaneousExpense> _pidfPbfRndCapexMiscellaneousExpenseRepository { get; set; }
        private IRepository<PidfPbfRnDPlantSupportCost> _pidfPbfRndPlantSupportCostRepository { get; set; }
        private IRepository<PidfPbfRnDReferenceProductDetail> _pidfPbfRndReferenceProductDetailRepository { get; set; }
        private IRepository<PidfPbfRnDFillingExpense> _pidfPbfRndFillingExpenseRepository { get; set; }

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
            _pidf_API_Charter_repository = _unitOfWork.GetRepository<PidfApiCharter>();
            _pidf_API_TimelineInMonth_repository = _unitOfWork.GetRepository<PidfApiCharterTimelineInMonth>();
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
            _PidfPbfAnalyticalCostsRepository = _unitOfWork.GetRepository<PidfPbfAnalyticalCost>();
            _masterDosageRepository = _unitOfWork.GetRepository<MasterDosage>();
            _pidfPbfClinicalRepository = _unitOfWork.GetRepository<PidfPbfClinical>();
            _pidfPbfClinicalPilotBioFastingRepository = _unitOfWork.GetRepository<PidfPbfClinicalPilotBioFasting>();
            _pidfPbfClinicalPilotBioFedRepository = _unitOfWork.GetRepository<PidfPbfClinicalPilotBioFed>();
            _pidfPbfClinicalPivotalBioFastingRepository = _unitOfWork.GetRepository<PidfPbfClinicalPivotalBioFasting>();
            _pidfPbfClinicalPivotalBioFedRepository = _unitOfWork.GetRepository<PidfPbfClinicalPivotalBioFed>();
            _pidfPbfClinicalCostRepository = _unitOfWork.GetRepository<PidfPbfClinicalCost>();
            _pidfPbfGeneralRepository = _unitOfWork.GetRepository<PidfPbfGeneral>();
            _masterFillingTypeRepository = _unitOfWork.GetRepository<MasterFilingType>();
            _pidfPbfMarketMappingRepository = _unitOfWork.GetRepository<PidfPbfMarketMapping>();
            _pidfPbfGeneralStrengthRepository = _unitOfWork.GetRepository<PidfPbfGeneralStrength>();
            _pidfPbfRnDRepository = _unitOfWork.GetRepository<PidfPbfRnD>();
            _pidfPbfRnDExicipientPrototype = _unitOfWork.GetRepository<PidfPbfRnDExicipientPrototype>();
            _pidfPbfAnalyticalCostStrengthMappingRepository = _unitOfWork.GetRepository<PidfPbfAnalyticalCostStrengthMapping>();
            _pidfPbfRnDExicipientRequirementRepository = _unitOfWork.GetRepository<PidfPbfRnDExicipientRequirement>();
            _pidfPbfRnDPackagingMaterialRepository = _unitOfWork.GetRepository<PidfPbfRnDPackagingMaterial>();
            _pidfPbfRnDMasterRepository = _unitOfWork.GetRepository<PidfPbfRnDMaster>();
            _pidfPbfRndBatchSizeRepository = _unitOfWork.GetRepository<PidfPbfRndBatchSize>();
            _pidfPbfRndApirequirementRepository = _unitOfWork.GetRepository<PidfPbfRnDApirequirement>();
            _pidfPbfRndToolingChangePartCostRepository = _unitOfWork.GetRepository<PidfPbfRnDToolingChangepart>();
            _pidfPbfRndCapexMiscellaneousExpenseRepository = _unitOfWork.GetRepository<PidfPbfRnDCapexMiscellaneousExpense>();
            _pidfPbfRndPlantSupportCostRepository = _unitOfWork.GetRepository<PidfPbfRnDPlantSupportCost>();
            _pidfPbfRndReferenceProductDetailRepository = _unitOfWork.GetRepository<PidfPbfRnDReferenceProductDetail>();
            _pidfPbfRndFillingExpenseRepository = _unitOfWork.GetRepository<PidfPbfRnDFillingExpense>();
            
        }


    public async Task<dynamic> FillDropdown(int PIDFId)
        {
            dynamic DropdownObjects = new ExpandoObject();

            var loggedInUserId = _helper.GetLoggedInUser().UserId;

            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", loggedInUserId),
                new SqlParameter("@PIDFId", PIDFId)
            };
            DataSet dsDropdownOptions = await _repository.GetDataSetBySP("SP_Fill_ddl_PBF", System.Data.CommandType.StoredProcedure, osqlParameter);
            DropdownObjects.MasterBusinessUnit = dsDropdownOptions.Tables[0];
            DropdownObjects.MasterBERequirement = dsDropdownOptions.Tables[1];
            DropdownObjects.MasterPlant = dsDropdownOptions.Tables[2];
            DropdownObjects.MasterWorkflow = dsDropdownOptions.Tables[3];
            DropdownObjects.MasterDosage = dsDropdownOptions.Tables[4];
            DropdownObjects.MasterFilingType = dsDropdownOptions.Tables[5];
            DropdownObjects.MasterFormRnDDivision = dsDropdownOptions.Tables[6];
            DropdownObjects.MasterPackagingType = dsDropdownOptions.Tables[7];
            DropdownObjects.MasterManufacturing = dsDropdownOptions.Tables[8];
            DropdownObjects.MasterCountry = dsDropdownOptions.Tables[9];
            DropdownObjects.MasterProductType = dsDropdownOptions.Tables[10];
            DropdownObjects.MasterTestLicense = dsDropdownOptions.Tables[11];
            DropdownObjects.MasterFormulationGL = dsDropdownOptions.Tables[12];
            DropdownObjects.MasterAnalyticalGL = dsDropdownOptions.Tables[13];
            DropdownObjects.PIDFEntity = dsDropdownOptions.Tables[14];
            DropdownObjects.PIDFIPDEntity = dsDropdownOptions.Tables[15];
            DropdownObjects.PIDFStrengthEntity = dsDropdownOptions.Tables[16];
            DropdownObjects.MasterTestType = dsDropdownOptions.Tables[17];
            //DropdownObjects.PBFClinicalEntity = dsDropdownOptions.Tables[18];

            return DropdownObjects;
        }

        //public string FileValidation(IFormFile file)
        //{
        //    PIDFMedicalViewModel fileUpload = new PIDFMedicalViewModel();
        //    fileUpload.FileSize = Convert.ToInt32(_configuration.GetSection("FileUploadSettings").GetSection("MaxFileSizeMb").Value);
        //    try
        //    {
        //        var supportedTypes = _configuration.GetSection("FileUploadSettings").GetSection("AllowedFileExtension").Value;
        //        var fileTypes = supportedTypes.Split(',');
        //        var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
        //        if (!fileTypes.Contains(fileExt))
        //        {
        //            fileUpload.ErrorMessage = _configuration.GetSection("FileUploadSettings").GetSection("FileNotAllowedErrorMessage").Value;
        //        }
        //        else if (file.Length > (fileUpload.FileSize * 1024 * 1024))
        //        {
        //            fileUpload.ErrorMessage = _configuration.GetSection("FileUploadSettings").GetSection("FileSizeExceedErrorMessage").Value;
        //        }
        //        else
        //        {
        //            fileUpload.ErrorMessage = null;
        //        }
        //        return fileUpload.ErrorMessage;
        //    }
        //    catch (Exception ex)
        //    {
        //        fileUpload.ErrorMessage = "Upload Container Should Not Be Empty or Contact Admin";
        //        return fileUpload.ErrorMessage;
        //    }
        //}
        //public async Task FileUpload(IFormFile files, string path, string uniqueFileName)
        //{
        //    if (files != null)
        //    {
        //        string uploadFolder = path;
        //        if (!Directory.Exists(uploadFolder))
        //        {
        //            Directory.CreateDirectory(uploadFolder);
        //        }
        //        var filePath = Path.Combine(uploadFolder, uniqueFileName);
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await files.CopyToAsync(stream);
        //        }

        //    }
        //}

        //public async Task<dynamic> FillDropdown()
        //{
        //    dynamic DropdownObjects = new ExpandoObject();

        //    DropdownObjects.MasterOrals = _oralService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterUnitofMeasurements = _unitofMeasurementService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterDosageForms = _dosageFormService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterDosage = _masterDosageRepository.GetAll().Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterPackagingTypes = _packagingTypeService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterBusinessUnits = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterCountrys = _countryService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MarketExtensions = _masterMarketExtensionService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.InHouses = new List<InHouseEntity> { new InHouseEntity { InHouseId = 1, InHouseName = "Yes" }, new InHouseEntity { InHouseId = 2, InHouseName = "No" } };
        //    DropdownObjects.MasterAPISourcing = _APISourcingService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterDIAs = _masterDIAService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterBERequirements = _masterBERequirementService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterProductType = _masterProductTypeService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterPlantService = _masterPlantService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterWorkflowService = _masterWorkflowService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterFormRNDDivisionService = _masterFormRNDDivisionService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterFormulationService = _masterFormulationService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterAnalyticalGLService = _masterAnalyticalGLService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterTestType = _masterTestTypeService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    DropdownObjects.MasterTestLicense = _masterTestLicenseService.GetAll().Result.Where(xx => xx.IsActive).ToList();

        //    return DropdownObjects;
        //}
        //public async Task<DBOperation> AddUpdatePBF(PidfPbfEntity pbfEntity)
        //{
        //    return DBOperation.Success;
        //}
        //// t		

        //public async Task<PidfPbfEntity> GetPbfFormData(long pidfId, int buid, int? strengthid)
        //{
        //    PidfPbfRnDEntity pidfPbfRnDEntity = new PidfPbfRnDEntity();
        //    Expression<Func<PidfPbf, bool>> expr;
        //    if (strengthid != null)
        //    {
        //        expr = u => u.Pidfid == pidfId;
        //    }
        //    else
        //    {
        //        expr = u => u.Pidfid == pidfId;
        //    }
        //    //var data = _mapperFactory.Get<Pidfipd, PIDFormEntity>(await _repository.GetAsync(id));

        //    dynamic objData = (dynamic)await _pbfRepository.FindAllAsync(expr);
        //    var data = new PidfPbfEntity();
        //    data.MasterBusinessUnitEntities = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    pidfPbfRnDEntity.MasterStrengthEntities = _productStrengthService.GetAll().Result.Where(x => x.Pidfid == pidfId).ToList();
        //    data.pidfPbfRndEntity = pidfPbfRnDEntity;
        //    data.BusinessUnitId = buid;
        //    data.Pidfid = pidfId;
        //    Pidf objPidf = await _repository.GetAsync(pidfId);
        //    data.StatusId = objPidf.StatusId;

        //    return data;
        //}

        //public async Task<PidfPbfAnalyticalEntity> GetPBFAnalyticalReadonlyData(long pidfid)
        //{
        //    PidfPbfAnalyticalEntity PAE = new PidfPbfAnalyticalEntity();
        //    PAE.ProductStrength = _pidfProductStrengthService.GetAll().Result.Where(x => x.Pidfid == pidfid).ToList();
        //    var objpidf = _repository.GetAll().Where(x => x.Pidfid == pidfid).FirstOrDefault();
        //    PAE.ProjectName = objpidf.MoleculeName;
        //    PAE.SAPProjectProjectCode = "NTD";
        //    PAE.ImprintingEmbossingCodes = "NTD";
        //    return PAE;
        //}


        //public async Task<DBOperation> AddUpdatePBFDetailsAnalytical(PidfPbfFormEntity pbfEntity)
        //{
        //    try
        //    {
        //        //Dummy function to same PIDFPBF Data
        //        long pidfpbfid = 0;
        //        PidfPbf objPIDFPbf;
        //        var loggedInUserId = _helper.GetLoggedInUser().UserId;
        //        pidfpbfid = await SavePidfAndPBFCommanDetails(pbfEntity.Pidfid, pbfEntity.PbfFormEntities);

        //        if (pidfpbfid > 0)
        //        {
        //           // var objPIDFPbfclincial = _pidfPbfClinicalRepository.GetAll().Where(x => x.Pbfid == pidfpbfid & x.BusinessUnitId == pbfEntity.BusinessUnitId).FirstOrDefault();
        //            var objPIDFPbfanalytical = _pidfPbfAnalyticalRepository.GetAll().Where(x => x.Pbfid == pidfpbfid & x.BusinessUnitId == pbfEntity.BusinessUnitId).FirstOrDefault();

        //            //For Updating PBF Analytical
        //            if (objPIDFPbfanalytical != null)
        //                {
        //                    //PidfPbfAnalytical _previousPbfAnalyticalEntity = _mapperFactory.Get<PidfPbfAnalyticalEntity, PidfPbfAnalytical>(pbfEntity.PidfPbfAnalyticals);
        //                    //_pidfPbfAnalyticalRepository.UpdateAsync(_previousPbfAnalyticalEntity);
        //                    //await _unitOfWork.SaveChangesAsync();

        //                    var prototypeDetails = _pidfPbfAnalyticalPrototypeRepository.GetAll().Where(x => x.PbfanalyticalId == objPIDFPbfanalytical.PbfanalyticalId).ToList();
        //                    if (prototypeDetails.Count > 0)
        //                    {
        //                        foreach (var item in prototypeDetails)
        //                        {
        //                            _pidfPbfAnalyticalPrototypeRepository.Remove(item);
        //                        }
        //                        await _unitOfWork.SaveChangesAsync();
        //                    }


        //                    var exhibitDetails = _pidfPbfAnalyticalExhibitRepository.GetAll().Where(x => x.PbfanalyticalId == objPIDFPbfanalytical.PbfanalyticalId).ToList();
        //                    if (exhibitDetails.Count > 0)
        //                    {
        //                        foreach (var item in exhibitDetails)
        //                        {
        //                            _pidfPbfAnalyticalExhibitRepository.Remove(item);
        //                        }
        //                        await _unitOfWork.SaveChangesAsync();
        //                    }


        //                    var scaleupDetails = _pidfPbfAnalyticalScaleUpRepository.GetAll().Where(x => x.PbfanalyticalId == objPIDFPbfanalytical.PbfanalyticalId).ToList();
        //                    if (scaleupDetails.Count > 0)
        //                    {
        //                        foreach (var item in scaleupDetails)
        //                        {
        //                            _pidfPbfAnalyticalScaleUpRepository.Remove(item);
        //                        }
        //                        await _unitOfWork.SaveChangesAsync();
        //                    }


        //                }                     
        //                await SaveChildDetailsAnalytical(pidfpbfid, loggedInUserId, pbfEntity.PidfPbfAnalyticals);

        //                var isSuccess = await _auditLogService.CreateAuditLog<PidfPbfFormEntity>(pbfEntity.Pidfid > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
        //                   Utility.Enums.ModuleEnum.PBF, pbfEntity, pbfEntity, Convert.ToInt32(pbfEntity.Pidfid));
        //                await _unitOfWork.SaveChangesAsync();
        //                var _StatusID = (pbfEntity.SaveSubmitType == "Save") ? Master_PIDFStatus.PBFSubmitted : Master_PIDFStatus.PBFInProgress;
        //                await _auditLogService.UpdatePIDFStatusCommon(pbfEntity.Pidfid, (int)_StatusID, loggedInUserId);

        //                return DBOperation.Success;
        //            }
        //            else
        //            {
        //                return DBOperation.NotFound;
        //            }


        //        //else
        //        //{

        //        //    var _objpidfpbfadd = new PidfPbf();
        //        //    _objpidfpbfadd.Pidfid = pbfEntity.Pidfid;
        //        //    _objpidfpbfadd.ProjectName = pbfEntity.ProjectName;
        //        //    _objpidfpbfadd.Market = pbfEntity.Market;
        //        //    _objpidfpbfadd.BusinessRelationable = pbfEntity.BusinessRelationable;
        //        //    _objpidfpbfadd.BerequirementId = pbfEntity.BerequirementId;
        //        //    _objpidfpbfadd.NumberOfApprovedAnda = pbfEntity.NumberOfApprovedAnda;
        //        //    _objpidfpbfadd.ProductTypeId = pbfEntity.ProductTypeId;
        //        //    _objpidfpbfadd.PlantId = pbfEntity.PlantId;
        //        //    _objpidfpbfadd.WorkflowId = pbfEntity.WorkflowId;
        //        //    _objpidfpbfadd.DosageId = pbfEntity.DosageId;
        //        //    _objpidfpbfadd.PatentStatus = pbfEntity.PatentStatus;
        //        //    _objpidfpbfadd.SponsorBusinessPartner = pbfEntity.SponsorBusinessPartner;
        //        //    _objpidfpbfadd.FormRnDdivisionId = pbfEntity.FormRnDdivisionId;
        //        //    _objpidfpbfadd.ProjectInitiationDate = pbfEntity.ProjectInitiationDate;
        //        //    _objpidfpbfadd.RnDhead = pbfEntity.RnDhead;
        //        //    _objpidfpbfadd.ProjectManager = pbfEntity.ProjectManager;
        //        //    _objpidfpbfadd.PackagingTypeId = (int)pbfEntity.PackagingTypeId;
        //        //    _objpidfpbfadd.DosageFormulationDetail = pbfEntity.DosageFormulationDetail;
        //        //    //_objpidfpbfadd.ManufacturingId = pbfEntity.manufeturingId;
        //        //    _objpidfpbfadd.CreatedBy = loggedInUserId;
        //        //    _objpidfpbfadd.CreatedDate = DateTime.Now;
        //        //    _pbfRepository.AddAsync(_objpidfpbfadd);

        //        //    await _unitOfWork.SaveChangesAsync();
        //        //    await SaveChildDetails(_objpidfpbfadd.Pidfpbfid, loggedInUserId, pbfEntity.PidfPbfAnalyticals, pbfEntity.PidfPbfClinicals, pbfEntity.pidfPbfRndEntity);
        //        //    var _StatusID = (pbfEntity.SaveSubmitType == "Save") ? Master_PIDFStatus.PBFSubmitted : Master_PIDFStatus.PBFInProgress;
        //        //    await _auditLogService.UpdatePIDFStatusCommon(pbfEntity.Pidfid, (int)_StatusID, loggedInUserId);
        //        //    return DBOperation.Success;

        //        //}
        //    }
        //    catch (Exception ex)
        //    {

        //        return DBOperation.Error;
        //    }
        //}

        //private async Task<bool> SaveChildDetailsAnalytical(long Pidfpbfid, int loggedInUserId, PidfPbfAnalyticalEntity analyticalEntites)
        //{
        //    try
        //    {

        //        //if (analyticalEntites != null && analyticalEntites.PidfPbfAnalyticalPrototypes.Count() > 0 && analyticalEntites.PidfPbfAnalyticalScaleUps.Count() > 0 && analyticalEntites.PidfPbfAnalyticalExhibits.Count() > 0)

        //        var NewAnalyticalId = 0;
        //        if (analyticalEntites.StrengthId > 0 && analyticalEntites.BusinessUnitId > 0)//for checking which tab is selected
        //        {
        //            if (analyticalEntites.PBFAnalyticalID > 0)
        //            {
        //                var objpidfpbfupdate = _pidfPbfAnalyticalRepository.GetAll().First(x => x.PbfanalyticalId == analyticalEntites.PBFAnalyticalID);
        //                if (objpidfpbfupdate != null)
        //                {

        //                    var _objpidfpbfadd = new PidfPbfAnalytical();
        //                    objpidfpbfupdate.Pbfid = Pidfpbfid;
        //                    objpidfpbfupdate.Pidfid = analyticalEntites.PIDFID;
        //                    objpidfpbfupdate.BusinessUnitId = analyticalEntites.BusinessUnitId;
        //                    objpidfpbfupdate.TotalExpense = analyticalEntites.TotalExpense;
        //                    objpidfpbfupdate.ProjectComplexity = analyticalEntites.ProjectComplexity;
        //                    objpidfpbfupdate.StrengthId = analyticalEntites.StrengthId;
        //                    objpidfpbfupdate.ProductTypeId = analyticalEntites.ProductTypeId;
        //                    objpidfpbfupdate.TestLicenseAvailability = analyticalEntites.TestLicenseAvailability;
        //                    objpidfpbfupdate.BudgetTimelineSubmissionDate = analyticalEntites.BudgetTimelineSubmissionDate;
        //                    objpidfpbfupdate.FormulationId = analyticalEntites.FormulationId;
        //                    objpidfpbfupdate.AnalyticalId = analyticalEntites.AnalyticalId;
        //                    _pidfPbfAnalyticalRepository.UpdateAsync(objpidfpbfupdate);
        //                    //return DBOperation.Success;
        //                    NewAnalyticalId = (int)objpidfpbfupdate.PbfanalyticalId;
        //                }
        //                else
        //                {
        //                    //return DBOperation.NotFound;
        //                }


        //            }
        //            else
        //            {
        //                var _objpidfpbfadd = new PidfPbfAnalytical();
        //                _objpidfpbfadd.Pbfid = Pidfpbfid;
        //                _objpidfpbfadd.Pidfid = analyticalEntites.PIDFID;
        //                _objpidfpbfadd.BusinessUnitId = analyticalEntites.BusinessUnitId;
        //                _objpidfpbfadd.TotalExpense = analyticalEntites.TotalExpense;
        //                _objpidfpbfadd.ProjectComplexity = analyticalEntites.ProjectComplexity;
        //                _objpidfpbfadd.StrengthId = analyticalEntites.StrengthId;
        //                _objpidfpbfadd.ProductTypeId = analyticalEntites.ProductTypeId;
        //                _objpidfpbfadd.TestLicenseAvailability = analyticalEntites.TestLicenseAvailability;
        //                _objpidfpbfadd.BudgetTimelineSubmissionDate = analyticalEntites.BudgetTimelineSubmissionDate;
        //                _objpidfpbfadd.FormulationId = analyticalEntites.FormulationId;
        //                _objpidfpbfadd.AnalyticalId = analyticalEntites.AnalyticalId;
        //                _objpidfpbfadd.CreatedBy = loggedInUserId;
        //                _objpidfpbfadd.CreatedDate = DateTime.Now;
        //                _pidfPbfAnalyticalRepository.AddAsync(_objpidfpbfadd);
        //                await _unitOfWork.SaveChangesAsync();
        //                NewAnalyticalId = (int)_objpidfpbfadd.PbfanalyticalId;
        //            }
        //                //Save Prototype Table
        //                if (analyticalEntites.PidfPbfAnalyticalPrototypes != null && analyticalEntites.PidfPbfAnalyticalPrototypes.Count() > 0)
        //                {
        //                    List<PidfPbfAnalyticalPrototype> _objAnalyticalprototype = new List<PidfPbfAnalyticalPrototype>();
        //                    foreach (var item in analyticalEntites.PidfPbfAnalyticalPrototypes)
        //                    {

        //                        PidfPbfAnalyticalPrototype analyticalprototype = new PidfPbfAnalyticalPrototype();
        //                        analyticalprototype.PbfanalyticalId = NewAnalyticalId;
        //                        analyticalprototype.StrengthId = analyticalEntites.StrengthId;
        //                        analyticalprototype.TestTypeId = (int)item.TestTypeId;
        //                        analyticalprototype.Numberoftests = item.Numberoftests;
        //                        analyticalprototype.PrototypeDevelopment = item.PrototypeDevelopment;
        //                        analyticalprototype.Cost = item.Cost;
        //                        analyticalprototype.PrototypeCost = item.PrototypeCost;
        //                        analyticalprototype.CreatedDate = DateTime.Now;
        //                        analyticalprototype.CreatedBy = loggedInUserId;

        //                        //analyticalprototype = _mapperFactory.Get<PidfPbfAnalyticalPrototypeEntity, PidfPbfAnalyticalPrototype>(item);
        //                        _objAnalyticalprototype.Add(analyticalprototype);
        //                    }
        //                    _pidfPbfAnalyticalPrototypeRepository.AddRangeAsync(_objAnalyticalprototype);
        //                    await _unitOfWork.SaveChangesAsync();
        //                }
        //                //Save ScaleUp Table
        //                if (analyticalEntites.PidfPbfAnalyticalScaleUps != null && analyticalEntites.PidfPbfAnalyticalScaleUps.Count() > 0)
        //                {
        //                    List<PidfPbfAnalyticalScaleUp> _objAnalyticalscaleup = new List<PidfPbfAnalyticalScaleUp>();
        //                    foreach (var item in analyticalEntites.PidfPbfAnalyticalScaleUps)
        //                    {

        //                        PidfPbfAnalyticalScaleUp analyticalscaleup = new PidfPbfAnalyticalScaleUp();
        //                        analyticalscaleup.PbfanalyticalId = NewAnalyticalId;
        //                        analyticalscaleup.StrengthId = analyticalEntites.StrengthId;
        //                        analyticalscaleup.TestTypeId = (int)item.TestTypeId;
        //                        analyticalscaleup.Numberoftests = item.Numberoftests;
        //                        analyticalscaleup.PrototypeDevelopment = item.PrototypeDevelopment;
        //                        analyticalscaleup.Cost = item.Cost;
        //                        analyticalscaleup.PrototypeCost = item.PrototypeCost;
        //                        analyticalscaleup.CreatedDate = DateTime.Now;
        //                        analyticalscaleup.CreatedBy = loggedInUserId;
        //                        //analyticalscaleup = _mapperFactory.Get<PidfPbfAnalyticalScaleUpEntity, PidfPbfAnalyticalScaleUp>(item);
        //                        _objAnalyticalscaleup.Add(analyticalscaleup);
        //                    }
        //                    _pidfPbfAnalyticalScaleUpRepository.AddRangeAsync(_objAnalyticalscaleup);
        //                    await _unitOfWork.SaveChangesAsync();
        //                }
        //                //Save Exhibit Table
        //                if (analyticalEntites.PidfPbfAnalyticalExhibits != null && analyticalEntites.PidfPbfAnalyticalExhibits.Count() > 0)
        //                {
        //                    List<PidfPbfAnalyticalExhibit> _objAnalyticalexhibit = new List<PidfPbfAnalyticalExhibit>();
        //                    foreach (var item in analyticalEntites.PidfPbfAnalyticalExhibits)
        //                    {

        //                        PidfPbfAnalyticalExhibit analyticalexhibit = new PidfPbfAnalyticalExhibit();
        //                        analyticalexhibit.PbfanalyticalId = NewAnalyticalId;
        //                        analyticalexhibit.StrengthId = analyticalEntites.StrengthId;
        //                        analyticalexhibit.TestTypeId = (int)item.TestTypeId;
        //                        analyticalexhibit.Numberoftests = item.Numberoftests;
        //                        analyticalexhibit.PrototypeDevelopment = item.PrototypeDevelopment;
        //                        analyticalexhibit.Cost = item.Cost;
        //                        analyticalexhibit.PrototypeCost = item.PrototypeCost;
        //                        analyticalexhibit.CreatedDate = DateTime.Now;
        //                        analyticalexhibit.CreatedBy = loggedInUserId;
        //                        //analyticalexhibit = _mapperFactory.Get<PidfPbfAnalyticalExhibitEntity, PidfPbfAnalyticalExhibit>(item);
        //                        _objAnalyticalexhibit.Add(analyticalexhibit);
        //                    }
        //                    _pidfPbfAnalyticalExhibitRepository.AddRangeAsync(_objAnalyticalexhibit);
        //                    await _unitOfWork.SaveChangesAsync();
        //                }
        //                //Save Cost Table
        //                if (analyticalEntites.PidfPbfAnalyticalCosts != null)
        //                {
        //                    PidfPbfAnalyticalCost analyticalcost = new PidfPbfAnalyticalCost();
        //                    analyticalcost.PbfanalyticalId = NewAnalyticalId;
        //                    analyticalcost.StrengthId = analyticalEntites.StrengthId;
        //                    analyticalcost.TotalAmvcost = analyticalEntites.PidfPbfAnalyticalCosts.TotalAMVCost;
        //                    analyticalcost.Remark = analyticalEntites.PidfPbfAnalyticalCosts.Remark;
        //                    analyticalcost.TotalPrototypeCost = analyticalEntites.PidfPbfAnalyticalCosts.TotalPrototypeCost;
        //                    analyticalcost.TotalScaleupCost = analyticalEntites.PidfPbfAnalyticalCosts.TotalScaleUpCost;
        //                    analyticalcost.TotalExhibitCost = analyticalEntites.PidfPbfAnalyticalCosts.TotalExhibitCost;
        //                    analyticalcost.TotalCost = analyticalEntites.PidfPbfAnalyticalCosts.TotalCost;
        //                    analyticalcost.CreatedDate = DateTime.Now;
        //                    analyticalcost.CreatedBy = loggedInUserId;
        //                    //analyticalexhibit = _mapperFactory.Get<PidfPbfAnalyticalExhibitEntity, PidfPbfAnalyticalExhibit>(item);
        //                    _PidfPbfAnalyticalCostsRepository.AddAsync(analyticalcost);
        //                    await _unitOfWork.SaveChangesAsync();
        //                }
        //        }



        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}




        //public async Task<PidfPbfFormEntity> GetPbfFormDetailsAnalytical(long pidfId, int buid, int strengthid)
        //{
        //    var data = await GetPbfTabDetailsAnalytical(pidfId, buid, strengthid);
        //    return data;

        //}

        //public async Task<PidfPbfFormEntity> GetPbfTabDetailsAnalytical(long pidfId, int buid, int strengthid)
        //{

        //    //PBF Entity Mapping
        //    var data = new PidfPbfFormEntity();
        //    data.MasterBusinessUnitEntities = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //    data.MasterStrengthEntities = _productStrengthService.GetAll().Result.Where(x => x.Pidfid == pidfId).ToList();

        //    if (data.MasterStrengthEntities.Count > 0 && data.MasterStrengthEntities != null)
        //        strengthid = (strengthid == 0) ? (int)data.MasterStrengthEntities[0].PidfproductStrengthId : strengthid;
        //    //analytical Tables
        //    PBFFormEntity _PbfaformEntity = new PBFFormEntity();
        //    PidfPbfAnalyticalEntity _pidfPbfanalyticalEntity = new PidfPbfAnalyticalEntity();           
        //    List<PidfPbfAnalyticalPrototypeEntity> objPrototypeList = new();
        //    List<PidfPbfAnalyticalExhibitEntity> objExhibitList = new();
        //    List<PidfPbfAnalyticalScaleUpEntity> objScaleUpList = new();
        //    PidfPbfAnalyticalCostEntity objanalyticalcost = new();

        //    SqlParameter[] osqlParameter = {
        //        new SqlParameter("@PIDFID", pidfId),
        //        new SqlParameter("@BUSINESSUNITId", buid),
        //        new SqlParameter("@STRENGTHId", strengthid)
        //    };

        //    var dbresult = await _pbfRepository.GetDataSetBySP("stp_npd_GetPbfData_Analytical", System.Data.CommandType.StoredProcedure, osqlParameter);

        //    dynamic pbf = new ExpandoObject();
        //    dynamic pidf = new ExpandoObject();
        //    dynamic analytical = new ExpandoObject();
        //   // dynamic clinical = new ExpandoObject();
        //    dynamic anlyticalprototype = new ExpandoObject();
        //    dynamic anlyticalscaleup = new ExpandoObject();
        //    dynamic analyticalexhibit = new ExpandoObject();
        //    dynamic analyticalcost = new ExpandoObject();

        //    if (dbresult != null)
        //    {
        //        if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
        //        {
        //            //var objPidf = UtilityHelper.ConvertDataTable<PidfPbfFormEntity>(dbresult.Tables[0]);
        //           // pidf = dbresult.Tables[0].DataTableToList<PidfPbfFormEntity>();
        //            pbf = dbresult.Tables[0].DataTableToList<PBFFormEntity>();
        //            if (pbf.Count > 0)
        //            {
        //                var _pidfEntity = _repository.GetAll().Where(x => x.Pidfid == pidfId).FirstOrDefault();
        //                //PBF Entity Mapping
        //                data.MasterBusinessUnitEntities = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //                //pidfPbfRnDEntity.MasterStrengthEntities = _productStrengthService.GetAll().Result.Where(x => x.Pidfid == pidfId).ToList();
        //                data.MasterStrengthEntities = _productStrengthService.GetAll().Result.Where(x => x.Pidfid == pidfId).ToList();
        //                //data.pidfPbfRndEntity = pidfPbfRnDEntity;
        //                data.BusinessUnitId = buid;
        //                //data.StrengthId = strengthid;
        //                data.Pidfid = pidfId;
        //                foreach (var item in pbf)
        //                {
        //                    data.Pidfpbfid = item.Pidfpbfid;
        //                    _PbfaformEntity.Pidfpbfid = item.Pidfpbfid;
        //                    _PbfaformEntity.ProductTypeId = item.ProductTypeId;
        //                    _PbfaformEntity.ProjectName = item.ProjectName;
        //                    _PbfaformEntity.Market = item.Market;
        //                    _PbfaformEntity.BusinessRelationable = item.BusinessRelationable;
        //                    _PbfaformEntity.BerequirementId = item.BerequirementId;
        //                    _PbfaformEntity.NumberOfApprovedAnda = item.NumberOfApprovedAnda;
        //                    _PbfaformEntity.ProductTypeId = item.ProductTypeId;
        //                    _PbfaformEntity.PlantId = item.PlantId;
        //                    _PbfaformEntity.WorkflowId = item.WorkflowId;
        //                    _PbfaformEntity.DosageId = item.DosageId;
        //                    _PbfaformEntity.PatentStatus = item.PatentStatus;
        //                    _PbfaformEntity.SponsorBusinessPartner = item.SponsorBusinessPartner;
        //                    _PbfaformEntity.FormRnDdivisionId = item.FormRnDdivisionId;
        //                    _PbfaformEntity.ProjectInitiationDate = item.ProjectInitiationDate;
        //                    _PbfaformEntity.RnDhead = item.RnDhead;
        //                    _PbfaformEntity.ProjectManager = item.ProjectManager;
        //                    _PbfaformEntity.DosageFormulationDetail = item.DosageFormulationDetail;
        //                    _PbfaformEntity.PackagingTypeId = item.PackagingTypeId;
        //                    _PbfaformEntity.BrandName = _pidfEntity.BrandName;
        //                    _PbfaformEntity.RFDIndication = _pidfEntity.Rfdindication;
        //                    _PbfaformEntity.RFDApplicant = _pidfEntity.Rfdapplicant;
        //                    _PbfaformEntity.RFDCountryId = _pidfEntity.RfdcountryId;
        //                }
        //                data.PbfFormEntities = _PbfaformEntity;

        //                //data.PidfPbfAnalyticals = _pidfPbfanalyticalEntity;
        //                //data.PidfPbfClinicals = _pidfPbfclinicalEntity;


        //            }
        //            //analytical table mapping
        //            analytical = dbresult.Tables[1].DataTableToList<PidfPbfAnalyticalEntity>();
        //            if (analytical.Count > 0)
        //            {
        //                foreach (var item in analytical)
        //                {
        //                    _pidfPbfanalyticalEntity.PBFAnalyticalID = item.PBFAnalyticalID;
        //                    _pidfPbfanalyticalEntity.PIDFID = item.PIDFID;
        //                    _pidfPbfanalyticalEntity.BusinessUnitId = item.BusinessUnitId;
        //                    _pidfPbfanalyticalEntity.TotalExpense = item.TotalExpense;
        //                    _pidfPbfanalyticalEntity.ProjectComplexity = item.ProjectComplexity;
        //                    _pidfPbfanalyticalEntity.ProductTypeId = item.ProductTypeId;
        //                    _pidfPbfanalyticalEntity.TestLicenseAvailability = item.TestLicenseAvailability;
        //                    _pidfPbfanalyticalEntity.BudgetTimelineSubmissionDate = item.BudgetTimelineSubmissionDate;
        //                    _pidfPbfanalyticalEntity.FormulationId = (int)item.FormulationId;
        //                    _pidfPbfanalyticalEntity.StrengthId = (int)item.StrengthId;
        //                    _pidfPbfanalyticalEntity.AnalyticalId = (int)item.AnalyticalId;
        //                }
        //                data.PidfPbfAnalyticals = _pidfPbfanalyticalEntity;
        //                //prototype table mapping
        //                anlyticalprototype = dbresult.Tables[2].DataTableToList<PidfPbfAnalyticalPrototypeEntity>();
        //                if (anlyticalprototype.Count > 0)
        //                {
        //                    //List<PidfPbfAnalyticalPrototypeEntity> objPrototypeList = new();
        //                    foreach (var item in anlyticalprototype)
        //                    {
        //                        PidfPbfAnalyticalPrototypeEntity objprototype = new();
        //                        objprototype.PrototypeId = item.PrototypeId;
        //                        objprototype.PBFAnalyticalId = item.PBFAnalyticalId;
        //                        objprototype.StrengthId = item.StrengthId;
        //                        objprototype.TestTypeId = item.TestTypeId;
        //                        objprototype.Numberoftests = item.Numberoftests;
        //                        objprototype.PrototypeDevelopment = item.PrototypeDevelopment;
        //                        objprototype.Cost = item.Cost;
        //                        objprototype.PrototypeCost = item.PrototypeCost;
        //                        //objprototype.CreatedDate = item.CreatedDate;
        //                        //objprototype.CreatedBy = item.CreatedBy;

        //                        objPrototypeList.Add(objprototype);
        //                    }
        //                    data.PidfPbfAnalyticals.PidfPbfAnalyticalPrototypes = objPrototypeList;

        //                }
        //                //scaleup table mapping

        //                anlyticalscaleup = dbresult.Tables[3].DataTableToList<PidfPbfAnalyticalScaleUpEntity>();
        //                if (anlyticalscaleup.Count > 0)
        //                {
        //                    foreach (var item in anlyticalscaleup)
        //                    {
        //                        PidfPbfAnalyticalScaleUpEntity objscaleup = new();
        //                        objscaleup.ScaleUpId = item.ScaleUpId;
        //                        objscaleup.PBFAnalyticalId = item.PBFAnalyticalId;
        //                        objscaleup.StrengthId = (int)item.StrengthId;
        //                        objscaleup.TestTypeId = (int)item.TestTypeId;
        //                        objscaleup.Numberoftests = item.Numberoftests;
        //                        objscaleup.PrototypeDevelopment = item.PrototypeDevelopment;
        //                        objscaleup.Cost = item.Cost;
        //                        objscaleup.PrototypeCost = item.PrototypeCost;
        //                        //objscaleup.CreatedDate = item.CreatedDate;
        //                        //objscaleup.CreatedBy = item.CreatedBy;

        //                        objScaleUpList.Add(objscaleup);
        //                    }
        //                    data.PidfPbfAnalyticals.PidfPbfAnalyticalScaleUps = objScaleUpList;
        //                }
        //                //exhibit table mapping

        //                analyticalexhibit = dbresult.Tables[4].DataTableToList<PidfPbfAnalyticalExhibitEntity>();
        //                if (analyticalexhibit.Count > 0)
        //                {
        //                    foreach (var item in analyticalexhibit)
        //                    {
        //                        PidfPbfAnalyticalExhibitEntity objexhibit = new();
        //                        objexhibit.ExhibitId = item.ExhibitId;
        //                        objexhibit.PBFAnalyticalId = item.PBFAnalyticalId;
        //                        objexhibit.StrengthId = item.StrengthId;
        //                        objexhibit.TestTypeId = item.TestTypeId;
        //                        objexhibit.Numberoftests = item.Numberoftests;
        //                        objexhibit.PrototypeDevelopment = item.PrototypeDevelopment;
        //                        objexhibit.Cost = item.Cost;
        //                        objexhibit.PrototypeCost = item.PrototypeCost;
        //                        //objexhibit.CreatedDate = item.CreatedDate;
        //                        //objexhibit.CreatedBy = item.CreatedBy;

        //                        objExhibitList.Add(objexhibit);
        //                    }
        //                    data.PidfPbfAnalyticals.PidfPbfAnalyticalExhibits = objExhibitList;
        //                }
        //                //cost table mapping
        //                analyticalcost = dbresult.Tables[5].DataTableToList<PidfPbfAnalyticalCostEntity>();
        //                //PidfPbfAnalyticalCostEntity analyticalcost = new();
        //                if (analyticalcost.Count > 0)
        //                {
        //                    foreach (var item in analyticalcost)
        //                    {

        //                        objanalyticalcost.PBFAnalyticalCostId = (int)item.PBFAnalyticalCostId;
        //                        objanalyticalcost.StrengthId = (int)item.StrengthId;
        //                        objanalyticalcost.TotalAMVCost = item.TotalAMVCost;
        //                        objanalyticalcost.Remark = item.Remark;
        //                        objanalyticalcost.TotalPrototypeCost = item.TotalPrototypeCost;
        //                        objanalyticalcost.TotalScaleUpCost = item.TotalScaleUpCost;
        //                        objanalyticalcost.TotalExhibitCost = item.TotalExhibitCost;
        //                        objanalyticalcost.TotalCost = item.TotalCost;

        //                    }

        //                }
        //                data.PidfPbfAnalyticals.PidfPbfAnalyticalCosts = objanalyticalcost;
        //            }

        //        }
        //    }
        //    return data;

        //}

        //public async Task<DBOperation> AddUpdatePBFClinicalDetails(PIDFPBFClinicalFormEntity pbfEntity)
        //{
        //    try
        //    {
        //        //Dummy function to same PIDFPBF Data
        //        long pbfgeneralid = 0;
        //        PidfPbf objPIDFPbf;
        //        var loggedInUserId = _helper.GetLoggedInUser().UserId;
        //        pbfgeneralid = await SavePidfAndPBFCommanDetails(pbfEntity.Pidfid, pbfEntity.PbfFormEntities);

        //        if (pbfgeneralid > 0)
        //        {
        //            var pilotBioFasting = _pidfPbfClinicalPilotBioFastingRepository.GetAll().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
        //            if (pilotBioFasting.Count > 0)
        //            {
        //                foreach (var item in pilotBioFasting)
        //                {
        //                    _pidfPbfClinicalPilotBioFastingRepository.Remove(item);
        //                }
        //                await _unitOfWork.SaveChangesAsync();
        //            }


        //            var pilotBioFed = _pidfPbfClinicalPilotBioFedRepository.GetAll().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
        //            if (pilotBioFed.Count > 0)
        //            {
        //                foreach (var item in pilotBioFed)
        //                {
        //                    _pidfPbfClinicalPilotBioFedRepository.Remove(item);
        //                }
        //                await _unitOfWork.SaveChangesAsync();
        //            }


        //            var pivotalBioFasting = _pidfPbfClinicalPivotalBioFastingRepository.GetAll().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
        //            if (pivotalBioFasting.Count > 0)
        //            {
        //                foreach (var item in pivotalBioFasting)
        //                {
        //                    _pidfPbfClinicalPivotalBioFastingRepository.Remove(item);
        //                }
        //                await _unitOfWork.SaveChangesAsync();
        //            }


        //            var pivotalBioFed = _pidfPbfClinicalPivotalBioFedRepository.GetAll().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
        //            if (pivotalBioFed.Count > 0)
        //            {
        //                foreach (var item in pivotalBioFed)
        //                {
        //                    _pidfPbfClinicalPivotalBioFedRepository.Remove(item);
        //                }
        //                await _unitOfWork.SaveChangesAsync();
        //            }

        //            var ClinicalCost = _pidfPbfClinicalCostRepository.GetAll().Where(x => x.Pbfgeneralld == pbfgeneralid).ToList();
        //            if (ClinicalCost.Count > 0)
        //            {
        //                foreach (var item in ClinicalCost)
        //                {
        //                    _pidfPbfClinicalCostRepository.Remove(item);
        //                }
        //                await _unitOfWork.SaveChangesAsync();
        //            }

        //            await SaveChildClinicalDetails(pbfgeneralid, loggedInUserId, pbfEntity.PidfPbfClinicals);

        //            var isSuccess = await _auditLogService.CreateAuditLog<PIDFPBFClinicalFormEntity>(pbfEntity.Pidfid > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
        //               Utility.Enums.ModuleEnum.PBF, pbfEntity, pbfEntity, Convert.ToInt32(pbfEntity.Pidfid));
        //            await _unitOfWork.SaveChangesAsync();
        //            var _StatusID = (pbfEntity.SaveSubmitType == "Save") ? Master_PIDFStatus.PBFSubmitted : Master_PIDFStatus.PBFInProgress;
        //            await _auditLogService.UpdatePIDFStatusCommon(pbfEntity.Pidfid, (int)_StatusID, loggedInUserId);

        //            return DBOperation.Success;
        //        }
        //        else
        //        {
        //            return DBOperation.Error;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return DBOperation.Error;
        //    }
        //}

        //private async Task<bool> SaveChildClinicalDetails(long pbfgeneralid, int loggedInUserId, PidfPbfClinicalEntity clinicalEntites)
        //{
        //    try
        //    {

        //        //Save pilot Bio Fasting Table
        //        if (clinicalEntites.pidfpbfClinicalpilotBioFastingEntity != null && clinicalEntites.pidfpbfClinicalpilotBioFastingEntity.Count() > 0)
        //        {
        //            List<PidfPbfClinicalPilotBioFasting> _objclinicalPilotBioFasting = new List<PidfPbfClinicalPilotBioFasting>();
        //            foreach (var item in clinicalEntites.pidfpbfClinicalpilotBioFastingEntity)
        //            {

        //                PidfPbfClinicalPilotBioFasting clinicalPilotBioFasting = new PidfPbfClinicalPilotBioFasting();
        //                clinicalPilotBioFasting.PbfgeneralId = pbfgeneralid;
        //                clinicalPilotBioFasting.StrengthId = clinicalEntites.StrengthId;
        //                clinicalPilotBioFasting.Fasting = item.Fasting;
        //                clinicalPilotBioFasting.NumberofVolunteers = item.NumberofVolunteers;
        //                clinicalPilotBioFasting.ClinicalCostandVol = item.ClinicalCostandVol;
        //                clinicalPilotBioFasting.DocCostandStudy = item.DocCostandStudy;
        //                clinicalPilotBioFasting.TotalCost = item.TotalCost;
        //                clinicalPilotBioFasting.CreatedDate = DateTime.Now;
        //                clinicalPilotBioFasting.CreatedBy = loggedInUserId;

        //                //clinicalPilotBioFasting = _mapperFactory.Get<PidfPbfClinicalPilotBioFastingEntity, PidfPbfClinicalPilotBioFasting>(item);
        //                _objclinicalPilotBioFasting.Add(clinicalPilotBioFasting);
        //            }
        //            _pidfPbfClinicalPilotBioFastingRepository.AddRangeAsync(_objclinicalPilotBioFasting);
        //            await _unitOfWork.SaveChangesAsync();
        //        }

        //        //Save pilot Bio Fed Table
        //        if (clinicalEntites.pidfpbfClinicalPilotBioFedEntity != null && clinicalEntites.pidfpbfClinicalPilotBioFedEntity.Count() > 0)
        //        {
        //            List<PidfPbfClinicalPilotBioFed> _objclinicalPilotBioFed = new List<PidfPbfClinicalPilotBioFed>();
        //            foreach (var item in clinicalEntites.pidfpbfClinicalPilotBioFedEntity)
        //            {

        //                PidfPbfClinicalPilotBioFed clinicalPilotBioFed = new PidfPbfClinicalPilotBioFed();
        //                clinicalPilotBioFed.PbfgeneralId = pbfgeneralid;
        //                clinicalPilotBioFed.StrengthId = clinicalEntites.StrengthId;
        //                clinicalPilotBioFed.Fed = item.Fed;
        //                clinicalPilotBioFed.NumberofVolunteers = item.NumberofVolunteers;
        //                clinicalPilotBioFed.ClinicalCostandVol = item.ClinicalCostandVol;
        //                clinicalPilotBioFed.DocCostandStudy = item.DocCostandStudy;
        //                clinicalPilotBioFed.TotalCost = (decimal)item.TotalCost;
        //                clinicalPilotBioFed.CreatedDate = DateTime.Now;
        //                clinicalPilotBioFed.CreatedBy = loggedInUserId;

        //                //clinicalPilotBioFed = _mapperFactory.Get<PidfPbfClinicalPilotBioFedEntity, PidfPbfClinicalPilotBioFed>(item);
        //                _objclinicalPilotBioFed.Add(clinicalPilotBioFed);
        //            }
        //            _pidfPbfClinicalPilotBioFedRepository.AddRangeAsync(_objclinicalPilotBioFed);
        //            await _unitOfWork.SaveChangesAsync();
        //        }
        //        //Save pivotal Bio Fasting Table
        //        if (clinicalEntites.pidfpbfClinicalPivotalBioFastingEntity != null && clinicalEntites.pidfpbfClinicalPivotalBioFastingEntity.Count() > 0)
        //        {
        //            List<PidfPbfClinicalPivotalBioFasting> _objclinicalPivotalBioFasting = new List<PidfPbfClinicalPivotalBioFasting>();
        //            foreach (var item in clinicalEntites.pidfpbfClinicalPivotalBioFastingEntity)
        //            {

        //                PidfPbfClinicalPivotalBioFasting clinicalPivotalBioFasting = new PidfPbfClinicalPivotalBioFasting();
        //                clinicalPivotalBioFasting.PbfgeneralId = pbfgeneralid;
        //                clinicalPivotalBioFasting.StrengthId = clinicalEntites.StrengthId;
        //                clinicalPivotalBioFasting.Fasting = item.Fasting;
        //                clinicalPivotalBioFasting.NumberofVolunteers = item.NumberofVolunteers;
        //                clinicalPivotalBioFasting.ClinicalCostandVol = item.ClinicalCostandVol;
        //                clinicalPivotalBioFasting.DocCostandStudy = item.DocCostandStudy;
        //                clinicalPivotalBioFasting.TotalCost = item.TotalCost;
        //                clinicalPivotalBioFasting.CreatedDate = DateTime.Now;
        //                clinicalPivotalBioFasting.CreatedBy = loggedInUserId;

        //                //clinicalPivotalBioFasting = _mapperFactory.Get<PidfPbfClinicalPivotalBioFastingEntity, PidfPbfClinicalPivotalBioFasting>(item);
        //                _objclinicalPivotalBioFasting.Add(clinicalPivotalBioFasting);
        //            }
        //            _pidfPbfClinicalPivotalBioFastingRepository.AddRangeAsync(_objclinicalPivotalBioFasting);
        //            await _unitOfWork.SaveChangesAsync();
        //        }

        //        //Save pivotal Bio Fed Table
        //        if (clinicalEntites.pidfpbfClinicalPivotalBioFedEntity != null && clinicalEntites.pidfpbfClinicalPivotalBioFedEntity.Count() > 0)
        //        {
        //            List<PidfPbfClinicalPivotalBioFed> _objclinicalPivotalBioFed = new List<PidfPbfClinicalPivotalBioFed>();
        //            foreach (var item in clinicalEntites.pidfpbfClinicalPivotalBioFedEntity)
        //            {

        //                PidfPbfClinicalPivotalBioFed clinicalPivotalBioFed = new PidfPbfClinicalPivotalBioFed();
        //                clinicalPivotalBioFed.PbfgeneralId = pbfgeneralid;
        //                clinicalPivotalBioFed.StrengthId = clinicalEntites.StrengthId;
        //                clinicalPivotalBioFed.Fed = item.Fed;
        //                clinicalPivotalBioFed.NumberofVolunteers = item.NumberofVolunteers;
        //                clinicalPivotalBioFed.ClinicalCostandVol = item.ClinicalCostandVol;
        //                clinicalPivotalBioFed.DocCostandStudy = item.DocCostandStudy;
        //                clinicalPivotalBioFed.TotalCost = item.TotalCost;
        //                clinicalPivotalBioFed.CreatedDate = DateTime.Now;
        //                clinicalPivotalBioFed.CreatedBy = loggedInUserId;

        //                // clinicalPivotalBioFed = _mapperFactory.Get<PidfPbfClinicalPivotalBioFedEntity, PidfPbfClinicalPivotalBioFed>(item);
        //                _objclinicalPivotalBioFed.Add(clinicalPivotalBioFed);
        //            }
        //            _pidfPbfClinicalPivotalBioFedRepository.AddRangeAsync(_objclinicalPivotalBioFed);
        //            await _unitOfWork.SaveChangesAsync();
        //        }
        //        //Save Cost Table
        //        if (clinicalEntites.pidfPbfClinicalCost != null)
        //        {
        //            PidfPbfClinicalCost clinicalcost = new PidfPbfClinicalCost();
        //            clinicalcost.Pbfgeneralld = pbfgeneralid;
        //            clinicalcost.StrengthId = clinicalEntites.StrengthId;
        //            clinicalcost.TotalPilotFastingCost = clinicalEntites.pidfPbfClinicalCost.TotalPilotFastingCost;
        //            clinicalcost.TotalPilotFedcost = clinicalEntites.pidfPbfClinicalCost.TotalPilotFEDCost;
        //            clinicalcost.TotalPivotalFastingCost = clinicalEntites.pidfPbfClinicalCost.TotalPivotalFastingCost;
        //            clinicalcost.TotalPivotalFedcost = clinicalEntites.pidfPbfClinicalCost.TotalPivotalFEDCost;
        //            clinicalcost.TotalCost = clinicalEntites.pidfPbfClinicalCost.TotalCost;
        //            clinicalcost.CreatedDate = DateTime.Now;
        //            clinicalcost.CreatedBy = loggedInUserId;
        //            //analyticalexhibit = _mapperFactory.Get<PidfPbfAnalyticalExhibitEntity, PidfPbfAnalyticalExhibit>(item);
        //            _pidfPbfClinicalCostRepository.AddAsync(clinicalcost);
        //            await _unitOfWork.SaveChangesAsync();
        //        }

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}




        //public async Task<PIDFPBFClinicalFormEntity> GetPbfClinicalFormDetails(long pidfId, int buid, long? strengthid)
        //{
        //    var data = await GetPbfTabDetails(pidfId, buid, strengthid);
        //    return data;

        //}
        //public async Task<PIDFPBFClinicalFormEntity> GetPbfTabDetails(long pidfId, int buid, long? strengthid)
        //{
        //    //PBF Entity Mapping
        //    var data = new PIDFPBFClinicalFormEntity();
        //    //analytical Tables
        //    PBFFormEntity _PbfaformEntity = new PBFFormEntity();

        //    //Clinical Tables
        //    PidfPbfGeneralEntity _pidfPbfgeneralEntity = new PidfPbfGeneralEntity();
        //    PidfPbfClinicalEntity _pidfPbfclinicalEntity = new PidfPbfClinicalEntity();
        //    List<PidfPbfClinicalPilotBioFastingEntity> _objclinicalPilotBioFasting = new();
        //    List<PidfPbfClinicalPilotBioFedEntity> _objclinicalPilotBioFed = new();
        //    List<PidfPbfClinicalPivotalBioFastingEntity> _objclinicalPivotalBioFasting = new();
        //    List<PidfPbfClinicalPivotalBioFedEntity> _objclinicalPivotalBioFed = new();
        //    PidfPbfClinicalCostEntity _obgclinicalcost = new();
        //    SqlParameter[] osqlParameter = {
        //        new SqlParameter("@PIDFID", pidfId),
        //        new SqlParameter("@BUSINESSUNITId", buid)
        //    };

        //    var dbresult = await _pbfRepository.GetDataSetBySP("stp_npd_GetPbfData_Clinical", System.Data.CommandType.StoredProcedure, osqlParameter);

        //    dynamic pbf = new ExpandoObject();
        //    dynamic General = new ExpandoObject();
        //    dynamic clinicalpilotbiofasting = new ExpandoObject();
        //    dynamic clinicalpilotbiofed = new ExpandoObject();
        //    dynamic clinicalpivotalbiofasting = new ExpandoObject();
        //    dynamic clinicalpivotalbiofed = new ExpandoObject();
        //    dynamic clinicalcost = new ExpandoObject();
        //    if (dbresult != null)
        //    {
        //        if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
        //        {
        //            //var objPidf = UtilityHelper.ConvertDataTable<PidfPbfFormEntity>(dbresult.Tables[0]);
        //            pbf = dbresult.Tables[0].DataTableToList<PBFFormEntity>();
        //            if (pbf.Count > 0)
        //            {
        //                var _pidfEntity = _repository.GetAll().Where(x => x.Pidfid == pidfId).FirstOrDefault();
        //                //PBF Entity Mapping
        //                data.MasterBusinessUnitEntities = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList();
        //                //pidfPbfRnDEntity.MasterStrengthEntities = _productStrengthService.GetAll().Result.Where(x => x.Pidfid == pidfId).ToList();
        //                data.MasterStrengthEntities = _productStrengthService.GetAll().Result.Where(x => x.Pidfid == pidfId).ToList();
        //                //data.pidfPbfRndEntity = pidfPbfRnDEntity;
        //                data.BusinessUnitId = buid;
        //                data.StrengthId = strengthid;
        //                data.Pidfid = pidfId;
        //                foreach (var item in pbf)
        //                {
        //                    data.Pidfpbfid = item.Pidfpbfid;
        //                    _PbfaformEntity.Pidfpbfid = item.Pidfpbfid;
        //                    _PbfaformEntity.ProductTypeId = item.ProductTypeId;
        //                    _PbfaformEntity.ProjectName = item.ProjectName;
        //                    _PbfaformEntity.Market = item.Market;
        //                    _PbfaformEntity.BusinessRelationable = item.BusinessRelationable;
        //                    _PbfaformEntity.BerequirementId = item.BerequirementId;
        //                    _PbfaformEntity.NumberOfApprovedAnda = item.NumberOfApprovedAnda;
        //                    _PbfaformEntity.ProductTypeId = item.ProductTypeId;
        //                    _PbfaformEntity.PlantId = item.PlantId;
        //                    _PbfaformEntity.WorkflowId = item.WorkflowId;
        //                    _PbfaformEntity.DosageId = item.DosageId;
        //                    _PbfaformEntity.PatentStatus = item.PatentStatus;
        //                    _PbfaformEntity.SponsorBusinessPartner = item.SponsorBusinessPartner;
        //                    _PbfaformEntity.FormRnDdivisionId = item.FormRnDdivisionId;
        //                    _PbfaformEntity.ProjectInitiationDate = item.ProjectInitiationDate;
        //                    _PbfaformEntity.RnDhead = item.RnDhead;
        //                    _PbfaformEntity.ProjectManager = item.ProjectManager;
        //                    _PbfaformEntity.DosageFormulationDetail = item.DosageFormulationDetail;
        //                    _PbfaformEntity.PackagingTypeId = item.PackagingTypeId;
        //                    _PbfaformEntity.BrandName = _pidfEntity.BrandName;
        //                    _PbfaformEntity.RFDIndication = _pidfEntity.Rfdindication;
        //                    _PbfaformEntity.RFDApplicant = _pidfEntity.Rfdapplicant;
        //                    _PbfaformEntity.RFDCountryId = _pidfEntity.RfdcountryId;




        //                }
        //                data.PbfFormEntities = _PbfaformEntity;

        //                //data.PidfPbfAnalyticals = _pidfPbfanalyticalEntity;
        //                //data.PidfPbfClinicals = _pidfPbfclinicalEntity;


        //            }

        //            //clinical table mapping
        //            //General = dbresult.Tables[1].DataTableToList<PidfPbfClinicalEntity>();
        //            //if (General.Count > 0)
        //            //{
        //            //    foreach (var item in General)
        //            //    {
        //            //        _pidfPbfclinicalEntity.PBFClinicalID = item.PBFClinicalID;
        //            //        _pidfPbfclinicalEntity.PIDFID = item.PIDFID;
        //            //        _pidfPbfclinicalEntity.BusinessUnitId = item.BusinessUnitId;
        //            //        _pidfPbfclinicalEntity.TotalExpense = item.TotalExpense;
        //            //        _pidfPbfclinicalEntity.ProjectComplexity = item.ProjectComplexity;
        //            //        _pidfPbfclinicalEntity.ProductTypeId = item.ProductTypeId;
        //            //        _pidfPbfclinicalEntity.TestLicenseAvailability = item.TestLicenseAvailability;
        //            //        _pidfPbfclinicalEntity.BudgetTimelineSubmissionDate = item.BudgetTimelineSubmissionDate;
        //            //        _pidfPbfclinicalEntity.FormulationId = (int)item.FormulationId;
        //            //        _pidfPbfclinicalEntity.StrengthId = (int)item.StrengthId;
        //            //        _pidfPbfclinicalEntity.AnalyticalId = (int)item.AnalyticalId;
        //            //    }
        //            //    data.PidfPbfClinicals = _pidfPbfclinicalEntity;
        //            //}
        //            //Pilot Bio Fasting Table
        //            clinicalpilotbiofasting = dbresult.Tables[2].DataTableToList<PidfPbfClinicalPilotBioFastingEntity>();
        //            if (clinicalpilotbiofasting.Count > 0)
        //            {
        //                foreach (var item in clinicalpilotbiofasting)
        //                {

        //                    PidfPbfClinicalPilotBioFastingEntity clinicalPilotBioFasting = new();
        //                    clinicalPilotBioFasting.PBFClinicalId = item.PBFClinicalId;
        //                    clinicalPilotBioFasting.PilotBioFastingId = item.PilotBioFastingId;
        //                    clinicalPilotBioFasting.StrengthId = item.StrengthId;
        //                    clinicalPilotBioFasting.Fasting = item.Fasting;
        //                    clinicalPilotBioFasting.NumberofVolunteers = item.NumberofVolunteers;
        //                    clinicalPilotBioFasting.ClinicalCostandVol = item.ClinicalCostandVol;
        //                    clinicalPilotBioFasting.DocCostandStudy = item.DocCostandStudy;
        //                    clinicalPilotBioFasting.TotalCost = item.TotalCost;
        //                    //clinicalPilotBioFasting.CreatedDate = DateTime.Now;
        //                    //clinicalPilotBioFasting.CreatedBy = loggedInUserId;

        //                    _objclinicalPilotBioFasting.Add(clinicalPilotBioFasting);
        //                }

        //                data.PidfPbfClinicals.pidfpbfClinicalpilotBioFastingEntity = strengthid.HasValue && strengthid > 0 ? _objclinicalPilotBioFasting.Where(x => x.StrengthId == strengthid).ToList().Count > 0 ? _objclinicalPilotBioFasting : null : null;
        //            }
        //            //Pilot Bio Fed Table
        //            clinicalpilotbiofed = dbresult.Tables[3].DataTableToList<PidfPbfClinicalPilotBioFedEntity>();
        //            if (clinicalpilotbiofed.Count > 0)
        //            {

        //                foreach (var item in clinicalpilotbiofed)
        //                {

        //                    PidfPbfClinicalPilotBioFedEntity clinicalPilotBioFed = new();
        //                    clinicalPilotBioFed.PilotBioFedid = item.PilotBioFedid;
        //                    clinicalPilotBioFed.PBFClinicalId = item.PBFClinicalId;
        //                    clinicalPilotBioFed.StrengthId = item.StrengthId;
        //                    clinicalPilotBioFed.Fed = item.Fed;
        //                    clinicalPilotBioFed.NumberofVolunteers = item.NumberofVolunteers;
        //                    clinicalPilotBioFed.ClinicalCostandVol = item.ClinicalCostandVol;
        //                    clinicalPilotBioFed.DocCostandStudy = item.DocCostandStudy;
        //                    clinicalPilotBioFed.TotalCost = item.TotalCost;
        //                    //clinicalPilotBioFed.CreatedDate = DateTime.Now;
        //                    //clinicalPilotBioFed.CreatedBy = loggedInUserId;

        //                    _objclinicalPilotBioFed.Add(clinicalPilotBioFed);
        //                }

        //                data.PidfPbfClinicals.pidfpbfClinicalPilotBioFedEntity = strengthid.HasValue && strengthid > 0 ? _objclinicalPilotBioFed.Where(x => x.StrengthId == strengthid).ToList().Count > 0 ? _objclinicalPilotBioFed : null : null;
        //            }
        //            //Pilot Bio Fasting Table
        //            clinicalpivotalbiofasting = dbresult.Tables[4].DataTableToList<PidfPbfClinicalPivotalBioFastingEntity>();
        //            if (clinicalpivotalbiofasting.Count > 0)
        //            {

        //                foreach (var item in clinicalpivotalbiofasting)
        //                {

        //                    PidfPbfClinicalPivotalBioFastingEntity clinicalPivotalBioFasting = new();
        //                    clinicalPivotalBioFasting.PBFClinicalId = item.PBFClinicalId;
        //                    clinicalPivotalBioFasting.PivotalBioFastingId = item.PivotalBioFastingId;
        //                    clinicalPivotalBioFasting.StrengthId = item.StrengthId;
        //                    clinicalPivotalBioFasting.Fasting = item.Fasting;
        //                    clinicalPivotalBioFasting.NumberofVolunteers = item.NumberofVolunteers;
        //                    clinicalPivotalBioFasting.ClinicalCostandVol = item.ClinicalCostandVol;
        //                    clinicalPivotalBioFasting.DocCostandStudy = item.DocCostandStudy;
        //                    clinicalPivotalBioFasting.TotalCost = item.TotalCost;
        //                    //clinicalPivotalBioFasting.CreatedDate = DateTime.Now;
        //                    //clinicalPivotalBioFasting.CreatedBy = loggedInUserId;

        //                    _objclinicalPivotalBioFasting.Add(clinicalPivotalBioFasting);
        //                }

        //                data.PidfPbfClinicals.pidfpbfClinicalPivotalBioFastingEntity = strengthid.HasValue && strengthid > 0 ? _objclinicalPivotalBioFasting.Where(x => x.StrengthId == strengthid).ToList().Count > 0 ? _objclinicalPivotalBioFasting : null : null;
        //            }
        //            //Pilot Bio Fed Table
        //            clinicalpivotalbiofed = dbresult.Tables[5].DataTableToList<PidfPbfClinicalPivotalBioFedEntity>();
        //            if (clinicalpivotalbiofed.Count > 0)
        //            {

        //                foreach (var item in clinicalpivotalbiofed)
        //                {
        //                    PidfPbfClinicalPivotalBioFedEntity clinicalPivotalBioFed = new();
        //                    clinicalPivotalBioFed.PBFClinicalId = item.PBFClinicalId;
        //                    clinicalPivotalBioFed.PivotalBioFedid = item.PivotalBioFedid;
        //                    clinicalPivotalBioFed.StrengthId = item.StrengthId;
        //                    clinicalPivotalBioFed.Fed = item.Fed;
        //                    clinicalPivotalBioFed.NumberofVolunteers = item.NumberofVolunteers;
        //                    clinicalPivotalBioFed.ClinicalCostandVol = item.ClinicalCostandVol;
        //                    clinicalPivotalBioFed.DocCostandStudy = item.DocCostandStudy;
        //                    clinicalPivotalBioFed.TotalCost = item.TotalCost;
        //                    //clinicalPivotalBioFed.CreatedDate = DateTime.Now;
        //                    //clinicalPivotalBioFed.CreatedBy = loggedInUserId;

        //                    _objclinicalPivotalBioFed.Add(clinicalPivotalBioFed);
        //                }

        //                data.PidfPbfClinicals.pidfpbfClinicalPivotalBioFedEntity = strengthid.HasValue && strengthid > 0 ? _objclinicalPivotalBioFed.Where(x => x.StrengthId == strengthid).ToList().Count > 0 ? _objclinicalPivotalBioFed : null : null;
        //            }
        //            //Cost Table
        //            clinicalcost = dbresult.Tables[6].DataTableToList<PidfPbfClinicalCostEntity>();
        //            if (clinicalcost.Count > 0)
        //            {


        //                foreach (var item in clinicalcost)
        //                {
        //                    if (strengthid.HasValue && strengthid.HasValue && strengthid > 0 && item.StrengthId == strengthid)
        //                    {
        //                        _obgclinicalcost.PBFClinicalCostId = (int)item.PBFClinicalCostId;
        //                        _obgclinicalcost.PBFClinicalId = (int)item.PBFClinicalId;
        //                        _obgclinicalcost.StrengthId = item.StrengthId;
        //                        _obgclinicalcost.TotalPilotFastingCost = item.TotalPilotFastingCost;
        //                        _obgclinicalcost.TotalPilotFEDCost = item.TotalPilotFEDCost;
        //                        _obgclinicalcost.TotalPivotalFastingCost = item.TotalPivotalFastingCost;
        //                        _obgclinicalcost.TotalPivotalFEDCost = item.TotalPivotalFEDCost;
        //                        _obgclinicalcost.TotalCost = item.TotalCost;
        //                    }

        //                }
        //                data.PidfPbfClinicals.pidfPbfClinicalCost = _obgclinicalcost;

        //            }
        //        }
        //    }
        //    return data;

        //}




        public async Task<PBFFormEntity> GetPbfFormDetails(long pidfId, int buid, int? strengthid)
        {
            var data = await GetPbfDetails(pidfId, buid, strengthid);
            return data;
        }
        public async Task<PBFFormEntity> GetPbfDetails(long pidfId, int buid, int? strengthid)
        {

            //PBF Entity Mapping
            var data = new PBFFormEntity();
            //data.MasterBusinessUnitEntities = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            //data.MasterStrengthEntities = _productStrengthService.GetAll().Result.Where(x => x.Pidfid == pidfId).ToList();
            data.BusinessUnitId = buid;
            data.Pidfid = pidfId;
            data.StrengthId = (int)strengthid;
            // PBF Entity Mapping

            PBFFormEntity _PbfaformEntity = new PBFFormEntity();
            SqlParameter[] osqlParameter = {
                    new SqlParameter("@PIDFID", pidfId),
                    new SqlParameter("@BUSINESSUNITId", buid)
                };

            var dbresult = await _pbfRepository.GetDataSetBySP("stp_npd_GetPbfData", System.Data.CommandType.StoredProcedure, osqlParameter);
            //dynamic pbf = new ExpandoObject();
            //dynamic General = new ExpandoObject();
            //dynamic General_Strength = new ExpandoObject();
            //dynamic pbfmarkettingmap = new ExpandoObject();           
            if (dbresult != null)
            {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                {
                    data = dbresult.Tables[0].DataTableToList<PBFFormEntity>()[0];
                }
                if (dbresult.Tables[1] != null && dbresult.Tables[1].Rows.Count > 0)
                {
                    data.GeneralStrengthEntities = dbresult.Tables[1].DataTableToList<GeneralStrengthEntity>();
                }
            }
            return data;
        }
        public async Task<DBOperation> AddUpdatePBFDetails(PBFFormEntity pbfEntity)
        {
            try
            {
                //Dummy function to same PIDFPBF Data
                long pbfgeneralid = 0;
                PidfPbf objPIDFPbf;
                var loggedInUserId = _helper.GetLoggedInUser().UserId;
                pbfgeneralid = await SavePidfAndPBFCommanDetails(pbfEntity.Pidfid, pbfEntity);

                if (pbfgeneralid > 0)
                {
                    var isSuccess = await _auditLogService.CreateAuditLog<PBFFormEntity>(pbfEntity.Pidfpbfid > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
                   Utility.Enums.ModuleEnum.PBF, pbfEntity, pbfEntity, Convert.ToInt32(pbfEntity.Pidfid));
                    await _unitOfWork.SaveChangesAsync();
                    var _StatusID = (pbfEntity.SaveType == "Save") ? Master_PIDFStatus.PBFSubmitted : Master_PIDFStatus.PBFInProgress;
                    await _auditLogService.UpdatePIDFStatusCommon(pbfEntity.Pidfid, (int)_StatusID, loggedInUserId);

                    return DBOperation.Success;
                }
                else
                {
                    return DBOperation.Error;
                }
            }
            catch (Exception ex)
            {
                return DBOperation.Error;
            }
        }
        #region saving RnD Details
        public async Task<DBOperation> AddUpdateRnD(PidfPbfGeneralEntity PidfPbfGeneralEntity)
        {
            //PidfPbfGeneral objpidfPbfGeneral;
            List<PidfPbfRnDExicipientPrototypeEntity> lspidfPbfRnDExicipientPrototypeEntity = new List<PidfPbfRnDExicipientPrototypeEntity>();
            var loggedInUserId = _helper.GetLoggedInUser().UserId;
            var pbfgeneralid = await SavePidfAndPBFCommanDetails(PidfPbfGeneralEntity.PIDFID, PidfPbfGeneralEntity.objPBFFormEntity);
            //objpidfPbfGeneral=_pidfPbfGeneralRepository.GetAll().Where(x=>x.PbfgeneralId== pbfgeneralid).First();

            var isSuccess = await _auditLogService.CreateAuditLog<PidfPbfGeneralEntity>(pbfgeneralid > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
                          Utility.Enums.ModuleEnum.PBF, PidfPbfGeneralEntity, PidfPbfGeneralEntity, Convert.ToInt32(pbfgeneralid));
            await _unitOfWork.SaveChangesAsync();
            var _StatusID = (PidfPbfGeneralEntity.SaveType == "Save") ? Master_PIDFStatus.PBFSubmitted : Master_PIDFStatus.PBFInProgress;
            await _auditLogService.UpdatePIDFStatusCommon(pbfgeneralid, (int)_StatusID, loggedInUserId);

            return DBOperation.Success;
        }
        #endregion
        public async Task<dynamic> PBFAllTabDetails(int PIDFId, int BUId)
        {
            dynamic DropdownObjects = new ExpandoObject();

            var loggedInUserId = _helper.GetLoggedInUser().UserId;

            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFId", PIDFId),
                new SqlParameter("@BUId", BUId)
            };
            DataSet dsDropdownOptions = await _repository.GetDataSetBySP("stp_npd_GetPbfAllTabData", System.Data.CommandType.StoredProcedure, osqlParameter);

            DropdownObjects.MasterTestType = dsDropdownOptions.Tables[0];
            DropdownObjects.MasterPackagingType = dsDropdownOptions.Tables[1];
            DropdownObjects.MasterBatchSize = dsDropdownOptions.Tables[2];
            DropdownObjects.MasterBusinessUnit = dsDropdownOptions.Tables[3];
            DropdownObjects.PIDFPBFGeneralStrength = dsDropdownOptions.Tables[4];
            DropdownObjects.PBFClinicalEntity = dsDropdownOptions.Tables[5];
            DropdownObjects.PBFAnalyticalEntity = dsDropdownOptions.Tables[6];
            DropdownObjects.PBFAnalyticalCostEntity = dsDropdownOptions.Tables[7];
            DropdownObjects.PBFRNDMasterEntity = dsDropdownOptions.Tables[8];
            DropdownObjects.PBFRNDExicipientEntity = dsDropdownOptions.Tables[9];
            DropdownObjects.PBFRNDPackagingEntity = dsDropdownOptions.Tables[10];
            DropdownObjects.PBFRNDBatchSize = dsDropdownOptions.Tables[11];
            DropdownObjects.PBFRNDAPIRequirement = dsDropdownOptions.Tables[12];
            DropdownObjects.PBFRNDToolingChangePart = dsDropdownOptions.Tables[13];
            DropdownObjects.PBFRNDCapexMiscExpenses = dsDropdownOptions.Tables[14];
            DropdownObjects.PBFRNDPlantSupportCost = dsDropdownOptions.Tables[15];
            DropdownObjects.PBFRNDReferenceProductDetail = dsDropdownOptions.Tables[16];
            DropdownObjects.PBFRNDFillingExpenses = dsDropdownOptions.Tables[17];
            
            return DropdownObjects;
        }
        #region Private Methods
        public async Task<long> SavePidfAndPBFCommanDetails(long pidfid, PBFFormEntity pbfentity)
        {
            long pidfpbfid = 0;
            long pbfgeneralid = 0;
            List<PidfPbfMarketMapping> objmapping = new();
            List<PidfPbfAnalyticalCostStrengthMapping> objACSMList = new();
            try
            {
                #region Section PBF Add Update
                var loggedInUserId = _helper.GetLoggedInUser().UserId;
                PidfPbf objPIDFPbf;
                Pidf objPIDFupdate;
                objPIDFPbf = _pbfRepository.GetAllQuery().Where(x => x.Pidfid == pbfentity.Pidfid).FirstOrDefault();
                if (objPIDFPbf != null)
                {
                    objPIDFPbf = _mapperFactory.Get<PBFFormEntity, PidfPbf>(pbfentity);
                    objPIDFPbf.ModifyBy = loggedInUserId;
                    objPIDFPbf.ModifyDate = DateTime.Now;
                    _pbfRepository.UpdateAsync(objPIDFPbf);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    objPIDFPbf = _mapperFactory.Get<PBFFormEntity, PidfPbf>(pbfentity);
                    objPIDFPbf.CreatedBy = loggedInUserId;
                    objPIDFPbf.CreatedDate = DateTime.Now;
                    _pbfRepository.AddAsync(objPIDFPbf);
                    await _unitOfWork.SaveChangesAsync();
                }
                pidfpbfid = objPIDFPbf.Pidfpbfid;
                #endregion

                #region Marketting Mapping Add Update
                if (pidfpbfid > 0 && pbfentity.MarketMappingId.Length > 0)
                {
                    var marketmapping = _pidfPbfMarketMappingRepository.GetAllQuery().Where(x => x.Pidfpbfid == pidfpbfid).ToList();
                    if (marketmapping.Count > 0)
                    {
                        foreach (var item in marketmapping)
                        {
                            _pidfPbfMarketMappingRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                    foreach (var item in pbfentity.MarketMappingId)
                    {
                        PidfPbfMarketMapping objMM = new();
                        objMM.BusinessUnitId = item;
                        objMM.Pidfpbfid = pidfpbfid;
                        objMM.CreatedBy = loggedInUserId;
                        objMM.CreatedDate = DateTime.Now;
                        objmapping.Add(objMM);
                    }
                    _pidfPbfMarketMappingRepository.AddRange(objmapping);
                    await _unitOfWork.SaveChangesAsync();
                }
                #endregion                

                #region Section PBF General Add Update
                PidfPbfGeneral objPIDFGeneralupdate;
                objPIDFGeneralupdate = _pidfPbfGeneralRepository.GetAllQuery().Where(x => x.Pidfpbfid == pbfentity.Pidfpbfid).FirstOrDefault();
                if (objPIDFGeneralupdate != null)
                {
                    objPIDFGeneralupdate = _mapperFactory.Get<PBFFormEntity, PidfPbfGeneral>(pbfentity);
                    objPIDFGeneralupdate.CreatedBy = loggedInUserId;
                    objPIDFGeneralupdate.CreatedDate = DateTime.Now;
                    _pidfPbfGeneralRepository.UpdateAsync(objPIDFGeneralupdate);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    objPIDFGeneralupdate = new PidfPbfGeneral();
                    objPIDFGeneralupdate = _mapperFactory.Get<PBFFormEntity, PidfPbfGeneral>(pbfentity);
                    objPIDFGeneralupdate.CreatedBy = loggedInUserId;
                    objPIDFGeneralupdate.CreatedDate = DateTime.Now;
                    _pidfPbfGeneralRepository.AddAsync(objPIDFGeneralupdate);
                    await _unitOfWork.SaveChangesAsync();

                }
                pbfgeneralid = objPIDFGeneralupdate.PbfgeneralId;

                #endregion

                #region Update PIDF
                objPIDFupdate = _repository.GetAllQuery().Where(x => x.Pidfid == pidfid).FirstOrDefault();
                //_repository.GetAll().Where(x => x.Pidfid == pidfid).FirstOrDefault();
                if (objPIDFupdate != null)
                {
                    objPIDFupdate.Rfdbrand = pbfentity.BrandName;
                    objPIDFupdate.Rfdindication = pbfentity.RFDIndication;
                    objPIDFupdate.Rfdapplicant = pbfentity.RFDApplicant;
                    objPIDFupdate.RfdcountryId = pbfentity.RFDCountryId;
                    objPIDFupdate.ModifyBy = loggedInUserId;
                    objPIDFupdate.ModifyDate = DateTime.Now;
                    _repository.UpdateAsync(objPIDFupdate);
                    await _unitOfWork.SaveChangesAsync();
                }
                #endregion

                #region GeneralProductStrength Add Update
                if (pbfgeneralid > 0)
                {
                    var generalStrength = _pidfPbfGeneralStrengthRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                    if (generalStrength.Count > 0)
                    {
                        foreach (var item in generalStrength)
                        {
                            _pidfPbfGeneralStrengthRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                }

                //Save General Strength Entities Table
                if (pbfentity.GeneralStrengthEntities != null && pbfentity.GeneralStrengthEntities.Count() > 0)
                {
                    List<PidfPbfGeneralStrength> _objPidfPbfGeneralStrength = new List<PidfPbfGeneralStrength>();
                    foreach (var item in pbfentity.GeneralStrengthEntities)
                    {
                        PidfPbfGeneralStrength pidfPbfGeneralStrength = new PidfPbfGeneralStrength();
                        pidfPbfGeneralStrength = _mapperFactory.Get<GeneralStrengthEntity, PidfPbfGeneralStrength>(item);
                        pidfPbfGeneralStrength.PbfgeneralId = pbfgeneralid;
                        pidfPbfGeneralStrength.CreatedDate = DateTime.Now;
                        pidfPbfGeneralStrength.CreatedBy = loggedInUserId;
                        _objPidfPbfGeneralStrength.Add(pidfPbfGeneralStrength);
                    }
                    _pidfPbfGeneralStrengthRepository.AddRangeAsync(_objPidfPbfGeneralStrength);
                    await _unitOfWork.SaveChangesAsync();
                }
                #endregion

                #region Section Clinical Add Update
                List<PidfPbfClinical> objClinicallist = new();
                if (pbfgeneralid > 0)
                {
                    var clinical = _pidfPbfClinicalRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                    if (clinical.Count > 0)
                    {
                        foreach (var item in clinical)
                        {
                            _pidfPbfClinicalRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                }

                //Save clinical Entities
                if (pbfentity.ClinicalEntities != null && pbfentity.ClinicalEntities.Count() > 0)
                {
                    foreach (var item in pbfentity.ClinicalEntities)
                    {
                        PidfPbfClinical obgclinical = new PidfPbfClinical();
                        obgclinical = _mapperFactory.Get<ClinicalEntity, PidfPbfClinical>(item);
                        obgclinical.PbfgeneralId = pbfgeneralid;
                        obgclinical.CreatedDate = DateTime.Now;
                        obgclinical.CreatedBy = loggedInUserId;
                        objClinicallist.Add(obgclinical);
                    }
                    _pidfPbfClinicalRepository.AddRangeAsync(objClinicallist);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion

                #region Section Analytical Add Update
                List<PidfPbfAnalytical> objAnalyticallist = new();
                if (pbfgeneralid > 0)
                {
                    var analytical = _pidfPbfAnalyticalRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                    if (analytical.Count > 0)
                    {
                        foreach (var item in analytical)
                        {
                            _pidfPbfAnalyticalRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                }

                //Save analytical Entities
                if (pbfentity.AnalyticalEntities != null && pbfentity.AnalyticalEntities.Count() > 0)
                {
                    foreach (var item in pbfentity.AnalyticalEntities)
                    {
                        PidfPbfAnalytical obganalytical = new PidfPbfAnalytical();
                        obganalytical = _mapperFactory.Get<AnalyticalEntity, PidfPbfAnalytical>(item);
                        obganalytical.PbfgeneralId = pbfgeneralid;
                        obganalytical.CreatedDate = DateTime.Now;
                        obganalytical.CreatedBy = loggedInUserId;
                        objAnalyticallist.Add(obganalytical);
                    }
                    _pidfPbfAnalyticalRepository.AddRangeAsync(objAnalyticallist);
                    await _unitOfWork.SaveChangesAsync();
                }
                //Save analytical cost start
                var analyticalcost = _PidfPbfAnalyticalCostsRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).FirstOrDefault();
                long analyticalCostId;
                if (analyticalcost != null)
                {
                    analyticalcost.TotalAmvcost = pbfentity.AMVCosts.TotalAmvcost;
                    analyticalcost.Remark = pbfentity.AMVCosts.Remark;
                    analyticalcost.CreatedDate = DateTime.Now;
                    analyticalcost.CreatedBy = loggedInUserId;
                    _PidfPbfAnalyticalCostsRepository.UpdateAsync(analyticalcost);
                    analyticalCostId = analyticalcost.PbfanalyticalCostId;
                }
                else
                {
                    PidfPbfAnalyticalCost obganalyticalcost = new PidfPbfAnalyticalCost();
                    obganalyticalcost = _mapperFactory.Get<AMVCost, PidfPbfAnalyticalCost>(pbfentity.AMVCosts);
                    obganalyticalcost.TotalAmvcost = pbfentity.AMVCosts.TotalAmvcost;
                    obganalyticalcost.Remark = pbfentity.AMVCosts.Remark;
                    obganalyticalcost.PbfgeneralId = pbfgeneralid;
                    obganalyticalcost.CreatedDate = DateTime.Now;
                    obganalyticalcost.CreatedBy = loggedInUserId;
                    _PidfPbfAnalyticalCostsRepository.AddAsync(obganalyticalcost);
                    analyticalCostId = obganalyticalcost.PbfanalyticalCostId;
                }
                await _unitOfWork.SaveChangesAsync();
                //Save analytical cost end

                //Save analytical cost strength mapping start
                if (pidfpbfid > 0 && pbfentity.MarketMappingId.Length > 0)
                {
                    var analyticalcoststrength = _pidfPbfAnalyticalCostStrengthMappingRepository.GetAllQuery().Where(x => x.PbfanalyticalCostId == analyticalCostId).ToList();
                    if (analyticalcoststrength.Count > 0)
                    {
                        foreach (var item in analyticalcoststrength)
                        {
                            _pidfPbfAnalyticalCostStrengthMappingRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                    foreach (var item in pbfentity.AMVCosts.StrengthId)
                    {
                        PidfPbfAnalyticalCostStrengthMapping objacsm = new();
                        objacsm.StrengthId = item;
                        objacsm.PbfanalyticalCostId = analyticalCostId;
                        objacsm.CreatedBy = loggedInUserId;
                        objacsm.CreatedDate = DateTime.Now;
                        objACSMList.Add(objacsm);
                    }
                    _pidfPbfAnalyticalCostStrengthMappingRepository.AddRange(objACSMList);
                    await _unitOfWork.SaveChangesAsync();
                }

                //Save analytical cost end
                #endregion


                #region RND Add Update

                #region RND Master Add Update                

                PidfPbfRnDMaster objrndMaster;
                objrndMaster = _pidfPbfRnDMasterRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).FirstOrDefault();
                if (objrndMaster != null)
                {
                    objrndMaster.BatchSizeId = pbfentity.RNDMasterEntities.BatchSizeId;
                    objrndMaster.ApirequirementMarketPrice = pbfentity.RNDMasterEntities.ApirequirementMarketPrice;
                    objrndMaster.ManHourRate = pbfentity.RNDMasterEntities.ManHourRate;
                    objrndMaster.PlanSupportCostRsPerDay = pbfentity.RNDMasterEntities.PlanSupportCostRsPerDay;
                    //objRndMaster.ModifyBy = loggedInUserId;
                    //objRndMaster.ModifyDate = DateTime.Now;
                    _pidfPbfRnDMasterRepository.UpdateAsync(objrndMaster);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    PidfPbfRnDMaster objRndMaster = new PidfPbfRnDMaster();
                    objRndMaster = _mapperFactory.Get<RNDMasterEntity, PidfPbfRnDMaster>(pbfentity.RNDMasterEntities);
                    objRndMaster.PbfgeneralId = pbfgeneralid;
                    objRndMaster.CreatedBy = loggedInUserId;
                    objRndMaster.CreatedDate = DateTime.Now;
                    _pidfPbfRnDMasterRepository.AddAsync(objRndMaster);
                    await _unitOfWork.SaveChangesAsync();
                }
                #endregion

                #region Batch Size Add Update
                List<PidfPbfRndBatchSize> objBatchSizelist = new();

                var batchsize = _pidfPbfRndBatchSizeRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (batchsize.Count > 0)
                {
                    foreach (var item in batchsize)
                    {
                        _pidfPbfRndBatchSizeRepository.Remove(item);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }


                //Save batch size Entities
                if (pbfentity.RNDBatchSizes != null && pbfentity.RNDBatchSizes.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDBatchSizes)
                    {
                        PidfPbfRndBatchSize objbatchsize = new PidfPbfRndBatchSize();
                        objbatchsize = _mapperFactory.Get<RNDBatchSize, PidfPbfRndBatchSize>(item);                      
                        objbatchsize.PbfgeneralId = pbfgeneralid;
                        objbatchsize.CreatedDate = DateTime.Now;
                        objbatchsize.CreatedBy = loggedInUserId;
                        objBatchSizelist.Add(objbatchsize);
                    }
                    _pidfPbfRndBatchSizeRepository.AddRangeAsync(objBatchSizelist);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion

                #region API Requirement Add Update
                List<PidfPbfRnDApirequirement> objApirequirementlist = new();

                var apirequirement = _pidfPbfRndApirequirementRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (apirequirement.Count > 0)
                {
                    foreach (var item in apirequirement)
                    {
                        _pidfPbfRndApirequirementRepository.Remove(item);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }


                //Save Api requirement Entities
                if (pbfentity.RNDApirequirements != null && pbfentity.RNDApirequirements.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDApirequirements)
                    {
                        PidfPbfRnDApirequirement objapirequirement = new PidfPbfRnDApirequirement();
                        objapirequirement = _mapperFactory.Get<RNDApirequirement, PidfPbfRnDApirequirement>(item);
                        objapirequirement.PbfgeneralId = pbfgeneralid;
                        objapirequirement.CreatedDate = DateTime.Now;
                        objapirequirement.CreatedBy = loggedInUserId;
                        objApirequirementlist.Add(objapirequirement);
                    }
                    _pidfPbfRndApirequirementRepository.AddRangeAsync(objApirequirementlist);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion
                #region RND Excipient Add Update
                List<PidfPbfRnDExicipientRequirement> objExicipientlist = new();
                if (pbfgeneralid > 0)
                {
                    var exicipient = _pidfPbfRnDExicipientRequirementRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                    if (exicipient.Count > 0)
                    {
                        foreach (var item in exicipient)
                        {
                            _pidfPbfRnDExicipientRequirementRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                }

                //Save Exicipient Entities
                if (pbfentity.RNDExicipients != null && pbfentity.RNDExicipients.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDExicipients)
                    {
                        PidfPbfRnDExicipientRequirement obgexcipient = new PidfPbfRnDExicipientRequirement();
                        obgexcipient = _mapperFactory.Get<RNDExicipient, PidfPbfRnDExicipientRequirement>(item);
                        obgexcipient.PbfgeneralId = pbfgeneralid;
                        obgexcipient.CreatedDate = DateTime.Now;
                        obgexcipient.CreatedBy = loggedInUserId;
                        objExicipientlist.Add(obgexcipient);
                    }
                    _pidfPbfRnDExicipientRequirementRepository.AddRangeAsync(objExicipientlist);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion
                #region RND Packaging Add Update
                List<PidfPbfRnDPackagingMaterial> objPackaginglist = new();
                if (pbfgeneralid > 0)
                {
                    var packging = _pidfPbfRnDPackagingMaterialRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                    if (packging.Count > 0)
                    {
                        foreach (var item in packging)
                        {
                            _pidfPbfRnDPackagingMaterialRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                }

                //Save Packaging Entities
                if (pbfentity.RNDPackagings != null && pbfentity.RNDPackagings.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDPackagings)
                    {
                        PidfPbfRnDPackagingMaterial obgpackaging = new PidfPbfRnDPackagingMaterial();
                        obgpackaging = _mapperFactory.Get<RNDPackaging, PidfPbfRnDPackagingMaterial>(item);
                        obgpackaging.PbfgeneralId = pbfgeneralid;
                        obgpackaging.CreatedDate = DateTime.Now;
                        obgpackaging.CreatedBy = loggedInUserId;
                        objPackaginglist.Add(obgpackaging);
                    }
                    _pidfPbfRnDPackagingMaterialRepository.AddRangeAsync(objPackaginglist);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion

                #region Tooling Change Part Add Update
                List<PidfPbfRnDToolingChangepart> objToolingChangePartCostlist = new();

                var toolongchangepart = _pidfPbfRndToolingChangePartCostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (toolongchangepart.Count > 0)
                {
                    foreach (var item in toolongchangepart)
                    {
                        _pidfPbfRndToolingChangePartCostRepository.Remove(item);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }


                //Save ToolingChangeparts Entities
                if (pbfentity.RNDToolingChangeparts != null && pbfentity.RNDToolingChangeparts.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDToolingChangeparts)
                    {
                        PidfPbfRnDToolingChangepart objtoolingcgangepart = new PidfPbfRnDToolingChangepart();
                        objtoolingcgangepart = _mapperFactory.Get<RNDToolingChangepart, PidfPbfRnDToolingChangepart>(item);
                        objtoolingcgangepart.PbfgeneralId = pbfgeneralid;
                        objtoolingcgangepart.CreatedDate = DateTime.Now;
                        objtoolingcgangepart.CreatedBy = loggedInUserId;
                        objToolingChangePartCostlist.Add(objtoolingcgangepart);
                    }
                    _pidfPbfRndToolingChangePartCostRepository.AddRangeAsync(objToolingChangePartCostlist);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion
                #region Capex and Miscellaneous Expenses Add Update
                List<PidfPbfRnDCapexMiscellaneousExpense> objCapexMiscellaneouslist = new();

                var capexandmiscellaneous = _pidfPbfRndCapexMiscellaneousExpenseRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (capexandmiscellaneous.Count > 0)
                {
                    foreach (var item in capexandmiscellaneous)
                    {
                        _pidfPbfRndCapexMiscellaneousExpenseRepository.Remove(item);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }


                //Save CapexMiscellaneousExpenses Entities
                if (pbfentity.RNDCapexMiscellaneousExpenses != null && pbfentity.RNDCapexMiscellaneousExpenses.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDCapexMiscellaneousExpenses)
                    {
                        PidfPbfRnDCapexMiscellaneousExpense objcapex = new PidfPbfRnDCapexMiscellaneousExpense();
                        objcapex = _mapperFactory.Get<RNDCapexMiscellaneousExpense, PidfPbfRnDCapexMiscellaneousExpense>(item);
                        objcapex.PbfgeneralId = pbfgeneralid;
                        objcapex.CreatedDate = DateTime.Now;
                        objcapex.CreatedBy = loggedInUserId;
                        objCapexMiscellaneouslist.Add(objcapex);
                    }
                    _pidfPbfRndCapexMiscellaneousExpenseRepository.AddRangeAsync(objCapexMiscellaneouslist);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion
                #region Plant Support Cost Add Update
                List<PidfPbfRnDPlantSupportCost> objPlantSupportCostlist = new();

                var plantsupportcost = _pidfPbfRndPlantSupportCostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (plantsupportcost.Count > 0)
                {
                    foreach (var item in plantsupportcost)
                    {
                        _pidfPbfRndPlantSupportCostRepository.Remove(item);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }


                //Save PlantSupportCosts Entities
                if (pbfentity.RNDPlantSupportCosts != null && pbfentity.RNDPlantSupportCosts.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDPlantSupportCosts)
                    {
                        PidfPbfRnDPlantSupportCost objplantsupportcost = new PidfPbfRnDPlantSupportCost();
                        objplantsupportcost = _mapperFactory.Get<RNDPlantSupportCost, PidfPbfRnDPlantSupportCost>(item);
                        objplantsupportcost.PbfgeneralId = pbfgeneralid;
                        objplantsupportcost.CreatedDate = DateTime.Now;
                        objplantsupportcost.CreatedBy = loggedInUserId;
                        objPlantSupportCostlist.Add(objplantsupportcost);
                    }
                    _pidfPbfRndPlantSupportCostRepository.AddRangeAsync(objPlantSupportCostlist);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion
                #region Reference Product Detail Add Update
                List<PidfPbfRnDReferenceProductDetail> objReferenceProductDetaillist = new();

                var referenceporduct = _pidfPbfRndReferenceProductDetailRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (referenceporduct.Count > 0)
                {
                    foreach (var item in referenceporduct)
                    {
                        _pidfPbfRndReferenceProductDetailRepository.Remove(item);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }


                //Save ReferenceProductDetail Entities
                if (pbfentity.RNDReferenceProductDetails != null && pbfentity.RNDReferenceProductDetails.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDReferenceProductDetails)
                    {
                        PidfPbfRnDReferenceProductDetail objreferenceporduct = new PidfPbfRnDReferenceProductDetail();
                        objreferenceporduct = _mapperFactory.Get<RNDReferenceProductDetail, PidfPbfRnDReferenceProductDetail>(item);
                        objreferenceporduct.PbfgeneralId = pbfgeneralid;
                        objreferenceporduct.CreatedDate = DateTime.Now;
                        objreferenceporduct.CreatedBy = loggedInUserId;
                        objReferenceProductDetaillist.Add(objreferenceporduct);
                    }
                    _pidfPbfRndReferenceProductDetailRepository.AddRangeAsync(objReferenceProductDetaillist);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion

                #region Filling Expenses Add Update
                List<PidfPbfRnDFillingExpense> objFillinfExpenseslist = new();

                var fillingexpenses = _pidfPbfRndFillingExpenseRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (fillingexpenses.Count > 0)
                {
                    foreach (var item in fillingexpenses)
                    {
                        _pidfPbfRndFillingExpenseRepository.Remove(item);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }


                //Save ReferenceProductDetail Entities
                if (pbfentity.RNDFillingExpenses != null && pbfentity.RNDFillingExpenses.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDFillingExpenses)
                    {
                        PidfPbfRnDFillingExpense objfillingexpense = new PidfPbfRnDFillingExpense();
                        objfillingexpense = _mapperFactory.Get<RNDFillingExpense, PidfPbfRnDFillingExpense>(item);
                        objfillingexpense.PbfgeneralId = pbfgeneralid;
                        objfillingexpense.CreatedDate = DateTime.Now;
                        objfillingexpense.CreatedBy = loggedInUserId;
                        objFillinfExpenseslist.Add(objfillingexpense);
                    }
                    _pidfPbfRndFillingExpenseRepository.AddRangeAsync(objFillinfExpenseslist);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion
                #endregion

                return pbfgeneralid;
            }
            catch (Exception ex)
            {
                return pbfgeneralid;
            }
        }
        #endregion
    }
}