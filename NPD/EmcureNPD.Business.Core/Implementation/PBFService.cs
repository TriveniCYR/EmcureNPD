﻿using Microsoft.Extensions.Configuration;
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
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private IRepository<PidfApiIpd> _pidf_API_IPD_repository { get; set; }
        private IRepository<PidfApiRnD> _pidf_API_RnD_repository { get; set; }
        private IRepository<PidfApiCharter> _pidf_API_Charter_repository { get; set; }
        private IRepository<PidfPbf> _pbfRepository { get; set; }

		private readonly IMasterAuditLogService _auditLogService;		

		private IRepository<Pidf> _repository { get; set; }	

		private IRepository<Pidfapidetail> _pidfApiRepository { get; set; }
		private IRepository<PidfproductStrength> _pidfProductStrength { get; set; }
		//Market Extension & In House

		public PBFService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IMasterOralService oralService, IMasterUnitofMeasurementService unitofMeasurementService, 
			IMasterDosageFormService dosageFormService, IMasterPackagingTypeService packagingTypeService, IMasterBusinessUnitService businessUnitService, 
			IMasterCountryService countryService, IMasterAPISourcingService masterAPISourcingService, IPidfApiDetailsService pidfApiDetailsService, 
			IPidfProductStrengthService pidfProductStrengthService, IMasterDIAService masterDium, IMasterMarketExtensionService masterMarketExtensionService,
			IMasterBERequirementService masterBERequirementService, IMasterProductTypeService masterProductTypeService, IMasterPlantService masterPlantService,
			IMasterWorkflowService masterWorkflowService, IMasterFormRNDDivisionService masterFormRNDDivisionService, IMasterFormulationService masterFormulationService,
			IMasterAnalyticalGLService masterAnalyticalGLService, IPidfProductStrengthService productStrengthService, Microsoft.Extensions.Configuration.IConfiguration configuration,
           
        IMasterAuditLogService auditLogService)
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

		bool IskeepLastFile=(jsonObject.MarketDetailsFileName==null || jsonObject.MarketDetailsFileName == "") ? false : true;	

            if ((_oAPIIPD_Form.Files == null || _oAPIIPD_Form.Files.Count <= 0))
			hasNewUploadFile = false;
            else
				IskeepLastFile = false;


            if (hasNewUploadFile)
			{
              var  MarketCGIFile = _oAPIIPD_Form.Files[0];
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
				if(lastApiIpd != null) 
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
			var _oAPIIPD = await _pidf_API_IPD_repository.GetAsync(x=>x.Pidfid == pidfId);
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

            dynamic _CharterObjects = new ExpandoObject();
            if (dbresult != null)
            {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                {
                    _CharterObjects = dbresult.Tables[0].DataTableToList<TimelineInMonths>();
                }
            }
            _oCharterEntity.timelineInMonths = _CharterObjects;
            return _oCharterEntity;
        }
        public async Task<DBOperation> AddUpdateAPICharter(PIDFAPICharterFormEntity _oAPIRnD)
        {
            //PIDFAPIIPDFormEntity _exsistingAPIRnD = new PIDFAPIIPDFormEntity();
            if (_oAPIRnD.PIDFAPICharterFormID > 0)
            {
                var lastApiCharter = _pidf_API_Charter_repository.GetAll().First(x => x.PidfApiCharterId == _oAPIRnD.PIDFAPICharterFormID);
                if (lastApiCharter != null)
                {
                    _oAPIRnD.ManHourRates = (Convert.ToString(_oAPIRnD.ManHourRates)=="")? "0":_oAPIRnD.ManHourRates;
                    lastApiCharter.ManHourRates = int.Parse(_oAPIRnD.ManHourRates);
                    lastApiCharter.ApigroupLeader = _oAPIRnD.APIGroupLeader;
                    lastApiCharter.ProjectComplexityId = _oAPIRnD.ProjectComplexityId;
                    
                    lastApiCharter.Pidfid = long.Parse(_oAPIRnD.Pidfid);
                    lastApiCharter.ModifyBy = _oAPIRnD.LoggedInUserId;
                    lastApiCharter.ModifyDate = DateTime.Now;
                    _pidf_API_Charter_repository.UpdateAsync(lastApiCharter);
                }
                else
                {
                    return DBOperation.NotFound;
                }
                //Implement AuditLog
                //Implement PIDF staurs change
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
            var _StatusID = (_oAPIRnD.SaveType == "Save") ? Master_PIDFStatus.APISubmitted : Master_PIDFStatus.APISubmitted;
            await _auditLogService.UpdatePIDFStatusCommon(long.Parse(_oAPIRnD.Pidfid), (int)_StatusID, _oAPIRnD.LoggedInUserId);
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
                var lastApiIpd = _pidf_API_RnD_repository.GetAll().First(x => x.PidfApiRnDId == _oAPIRnD.PIDFAPIRnDFormID);
                if (lastApiIpd != null)
                {                  
                    lastApiIpd.PlantQc = _oAPIRnD.PlantQC;
                    lastApiIpd.Development = _oAPIRnD.Development;
                    lastApiIpd.Total = _oAPIRnD.Total;
                    lastApiIpd.Exhibit = _oAPIRnD.Exhibit;
                    lastApiIpd.ScaleUp = _oAPIRnD.ScaleUp;
                    lastApiIpd.MarketExtenstionId= _oAPIRnD.MarketID;
                    lastApiIpd.SponsorBusinessPartner= _oAPIRnD.SponsorBusinessPartner;
                    lastApiIpd.ApimarketPrice= _oAPIRnD.APIMarketPrice;
                    lastApiIpd.ApitargetRmcCcpc= _oAPIRnD.APITargetRMC_CCPC;
                    lastApiIpd.Pidfid = long.Parse(_oAPIRnD.Pidfid);
                    lastApiIpd.ModifyBy = _oAPIRnD.LoggedInUserId;
                    lastApiIpd.ModifyDate = DateTime.Now;
                    _pidf_API_RnD_repository.UpdateAsync(lastApiIpd);
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
			return DropdownObjects;
		}
		public async Task<DBOperation> AddUpdatePBF(PidfPbfEntity pbfEntity)
		{		
				return DBOperation.Success;			
		}
		// t		

		public async Task<PidfPbfEntity> GetPbfFormData(long pidfId, int buid, int? strengthid)
		{
			PidfPbfRnDEntity pidfPbfRnDEntity = new PidfPbfRnDEntity() ;
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
    }
}
