using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Core.ServiceImplementations;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

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
			IMasterAnalyticalGLService masterAnalyticalGLService, IPidfProductStrengthService productStrengthService,

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
		public async Task<DBOperation> AddUpdatePBF(PidfPbfRnDEntity entityPIDF)
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
				expr = u => u.BusinessUnitId == buid && u.Pidfid == pidfId && u.PidfproductStrengthId == strengthid;
			}
			else
			{
				expr = u => u.BusinessUnitId == buid && u.Pidfid == pidfId;
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
	}
}
