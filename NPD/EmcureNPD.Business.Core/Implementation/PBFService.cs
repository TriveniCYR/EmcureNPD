using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class PBFService : IPBFService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;

        private readonly INotificationService _notificationService;
        private readonly IMasterAuditLogService _auditLogService;
        private readonly IHelper _helper;
        private readonly IExceptionService _ExceptionService;

        private IRepository<PidfPbf> _pbfRepository { get; set; }
        private IRepository<Pidf> _repository { get; set; }
        private IRepository<PidfPbfAnalytical> _pidfPbfAnalyticalRepository { get; set; }
        private IRepository<PidfPbfAnalyticalAmvcost> _PidfPbfAnalyticalAmvcostRepository { get; set; }
        private IRepository<PidfPbfClinical> _pidfPbfClinicalRepository { get; set; }
        private IRepository<PidfPbfGeneral> _pidfPbfGeneralRepository { get; set; }
        private IRepository<PidfPbfMarketMapping> _pidfPbfMarketMappingRepository { get; set; }
        private IRepository<PidfPbfGeneralStrength> _pidfPbfGeneralStrengthRepository { get; set; }
        private IRepository<PidfPbfRnDExicipientRequirement> _pidfPbfRnDExicipientRequirementRepository { get; set; }
        private IRepository<PidfPbfRnDPackagingMaterial> _pidfPbfRnDPackagingMaterialRepository { get; set; }
        private IRepository<PidfPbfAnalyticalAmvcostStrengthMapping> _pidfPbfAnalyticalAmvcostStrengthMappingRepository { get; set; }
        private IRepository<PidfPbfRnDMaster> _pidfPbfRnDMasterRepository { get; set; }
        private IRepository<PidfPbfRndBatchSize> _pidfPbfRndBatchSizeRepository { get; set; }
        private IRepository<PidfPbfRnDApirequirement> _pidfPbfRndApirequirementRepository { get; set; }
        private IRepository<PidfPbfRnDToolingChangepart> _pidfPbfRndToolingChangePartCostRepository { get; set; }
        private IRepository<PidfPbfRnDCapexMiscellaneousExpense> _pidfPbfRndCapexMiscellaneousExpenseRepository { get; set; }
        private IRepository<PidfPbfRnDPlantSupportCost> _pidfPbfRndPlantSupportCostRepository { get; set; }
        private IRepository<PidfPbfRnDReferenceProductDetail> _pidfPbfRndReferenceProductDetailRepository { get; set; }
        private IRepository<PidfPbfRnDFillingExpense> _pidfPbfRndFillingExpenseRepository { get; set; }
        private IRepository<PidfPbfRnDManPowerCost> _pidfPbfRndPidfPbfRnDManPowerCostRepository { get; set; }

        public PBFService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, INotificationService notificationService, IMasterAuditLogService auditLogService, IHelper helper, IExceptionService exceptionService)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _auditLogService = auditLogService;
            _helper = helper;
            _notificationService = notificationService;
            _ExceptionService = exceptionService;

            _repository = _unitOfWork.GetRepository<Pidf>();
            _pbfRepository = _unitOfWork.GetRepository<PidfPbf>();
            _pidfPbfAnalyticalRepository = _unitOfWork.GetRepository<PidfPbfAnalytical>();
            _PidfPbfAnalyticalAmvcostRepository = _unitOfWork.GetRepository<PidfPbfAnalyticalAmvcost>();
            _pidfPbfClinicalRepository = _unitOfWork.GetRepository<PidfPbfClinical>();
            _pidfPbfGeneralRepository = _unitOfWork.GetRepository<PidfPbfGeneral>();
            _pidfPbfMarketMappingRepository = _unitOfWork.GetRepository<PidfPbfMarketMapping>();
            _pidfPbfGeneralStrengthRepository = _unitOfWork.GetRepository<PidfPbfGeneralStrength>();
            _pidfPbfAnalyticalAmvcostStrengthMappingRepository = _unitOfWork.GetRepository<PidfPbfAnalyticalAmvcostStrengthMapping>();
            _pidfPbfRnDExicipientRequirementRepository = _unitOfWork.GetRepository<PidfPbfRnDExicipientRequirement>();
            _pidfPbfRnDPackagingMaterialRepository = _unitOfWork.GetRepository<PidfPbfRnDPackagingMaterial>();
            _pidfPbfRnDMasterRepository = _unitOfWork.GetRepository<PidfPbfRnDMaster>();
            _pidfPbfRndBatchSizeRepository = _unitOfWork.GetRepository<PidfPbfRndBatchSize>();
            _pidfPbfRndApirequirementRepository = _unitOfWork.GetRepository<PidfPbfRnDApirequirement>();
            _pidfPbfRndToolingChangePartCostRepository = _unitOfWork.GetRepository<PidfPbfRnDToolingChangepart>();
            _pidfPbfRndCapexMiscellaneousExpenseRepository = _unitOfWork.GetRepository<PidfPbfRnDCapexMiscellaneousExpense>();
            _pidfPbfRndPlantSupportCostRepository = _unitOfWork.GetRepository<PidfPbfRnDPlantSupportCost>();
            _pidfPbfRndReferenceProductDetailRepository = _unitOfWork.GetRepository<PidfPbfRnDReferenceProductDetail>();
            _pidfPbfRndPidfPbfRnDManPowerCostRepository = _unitOfWork.GetRepository<PidfPbfRnDManPowerCost>();
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
                pbfgeneralid = await SavePidfAndPBFCommanDetailsnew1(pbfEntity.Pidfid, pbfEntity);

                if (pbfgeneralid > 0)
                {
                    // var isSuccess = await _auditLogService.CreateAuditLog<PBFFormEntity>(pbfEntity.Pidfpbfid > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
                    //Utility.Enums.ModuleEnum.PBF, pbfEntity, pbfEntity, Convert.ToInt32(pbfEntity.Pidfid));
                    await _unitOfWork.SaveChangesAsync();
                    var _StatusID = (pbfEntity.SaveType == "Save") ? Master_PIDFStatus.PBFSubmitted : Master_PIDFStatus.PBFInProgress;
                    await _auditLogService.UpdatePIDFStatusCommon(pbfEntity.Pidfid, (int)_StatusID, loggedInUserId);
                    await _notificationService.CreateNotification(pbfEntity.Pidfid, (int)_StatusID, string.Empty, string.Empty, loggedInUserId);
                    return DBOperation.Success;

                }
                else
                {
                    return DBOperation.Error;
                }
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
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

            //var isSuccess = await _auditLogService.CreateAuditLog<PidfPbfGeneralEntity>(pbfgeneralid > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
            //              Utility.Enums.ModuleEnum.PBF, PidfPbfGeneralEntity, PidfPbfGeneralEntity, Convert.ToInt32(pbfgeneralid));
            await _unitOfWork.SaveChangesAsync();
            var _StatusID = (PidfPbfGeneralEntity.SaveType == "Save") ? Master_PIDFStatus.PBFSubmitted : Master_PIDFStatus.PBFInProgress;
            await _auditLogService.UpdatePIDFStatusCommon(pbfgeneralid, (int)_StatusID, loggedInUserId);

            return DBOperation.Success;
        }

        #endregion saving RnD Details

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
            DropdownObjects.MasterPackingType = dsDropdownOptions.Tables[1];
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
            DropdownObjects.PBFRNDManPowerCost = dsDropdownOptions.Tables[18];
            DropdownObjects.IPDCostOfLitigation = dsDropdownOptions.Tables[19];

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
                var OldPBFEntity = _mapperFactory.Get<PidfPbf, PBFFormEntity>(objPIDFPbf);
                if (objPIDFPbf != null)
                {
                    objPIDFPbf = _mapperFactory.Get<PBFFormEntity, PidfPbf>(pbfentity);
                    objPIDFPbf.ModifyBy = loggedInUserId;
                    objPIDFPbf.ModifyDate = DateTime.Now;
                    _pbfRepository.UpdateAsync(objPIDFPbf);
                    await _unitOfWork.SaveChangesAsync();
                    var isSuccess = await _auditLogService.CreateAuditLog<PBFFormEntity>(Utility.Audit.AuditActionType.Update,
                  ModuleEnum.PBF, OldPBFEntity, pbfentity, (int)pbfentity.Pidfid);
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

                #endregion Section PBF Add Update

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

                #endregion Marketting Mapping Add Update

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

                #endregion Update PIDF

                #region Section PBF General Add Update

                //PidfPbfGeneral objPIDFGeneralupdate;
                var objPIDFGeneralupdate = _pidfPbfGeneralRepository.GetAllQuery().Where(x => x.Pidfpbfid == pbfentity.Pidfpbfid && x.BusinessUnitId == pbfentity.BusinessUnitId).FirstOrDefault();
                if (objPIDFGeneralupdate != null)
                {
                    //objPIDFGeneralupdate = _mapperFactory.Get<PBFFormEntity, PidfPbfGeneral>(pbfentity);

                    //objPIDFGeneralupdate.BusinessUnitId = pbfentity.BusinessUnitId;
                    objPIDFGeneralupdate.Capex = pbfentity.Capex;
                    objPIDFGeneralupdate.TotalExpense = pbfentity.TotalExpense;
                    objPIDFGeneralupdate.ProjectComplexity = pbfentity.ProjectComplexity;
                    objPIDFGeneralupdate.ProductTypeId = pbfentity.GeneralProductTypeId;
                    objPIDFGeneralupdate.TestLicenseAvailability = pbfentity.TestLicenseAvailability;
                    objPIDFGeneralupdate.BudgetTimelineSubmissionDate = pbfentity.BudgetTimelineSubmissionDate;
                    objPIDFGeneralupdate.ProjectDevelopmentInitialDate = pbfentity.ProjectDevelopmentInitialDate;
                    objPIDFGeneralupdate.FormulationGlid = pbfentity.FormulationGLId;
                    objPIDFGeneralupdate.AnalyticalGlid = pbfentity.AnalyticalGLId;
                    //objPIDFGeneralupdate = _mapperFactory.Get<PBFFormEntity, PidfPbfGeneral>(pbfentity);
                    _pidfPbfGeneralRepository.UpdateAsync(objPIDFGeneralupdate);
                    await _unitOfWork.SaveChangesAsync();

                    pbfgeneralid = objPIDFGeneralupdate.PbfgeneralId;
                }
                else
                {
                    PidfPbfGeneral objPIDFGeneraladd = new PidfPbfGeneral();
                    objPIDFGeneraladd.Pidfpbfid = pidfpbfid;
                    objPIDFGeneraladd.BusinessUnitId = pbfentity.BusinessUnitId;
                    objPIDFGeneraladd.Capex = pbfentity.Capex;
                    objPIDFGeneraladd.TotalExpense = pbfentity.TotalExpense;
                    objPIDFGeneraladd.ProjectComplexity = pbfentity.ProjectComplexity;
                    objPIDFGeneraladd.ProductTypeId = pbfentity.GeneralProductTypeId;
                    objPIDFGeneraladd.TestLicenseAvailability = pbfentity.TestLicenseAvailability;
                    objPIDFGeneraladd.BudgetTimelineSubmissionDate = pbfentity.BudgetTimelineSubmissionDate;
                    objPIDFGeneraladd.ProjectDevelopmentInitialDate = pbfentity.ProjectDevelopmentInitialDate;
                    objPIDFGeneraladd.FormulationGlid = pbfentity.FormulationGLId;
                    objPIDFGeneraladd.AnalyticalGlid = pbfentity.AnalyticalGLId;
                    //objPIDFGeneraladd = _mapperFactory.Get<PBFFormEntity, PidfPbfGeneral>(pbfentity);
                    objPIDFGeneraladd.CreatedBy = loggedInUserId;
                    objPIDFGeneraladd.CreatedDate = DateTime.Now;
                    _pidfPbfGeneralRepository.AddAsync(objPIDFGeneraladd);
                    await _unitOfWork.SaveChangesAsync();
                    pbfgeneralid = objPIDFGeneraladd.PbfgeneralId;
                }

                #endregion Section PBF General Add Update

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

                #endregion GeneralProductStrength Add Update

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

                #endregion Section Clinical Add Update

                #region Section Analytical Add Update

                List<PidfPbfAnalytical> objAnalyticallist = new();
                List<PidfPbfAnalyticalAmvcostStrengthMapping> objAnalyticalAmvcostStrengthMappinglist = new();
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
                long TotalAMVCostId = 0;
                if (pbfentity.AnalyticalAMVCosts != null)
                {
                    var analyticalamvcost = _PidfPbfAnalyticalAmvcostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).FirstOrDefault();
                    var OldPBFAnalyticalAmvcost = _mapperFactory.Get<PidfPbfAnalyticalAmvcost, AnalyticalAmvcost>(analyticalamvcost);
                    if (analyticalamvcost != null)
                    {
                        analyticalamvcost.TotalAmvtitle = pbfentity.AnalyticalAMVCosts.TotalAmvtitle;
                        analyticalamvcost.TotalAmvcost = pbfentity.AnalyticalAMVCosts.TotalAmvcost;
                        analyticalamvcost.Remark = pbfentity.AnalyticalAMVCosts.Remark;
                        analyticalamvcost.CreatedDate = DateTime.Now;
                        analyticalamvcost.CreatedBy = loggedInUserId;
                        _PidfPbfAnalyticalAmvcostRepository.UpdateAsync(analyticalamvcost);
                        TotalAMVCostId = analyticalamvcost.TotalAmvcostId;

                        //await _auditLogService.CreateAuditLog(Utility.Audit.AuditActionType.Update,
                        //                ModuleEnum.PBF, OldPBFAnalyticalAmvcost, pbfentity.AnalyticalAMVCosts,(int)pbfgeneralid);
                    }
                    else
                    {
                        PidfPbfAnalyticalAmvcost obganalyticalamvcost = new PidfPbfAnalyticalAmvcost();
                        obganalyticalamvcost.TotalAmvtitle = pbfentity.AnalyticalAMVCosts.TotalAmvtitle;
                        obganalyticalamvcost.TotalAmvcost = pbfentity.AnalyticalAMVCosts.TotalAmvcost;
                        obganalyticalamvcost.Remark = pbfentity.AnalyticalAMVCosts.Remark;
                        obganalyticalamvcost.PbfgeneralId = pbfgeneralid;
                        obganalyticalamvcost.CreatedDate = DateTime.Now;
                        obganalyticalamvcost.CreatedBy = loggedInUserId;
                        _PidfPbfAnalyticalAmvcostRepository.AddAsync(obganalyticalamvcost);
                        TotalAMVCostId = obganalyticalamvcost.TotalAmvcostId;
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                //Save analytical cost end

                //Save analytical Total strength mapping start
                if (pidfpbfid > 0 && pbfentity.AnalyticalStrengthMappingEntities.Count > 0)
                {
                    var analyticalstrengthmapping = _pidfPbfAnalyticalAmvcostStrengthMappingRepository.GetAllQuery().Where(x => x.TotalAmvcostId == TotalAMVCostId).ToList();
                    if (analyticalstrengthmapping.Count > 0)
                    {
                        foreach (var item in analyticalstrengthmapping)
                        {
                            _pidfPbfAnalyticalAmvcostStrengthMappingRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                    if (pbfentity.AnalyticalStrengthMappingEntities != null && pbfentity.AnalyticalStrengthMappingEntities.Count() > 0)
                    {
                        foreach (var item in pbfentity.AnalyticalStrengthMappingEntities)
                        {
                            PidfPbfAnalyticalAmvcostStrengthMapping objstrengthmapping = new PidfPbfAnalyticalAmvcostStrengthMapping();
                            objstrengthmapping = _mapperFactory.Get<AnalyticalAmvcostStrengthMappingEntity, PidfPbfAnalyticalAmvcostStrengthMapping>(item);
                            objstrengthmapping.TotalAmvcostId = TotalAMVCostId;
                            objstrengthmapping.CreatedDate = DateTime.Now;
                            objstrengthmapping.CreatedBy = loggedInUserId;
                            objAnalyticalAmvcostStrengthMappinglist.Add(objstrengthmapping);
                        }
                        _pidfPbfAnalyticalAmvcostStrengthMappingRepository.AddRangeAsync(objAnalyticalAmvcostStrengthMappinglist);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }

                //Save analytical cost end

                #endregion Section Analytical Add Update

                #region RND Add Update

                #region RND Master Add Update

                PidfPbfRnDMaster objrndMaster;
                if (pbfentity.RNDMasterEntities != null)
                {
                    objrndMaster = _pidfPbfRnDMasterRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).FirstOrDefault();
                    var OldPBFRndEntity = _mapperFactory.Get<PidfPbfRnDMaster, RNDMasterEntity>(objrndMaster);
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

                        //await _auditLogService.CreateAuditLog<RNDMasterEntity>(Utility.Audit.AuditActionType.Update,
                        //                ModuleEnum.PBF, OldPBFRndEntity, pbfentity.RNDMasterEntities, (int)pbfgeneralid);
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
                }


                #endregion RND Master Add Update

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

                #endregion Batch Size Add Update

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

                #endregion API Requirement Add Update

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

                #endregion RND Excipient Add Update

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

                #endregion RND Packaging Add Update

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

                #endregion Tooling Change Part Add Update

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

                #endregion Capex and Miscellaneous Expenses Add Update

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

                #endregion Plant Support Cost Add Update

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

                #endregion Reference Product Detail Add Update

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

                #endregion Filling Expenses Add Update

                #region Man Power Cost Add Update

                List<PidfPbfRnDManPowerCost> objManPowerCostlist = new();

                var manpowercost = _pidfPbfRndPidfPbfRnDManPowerCostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (manpowercost.Count > 0)
                {
                    foreach (var item in manpowercost)
                    {
                        _pidfPbfRndPidfPbfRnDManPowerCostRepository.Remove(item);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                //Save manpowercost Entities
                if (pbfentity.RNDManPowerCosts != null && pbfentity.RNDManPowerCosts.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDManPowerCosts)
                    {
                        PidfPbfRnDManPowerCost objmanpowercost = new PidfPbfRnDManPowerCost();
                        objmanpowercost = _mapperFactory.Get<RNDManPowerCost, PidfPbfRnDManPowerCost>(item);
                        objmanpowercost.PbfgeneralId = pbfgeneralid;
                        objmanpowercost.CreatedDate = DateTime.Now;
                        objmanpowercost.CreatedBy = loggedInUserId;
                        objManPowerCostlist.Add(objmanpowercost);
                    }
                    _pidfPbfRndPidfPbfRnDManPowerCostRepository.AddRangeAsync(objManPowerCostlist);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion Man Power Cost Add Update

                #endregion RND Add Update

                return pbfgeneralid;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return pbfgeneralid;
            }
        }

        public async Task<long> SavePidfAndPBFCommanDetailsnew(long pidfid, PBFFormEntity pbfentity)
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
                var OldPBFEntity = _mapperFactory.Get<PidfPbf, PBFFormEntity>(objPIDFPbf);
                if (objPIDFPbf != null)
                {
                    objPIDFPbf = _mapperFactory.Get<PBFFormEntity, PidfPbf>(pbfentity);
                    objPIDFPbf.ModifyBy = loggedInUserId;
                    objPIDFPbf.ModifyDate = DateTime.Now;
                    _pbfRepository.UpdateAsync(objPIDFPbf);
                    await _unitOfWork.SaveChangesAsync();
                    var isSuccess = await _auditLogService.CreateAuditLog<PBFFormEntity>(Utility.Audit.AuditActionType.Update,
                  ModuleEnum.PBF, OldPBFEntity, pbfentity, (int)pbfentity.Pidfid);
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

                #endregion Section PBF Add Update

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

                #endregion Marketting Mapping Add Update

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

                #endregion Update PIDF

                #region Section PBF General Add Update

                //PidfPbfGeneral objPIDFGeneralupdate;
                var objPIDFGeneralupdate = _pidfPbfGeneralRepository.GetAllQuery().Where(x => x.Pidfpbfid == pbfentity.Pidfpbfid && x.BusinessUnitId == pbfentity.BusinessUnitId).FirstOrDefault();
                if (objPIDFGeneralupdate != null)
                {
                    //objPIDFGeneralupdate = _mapperFactory.Get<PBFFormEntity, PidfPbfGeneral>(pbfentity);

                    //objPIDFGeneralupdate.BusinessUnitId = pbfentity.BusinessUnitId;
                    objPIDFGeneralupdate.Capex = pbfentity.Capex;
                    objPIDFGeneralupdate.TotalExpense = pbfentity.TotalExpense;
                    objPIDFGeneralupdate.ProjectComplexity = pbfentity.ProjectComplexity;
                    objPIDFGeneralupdate.ProductTypeId = pbfentity.GeneralProductTypeId;
                    objPIDFGeneralupdate.TestLicenseAvailability = pbfentity.TestLicenseAvailability;
                    objPIDFGeneralupdate.BudgetTimelineSubmissionDate = pbfentity.BudgetTimelineSubmissionDate;
                    objPIDFGeneralupdate.ProjectDevelopmentInitialDate = pbfentity.ProjectDevelopmentInitialDate;
                    objPIDFGeneralupdate.FormulationGlid = pbfentity.FormulationGLId;
                    objPIDFGeneralupdate.AnalyticalGlid = pbfentity.AnalyticalGLId;
                    //objPIDFGeneralupdate = _mapperFactory.Get<PBFFormEntity, PidfPbfGeneral>(pbfentity);
                    _pidfPbfGeneralRepository.UpdateAsync(objPIDFGeneralupdate);
                    await _unitOfWork.SaveChangesAsync();

                    pbfgeneralid = objPIDFGeneralupdate.PbfgeneralId;
                }
                else
                {
                    PidfPbfGeneral objPIDFGeneraladd = new PidfPbfGeneral();
                    objPIDFGeneraladd.Pidfpbfid = pidfpbfid;
                    objPIDFGeneraladd.BusinessUnitId = pbfentity.BusinessUnitId;
                    objPIDFGeneraladd.Capex = pbfentity.Capex;
                    objPIDFGeneraladd.TotalExpense = pbfentity.TotalExpense;
                    objPIDFGeneraladd.ProjectComplexity = pbfentity.ProjectComplexity;
                    objPIDFGeneraladd.ProductTypeId = pbfentity.GeneralProductTypeId;
                    objPIDFGeneraladd.TestLicenseAvailability = pbfentity.TestLicenseAvailability;
                    objPIDFGeneraladd.BudgetTimelineSubmissionDate = pbfentity.BudgetTimelineSubmissionDate;
                    objPIDFGeneraladd.ProjectDevelopmentInitialDate = pbfentity.ProjectDevelopmentInitialDate;
                    objPIDFGeneraladd.FormulationGlid = pbfentity.FormulationGLId;
                    objPIDFGeneraladd.AnalyticalGlid = pbfentity.AnalyticalGLId;
                    //objPIDFGeneraladd = _mapperFactory.Get<PBFFormEntity, PidfPbfGeneral>(pbfentity);
                    objPIDFGeneraladd.CreatedBy = loggedInUserId;
                    objPIDFGeneraladd.CreatedDate = DateTime.Now;
                    _pidfPbfGeneralRepository.AddAsync(objPIDFGeneraladd);
                    await _unitOfWork.SaveChangesAsync();
                    pbfgeneralid = objPIDFGeneraladd.PbfgeneralId;
                }

                #endregion Section PBF General Add Update

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

                #endregion GeneralProductStrength Add Update

                #region Section Clinical Add Update

                //Save clinical Entities
                if (pbfentity.ClinicalEntities != null && pbfentity.ClinicalEntities.Count() > 0)
                {
                    foreach (var item in pbfentity.ClinicalEntities)
                    {

                        var clinical = _pidfPbfClinicalRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid && x.StrengthId == item.StrengthId && x.BioStudyTypeId == item.BioStudyTypeId).FirstOrDefault();

                        if (clinical != null)
                        {
                            clinical.FastingOrFed = item.FastingOrFed;
                            clinical.NumberofVolunteers = item.NumberofVolunteers;
                            clinical.ClinicalCostAndVolume = item.ClinicalCostAndVolume;
                            clinical.BioAnalyticalCostAndVolume = item.BioAnalyticalCostAndVolume;
                            clinical.DocCostandStudy = item.DocCostandStudy;
                            clinical.CreatedDate = DateTime.Now;
                            clinical.CreatedBy = loggedInUserId;
                            _pidfPbfClinicalRepository.UpdateAsync(clinical);
                        }
                        else
                        {
                            PidfPbfClinical obgclinical = new PidfPbfClinical();
                            obgclinical = _mapperFactory.Get<ClinicalEntity, PidfPbfClinical>(item);
                            obgclinical.PbfgeneralId = pbfgeneralid;
                            obgclinical.CreatedDate = DateTime.Now;
                            obgclinical.CreatedBy = loggedInUserId;
                            _pidfPbfClinicalRepository.AddAsync(obgclinical);
                        }

                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion Section Clinical Add Update

                #region Section Analytical Add Update

                List<PidfPbfAnalyticalAmvcostStrengthMapping> objAnalyticalAmvcostStrengthMappinglist = new();

                //Save analytical Entities
                if (pbfentity.AnalyticalEntities != null && pbfentity.AnalyticalEntities.Count() > 0)
                {
                    var filtertesttype = pbfentity.AnalyticalEntities.ToList().Select(x => x.TestTypeId).ToList();

                    var itemlisttodelete = _pidfPbfAnalyticalRepository.GetAllQuery().Where(m => m.PbfgeneralId == pbfgeneralid && !filtertesttype.Contains(m.TestTypeId)).ToList();
                    if (itemlisttodelete.Count > 0)
                    {
                        //_pidfPbfAnalyticalRepository.RemoveRange(itemlisttodelete);
                        //await _unitOfWork.SaveChangesAsync();
                        foreach (var item in itemlisttodelete)
                        {
                            _pidfPbfAnalyticalRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }

                    foreach (var item in pbfentity.AnalyticalEntities)
                    {
                        var analytical = _pidfPbfAnalyticalRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid && x.StrengthId == item.StrengthId && x.ActivityTypeId == item.ActivityTypeId && x.TestTypeId == item.TestTypeId).FirstOrDefault();
                        if (analytical != null)
                        {
                            analytical.Numberoftests = item.Numberoftests;
                            analytical.PrototypeDevelopment = item.PrototypeDevelopment;
                            analytical.CostPerTest = item.CostPerTest;
                            analytical.PrototypeCost = item.PrototypeCost;
                            analytical.CreatedDate = DateTime.Now;
                            analytical.CreatedBy = loggedInUserId;
                            _pidfPbfAnalyticalRepository.UpdateAsync(analytical);
                        }
                        else
                        {
                            PidfPbfAnalytical obganalytical = new PidfPbfAnalytical();
                            obganalytical = _mapperFactory.Get<AnalyticalEntity, PidfPbfAnalytical>(item);
                            obganalytical.PbfgeneralId = pbfgeneralid;
                            obganalytical.CreatedDate = DateTime.Now;
                            obganalytical.CreatedBy = loggedInUserId;
                            _pidfPbfAnalyticalRepository.AddAsync(obganalytical);
                        }
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
                //Save analytical cost start
                long TotalAMVCostId = 0;
                if (pbfentity.AnalyticalAMVCosts != null)
                {
                    var analyticalamvcost = _PidfPbfAnalyticalAmvcostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).FirstOrDefault();
                    var OldPBFAnalyticalAmvcost = _mapperFactory.Get<PidfPbfAnalyticalAmvcost, AnalyticalAmvcost>(analyticalamvcost);
                    if (analyticalamvcost != null)
                    {
                        analyticalamvcost.TotalAmvtitle = pbfentity.AnalyticalAMVCosts.TotalAmvtitle;
                        analyticalamvcost.TotalAmvcost = pbfentity.AnalyticalAMVCosts.TotalAmvcost;
                        analyticalamvcost.Remark = pbfentity.AnalyticalAMVCosts.Remark;
                        analyticalamvcost.CreatedDate = DateTime.Now;
                        analyticalamvcost.CreatedBy = loggedInUserId;
                        _PidfPbfAnalyticalAmvcostRepository.UpdateAsync(analyticalamvcost);
                        TotalAMVCostId = analyticalamvcost.TotalAmvcostId;

                        //await _auditLogService.CreateAuditLog(Utility.Audit.AuditActionType.Update,
                        //                ModuleEnum.PBF, OldPBFAnalyticalAmvcost, pbfentity.AnalyticalAMVCosts,(int)pbfgeneralid);
                    }
                    else
                    {
                        PidfPbfAnalyticalAmvcost obganalyticalamvcost = new PidfPbfAnalyticalAmvcost();
                        obganalyticalamvcost.TotalAmvtitle = pbfentity.AnalyticalAMVCosts.TotalAmvtitle;
                        obganalyticalamvcost.TotalAmvcost = pbfentity.AnalyticalAMVCosts.TotalAmvcost;
                        obganalyticalamvcost.Remark = pbfentity.AnalyticalAMVCosts.Remark;
                        obganalyticalamvcost.PbfgeneralId = pbfgeneralid;
                        obganalyticalamvcost.CreatedDate = DateTime.Now;
                        obganalyticalamvcost.CreatedBy = loggedInUserId;
                        _PidfPbfAnalyticalAmvcostRepository.AddAsync(obganalyticalamvcost);
                        TotalAMVCostId = obganalyticalamvcost.TotalAmvcostId;
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                //Save analytical cost end

                //Save analytical Total strength mapping start
                if (pidfpbfid > 0 && pbfentity.AnalyticalStrengthMappingEntities.Count > 0)
                {
                    var filterstrength = pbfentity.AnalyticalStrengthMappingEntities.ToList().Select(x => x.StrengthId).ToList();

                    var itemlisttodelete = _pidfPbfAnalyticalAmvcostStrengthMappingRepository.GetAllQuery().Where(m => m.TotalAmvcostId == TotalAMVCostId && !filterstrength.Contains(m.StrengthId)).ToList();
                    if (itemlisttodelete.Count > 0)
                    {
                        //_pidfPbfAnalyticalRepository.RemoveRange(itemlisttodelete);
                        //await _unitOfWork.SaveChangesAsync();
                        foreach (var item in itemlisttodelete)
                        {
                            _pidfPbfAnalyticalAmvcostStrengthMappingRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }

                    if (pbfentity.AnalyticalStrengthMappingEntities != null && pbfentity.AnalyticalStrengthMappingEntities.Count() > 0)
                    {
                        foreach (var item in pbfentity.AnalyticalStrengthMappingEntities)
                        {
                            var analyticalstrengthmapping = _pidfPbfAnalyticalAmvcostStrengthMappingRepository.GetAllQuery().Where(x => x.TotalAmvcostId == TotalAMVCostId && x.StrengthId == item.StrengthId).FirstOrDefault();
                            if (analyticalstrengthmapping != null)
                            {
                                analyticalstrengthmapping.IsChecked = item.IsChecked;
                                analyticalstrengthmapping.CreatedDate = DateTime.Now;
                                analyticalstrengthmapping.CreatedBy = loggedInUserId;
                                _pidfPbfAnalyticalAmvcostStrengthMappingRepository.UpdateAsync(analyticalstrengthmapping);

                            }
                            else
                            {
                                PidfPbfAnalyticalAmvcostStrengthMapping objstrengthmapping = new PidfPbfAnalyticalAmvcostStrengthMapping();
                                objstrengthmapping = _mapperFactory.Get<AnalyticalAmvcostStrengthMappingEntity, PidfPbfAnalyticalAmvcostStrengthMapping>(item);
                                objstrengthmapping.TotalAmvcostId = TotalAMVCostId;
                                objstrengthmapping.CreatedDate = DateTime.Now;
                                objstrengthmapping.CreatedBy = loggedInUserId;
                                _pidfPbfAnalyticalAmvcostStrengthMappingRepository.AddAsync(objstrengthmapping);
                            }

                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                }

                //Save analytical cost end

                #endregion Section Analytical Add Update

                #region RND Add Update

                #region RND Master Add Update

                PidfPbfRnDMaster objrndMaster;
                if (pbfentity.RNDMasterEntities != null)
                {
                    objrndMaster = _pidfPbfRnDMasterRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).FirstOrDefault();
                    var OldPBFRndEntity = _mapperFactory.Get<PidfPbfRnDMaster, RNDMasterEntity>(objrndMaster);
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

                        //await _auditLogService.CreateAuditLog<RNDMasterEntity>(Utility.Audit.AuditActionType.Update,
                        //                ModuleEnum.PBF, OldPBFRndEntity, pbfentity.RNDMasterEntities, (int)pbfgeneralid);
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
                }


                #endregion RND Master Add Update

                #region Batch Size Add Update

                //Save batch size Entities
                if (pbfentity.RNDBatchSizes != null && pbfentity.RNDBatchSizes.Count() > 0)
                {
                    //var filterstrength = pbfentity.RNDBatchSizes.ToList().Select(x => x.StrengthId).ToList();

                    //var itemlisttodelete = _pidfPbfRndBatchSizeRepository.GetAllQuery().Where(m => m.PbfgeneralId == pbfgeneralid && !filterstrength.Contains(m.StrengthId)).ToList();
                    //if (itemlisttodelete.Count > 0)
                    //{
                    //    //_pidfPbfAnalyticalRepository.RemoveRange(itemlisttodelete);
                    //    //await _unitOfWork.SaveChangesAsync();
                    //    foreach (var item in itemlisttodelete)
                    //    {
                    //        _pidfPbfRndBatchSizeRepository.Remove(item);
                    //    }
                    //    await _unitOfWork.SaveChangesAsync();
                    //}

                    foreach (var item in pbfentity.RNDBatchSizes)
                    {
                        var batchsize = _pidfPbfRndBatchSizeRepository.GetAllQuery().Where(m => m.PbfgeneralId == pbfgeneralid && m.StrengthId == item.StrengthId).FirstOrDefault();
                        if (batchsize != null)
                        {
                            batchsize.PrototypeFormulation = item.PrototypeFormulation;
                            batchsize.ScaleUpbatch = item.ScaleUpbatch;
                            batchsize.ExhibitBatch1 = item.ExhibitBatch1;
                            batchsize.ExhibitBatch2 = item.ExhibitBatch2;
                            batchsize.ExhibitBatch3 = item.ExhibitBatch3;
                            batchsize.CreatedDate = DateTime.Now;
                            batchsize.CreatedBy = loggedInUserId;
                            _pidfPbfRndBatchSizeRepository.UpdateAsync(batchsize);
                        }
                        else
                        {
                            PidfPbfRndBatchSize objbatchsize = new PidfPbfRndBatchSize();
                            objbatchsize = _mapperFactory.Get<RNDBatchSize, PidfPbfRndBatchSize>(item);
                            objbatchsize.PbfgeneralId = pbfgeneralid;
                            objbatchsize.CreatedDate = DateTime.Now;
                            objbatchsize.CreatedBy = loggedInUserId;
                            _pidfPbfRndBatchSizeRepository.AddAsync(batchsize);
                        }

                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion Batch Size Add Update

                #region API Requirement Add Update



                //Save Api requirement Entities
                if (pbfentity.RNDApirequirements != null && pbfentity.RNDApirequirements.Count() > 0)
                {
                    //var filterstrength = pbfentity.RNDBatchSizes.ToList().Select(x => x.StrengthId).ToList();

                    //var itemlisttodelete = _pidfPbfRndApirequirementRepository.GetAllQuery().Where(m => m.PbfgeneralId == pbfgeneralid && !filterstrength.Contains(m.StrengthId)).ToList();
                    //if (itemlisttodelete.Count > 0)
                    //{
                    //    //_pidfPbfRndApirequirementRepository.RemoveRange(itemlisttodelete);
                    //    //await _unitOfWork.SaveChangesAsync();
                    //    foreach (var item in itemlisttodelete)
                    //    {
                    //        _pidfPbfRndApirequirementRepository.Remove(item);
                    //    }
                    //    await _unitOfWork.SaveChangesAsync();
                    //}
                    foreach (var item in pbfentity.RNDApirequirements)
                    {
                        var apirequirement = _pidfPbfRndApirequirementRepository.GetAllQuery().Where(m => m.PbfgeneralId == pbfgeneralid && m.StrengthId == item.StrengthId).FirstOrDefault();
                        if (apirequirement != null)
                        {
                            apirequirement.Prototype = item.Prototype;
                            apirequirement.ScaleUp = item.ScaleUp;
                            apirequirement.ExhibitBatch1 = item.ExhibitBatch1;
                            apirequirement.ExhibitBatch2 = item.ExhibitBatch2;
                            apirequirement.ExhibitBatch3 = item.ExhibitBatch3;
                            apirequirement.PrototypeCost = item.PrototypeCost;
                            apirequirement.ScaleUpCost = item.ScaleUpCost;
                            apirequirement.ExhibitBatchCost = item.ExhibitBatchCost;
                            apirequirement.TotalCost = item.TotalCost;
                            apirequirement.CreatedDate = DateTime.Now;
                            apirequirement.CreatedBy = loggedInUserId;
                            _pidfPbfRndApirequirementRepository.UpdateAsync(apirequirement);
                        }
                        else
                        {
                            PidfPbfRnDApirequirement objapirequirement = new PidfPbfRnDApirequirement();
                            objapirequirement = _mapperFactory.Get<RNDApirequirement, PidfPbfRnDApirequirement>(item);
                            objapirequirement.PbfgeneralId = pbfgeneralid;
                            objapirequirement.CreatedDate = DateTime.Now;
                            objapirequirement.CreatedBy = loggedInUserId;
                            _pidfPbfRndApirequirementRepository.AddAsync(objapirequirement);

                        }
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion API Requirement Add Update

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

                #endregion RND Excipient Add Update

                #region RND Packaging Add Update

                //Save Packaging Entities
                if (pbfentity.RNDPackagings != null && pbfentity.RNDPackagings.Count() > 0)
                {
                    var filterpakingtype = pbfentity.RNDPackagings.ToList().Select(x => x.PackingTypeId).ToList();

                    var itemlisttodelete = _pidfPbfRnDPackagingMaterialRepository.GetAllQuery().Where(m => m.PbfgeneralId == pbfgeneralid && !filterpakingtype.Contains((int)m.PackingTypeId)).ToList();
                    if (itemlisttodelete.Count > 0)
                    {
                        //_pidfPbfAnalyticalRepository.RemoveRange(itemlisttodelete);
                        //await _unitOfWork.SaveChangesAsync();
                        foreach (var item in itemlisttodelete)
                        {
                            _pidfPbfRnDPackagingMaterialRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }

                    foreach (var item in pbfentity.RNDPackagings)
                    {
                        var packging = _pidfPbfRnDPackagingMaterialRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid && x.StrengthId == item.StrengthId && x.ActivityTypeId == item.ActivityTypeId && x.PackingTypeId == item.PackingTypeId).FirstOrDefault();

                        if (packging != null)
                        {

                            packging.UnitOfMeasurement = item.UnitOfMeasurement;
                            packging.PackagingDevelopment = item.PackagingDevelopment;
                            packging.RsPerUnit = item.RsPerUnit;
                            packging.Quantity = item.Quantity;
                            packging.CreatedDate = DateTime.Now;
                            packging.CreatedBy = loggedInUserId;
                            _pidfPbfRnDPackagingMaterialRepository.UpdateAsync(packging);

                        }
                        else
                        {
                            PidfPbfRnDPackagingMaterial obgpackaging = new PidfPbfRnDPackagingMaterial();
                            obgpackaging = _mapperFactory.Get<RNDPackaging, PidfPbfRnDPackagingMaterial>(item);
                            obgpackaging.PbfgeneralId = pbfgeneralid;
                            obgpackaging.CreatedDate = DateTime.Now;
                            obgpackaging.CreatedBy = loggedInUserId;
                            _pidfPbfRnDPackagingMaterialRepository.AddAsync(obgpackaging);
                            //objPackaginglist.Add(obgpackaging);
                        }

                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion RND Packaging Add Update

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

                #endregion Tooling Change Part Add Update

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

                #endregion Capex and Miscellaneous Expenses Add Update

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

                #endregion Plant Support Cost Add Update

                #region Reference Product Detail Add Update

                List<PidfPbfRnDReferenceProductDetail> objReferenceProductDetaillist = new();

                //var referenceporduct = _pidfPbfRndReferenceProductDetailRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                //if (referenceporduct.Count > 0)
                //{
                //    foreach (var item in referenceporduct)
                //    {
                //        _pidfPbfRndReferenceProductDetailRepository.Remove(item);
                //    }
                //    await _unitOfWork.SaveChangesAsync();
                //}

                //Save ReferenceProductDetail Entities
                if (pbfentity.RNDReferenceProductDetails != null && pbfentity.RNDReferenceProductDetails.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDReferenceProductDetails)
                    {
                        var referenceporduct = _pidfPbfRndReferenceProductDetailRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid && x.StrengthId == item.StrengthId).FirstOrDefault();

                        if (referenceporduct != null)
                        {
                            referenceporduct.UnitCostOfReferenceProduct = item.UnitCostOfReferenceProduct;
                            referenceporduct.FormulationDevelopment = item.FormulationDevelopment;
                            referenceporduct.PilotBe = item.PilotBe;
                            referenceporduct.PharmasuiticalEquivalence = item.PharmasuiticalEquivalence;
                            referenceporduct.PivotalBio = item.PivotalBio;
                            referenceporduct.TotalCost = item.TotalCost;
                            referenceporduct.CreatedDate = DateTime.Now;
                            referenceporduct.CreatedBy = loggedInUserId;
                            _pidfPbfRndReferenceProductDetailRepository.UpdateAsync(referenceporduct);
                        }
                        else
                        {
                            PidfPbfRnDReferenceProductDetail objreferenceporduct = new PidfPbfRnDReferenceProductDetail();
                            objreferenceporduct = _mapperFactory.Get<RNDReferenceProductDetail, PidfPbfRnDReferenceProductDetail>(item);
                            objreferenceporduct.PbfgeneralId = pbfgeneralid;
                            objreferenceporduct.CreatedDate = DateTime.Now;
                            objreferenceporduct.CreatedBy = loggedInUserId;
                            _pidfPbfRndReferenceProductDetailRepository.AddAsync(referenceporduct);
                        }

                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion Reference Product Detail Add Update

                #region Filling Expenses Add Update


                //Save ReferenceProductDetail Entities
                if (pbfentity.RNDFillingExpenses != null && pbfentity.RNDFillingExpenses.Count() > 0)
                {
                    var filterbusinessunit = pbfentity.RNDFillingExpenses.ToList().Select(x => x.BusinessUnitId).ToList();

                    var itemlisttodelete = _pidfPbfRndFillingExpenseRepository.GetAllQuery().Where(m => m.PbfgeneralId == pbfgeneralid && !filterbusinessunit.Contains(m.BusinessUnitId)).ToList();
                    if (itemlisttodelete.Count > 0)
                    {
                        //_pidfPbfRndFillingExpenseRepository.RemoveRange(itemlisttodelete);
                        //await _unitOfWork.SaveChangesAsync();
                        foreach (var item in itemlisttodelete)
                        {
                            _pidfPbfRndFillingExpenseRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                    foreach (var item in pbfentity.RNDFillingExpenses)
                    {
                        var fillingexpenses = _pidfPbfRndFillingExpenseRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid && x.StrengthId == item.StrengthId && x.BusinessUnitId == item.BusinessUnitId).FirstOrDefault();
                        if (fillingexpenses != null)
                        {
                            fillingexpenses.IsChecked = item.IsChecked;
                            fillingexpenses.TotalCost = item.TotalCost;
                            fillingexpenses.CreatedDate = DateTime.Now;
                            fillingexpenses.CreatedBy = loggedInUserId;
                            _pidfPbfRndFillingExpenseRepository.UpdateAsync(fillingexpenses);
                        }
                        else
                        {
                            PidfPbfRnDFillingExpense objfillingexpense = new PidfPbfRnDFillingExpense();
                            objfillingexpense = _mapperFactory.Get<RNDFillingExpense, PidfPbfRnDFillingExpense>(item);
                            objfillingexpense.PbfgeneralId = pbfgeneralid;
                            objfillingexpense.CreatedDate = DateTime.Now;
                            objfillingexpense.CreatedBy = loggedInUserId;
                            _pidfPbfRndFillingExpenseRepository.AddAsync(objfillingexpense);
                        }
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion Filling Expenses Add Update

                #region Man Power Cost Add Update

                //Save manpowercost Entities
                if (pbfentity.RNDManPowerCosts != null && pbfentity.RNDManPowerCosts.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDManPowerCosts)
                    {
                        var manpowercost = _pidfPbfRndPidfPbfRnDManPowerCostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid && x.ProjectActivitiesId == item.ProjectActivitiesId).FirstOrDefault();
                        if (manpowercost != null)
                        {
                            manpowercost.ManPowerInDays = item.ManPowerInDays;
                            manpowercost.DurationInDays = item.DurationInDays;
                            manpowercost.CreatedDate = DateTime.Now;
                            manpowercost.CreatedBy = loggedInUserId;
                            _pidfPbfRndPidfPbfRnDManPowerCostRepository.UpdateAsync(manpowercost);
                        }

                        PidfPbfRnDManPowerCost objmanpowercost = new PidfPbfRnDManPowerCost();
                        objmanpowercost = _mapperFactory.Get<RNDManPowerCost, PidfPbfRnDManPowerCost>(item);
                        objmanpowercost.PbfgeneralId = pbfgeneralid;
                        objmanpowercost.CreatedDate = DateTime.Now;
                        objmanpowercost.CreatedBy = loggedInUserId;
                        _pidfPbfRndPidfPbfRnDManPowerCostRepository.AddAsync(objmanpowercost);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion Man Power Cost Add Update

                #endregion RND Add Update

                return pbfgeneralid;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return pbfgeneralid;
            }
        }

        public async Task<long> SavePidfAndPBFCommanDetailsnew1(long pidfid, PBFFormEntity pbfentity)
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
                var OldPBFEntity = _mapperFactory.Get<PidfPbf, PBFFormEntity>(objPIDFPbf);
                if (objPIDFPbf != null)
                {
                    objPIDFPbf = _mapperFactory.Get<PBFFormEntity, PidfPbf>(pbfentity);
                    objPIDFPbf.ModifyBy = loggedInUserId;
                    objPIDFPbf.ModifyDate = DateTime.Now;
                    _pbfRepository.UpdateAsync(objPIDFPbf);
                    await _unitOfWork.SaveChangesAsync();
                    var isSuccess = await _auditLogService.CreateAuditLog<PBFFormEntity>(Utility.Audit.AuditActionType.Update,
                  ModuleEnum.PBF, OldPBFEntity, pbfentity, (int)pbfentity.Pidfid);
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

                #endregion Section PBF Add Update

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

                #endregion Marketting Mapping Add Update

                #region Update PIDF

                objPIDFupdate = _repository.GetAllQuery().Where(x => x.Pidfid == pidfid).FirstOrDefault();
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

                #endregion Update PIDF

                #region Section PBF General Add Update

                #region GeneralProductStrength Add Update
                List<PidfPbfGeneralStrength> _objPidfPbfGeneralStrength = new List<PidfPbfGeneralStrength>();

                //Save General Strength Entities Table
                if (pbfentity.GeneralStrengthEntities != null && pbfentity.GeneralStrengthEntities.Count() > 0)
                {
                    foreach (var item in pbfentity.GeneralStrengthEntities)
                    {
                        PidfPbfGeneralStrength pidfPbfGeneralStrength = new PidfPbfGeneralStrength();
                        pidfPbfGeneralStrength = _mapperFactory.Get<GeneralStrengthEntity, PidfPbfGeneralStrength>(item);
                        pidfPbfGeneralStrength.PbfgeneralId = pbfgeneralid;
                        pidfPbfGeneralStrength.CreatedDate = DateTime.Now;
                        pidfPbfGeneralStrength.CreatedBy = loggedInUserId;
                        _objPidfPbfGeneralStrength.Add(pidfPbfGeneralStrength);
                    }
                }
                #endregion GeneralProductStrength Add Update

                #region Section Clinical Add Update

                List<PidfPbfClinical> objClinicallist = new();
               
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
                }

                #endregion Section Clinical Add Update

                #region Section Analytical Add Update

                List<PidfPbfAnalytical> objAnalyticallist = new();
                List<PidfPbfAnalyticalAmvcost> objAnalyticalAmvcosts = new();
                List<PidfPbfAnalyticalAmvcostStrengthMapping> objAnalyticalAmvcostStrengthMappinglist = new();
               
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
                }
                //Save analytical Total strength mapping start
                if (pbfentity.AnalyticalStrengthMappingEntities != null && pbfentity.AnalyticalStrengthMappingEntities.Count() > 0)
                {
                    foreach (var item in pbfentity.AnalyticalStrengthMappingEntities)
                    {
                        PidfPbfAnalyticalAmvcostStrengthMapping objstrengthmapping = new PidfPbfAnalyticalAmvcostStrengthMapping();
                        objstrengthmapping = _mapperFactory.Get<AnalyticalAmvcostStrengthMappingEntity, PidfPbfAnalyticalAmvcostStrengthMapping>(item);
                        // objstrengthmapping.TotalAmvcostId = TotalAMVCostId;
                        objstrengthmapping.CreatedDate = DateTime.Now;
                        objstrengthmapping.CreatedBy = loggedInUserId;
                        objAnalyticalAmvcostStrengthMappinglist.Add(objstrengthmapping);
                    }
                }
                //Save analytical cost end

                //Save analytical cost start
                long TotalAMVCostId = 0;
                if (pbfentity.AnalyticalAMVCosts != null)
                {
                    PidfPbfAnalyticalAmvcost obganalyticalamvcost = new PidfPbfAnalyticalAmvcost();
                    obganalyticalamvcost.TotalAmvtitle = pbfentity.AnalyticalAMVCosts.TotalAmvtitle;
                    obganalyticalamvcost.TotalAmvcost = pbfentity.AnalyticalAMVCosts.TotalAmvcost;
                    obganalyticalamvcost.Remark = pbfentity.AnalyticalAMVCosts.Remark;
                    obganalyticalamvcost.PbfgeneralId = pbfgeneralid;
                    obganalyticalamvcost.CreatedDate = DateTime.Now;
                    obganalyticalamvcost.CreatedBy = loggedInUserId;
                    obganalyticalamvcost.PidfPbfAnalyticalAmvcostStrengthMappings = objAnalyticalAmvcostStrengthMappinglist;
                    objAnalyticalAmvcosts.Add(obganalyticalamvcost);
                }
                //Save analytical cost end

                #endregion Section Analytical Add Update

                #region RND Add Update

                #region RND Master Add Update
                List<PidfPbfRnDMaster> objrndMasters = new();
                if (pbfentity.RNDMasterEntities != null)
                {
                    PidfPbfRnDMaster objMaster = new PidfPbfRnDMaster();
                    objMaster = _mapperFactory.Get<RNDMasterEntity, PidfPbfRnDMaster>(pbfentity.RNDMasterEntities);
                    objMaster.PbfgeneralId = pbfgeneralid;
                    objMaster.CreatedBy = loggedInUserId;
                    objMaster.CreatedDate = DateTime.Now;
                    objrndMasters.Add(objMaster);
                }


                #endregion RND Master Add Update

                #region Batch Size Add Update

                List<PidfPbfRndBatchSize> objBatchSizelist = new();

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
                   
                }

                #endregion Batch Size Add Update

                #region API Requirement Add Update

                List<PidfPbfRnDApirequirement> objApirequirementlist = new();

               

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
                    //_pidfPbfRndApirequirementRepository.AddRangeAsync(objApirequirementlist);
                    //await _unitOfWork.SaveChangesAsync();
                }

                #endregion API Requirement Add Update

                #region RND Excipient Add Update

                List<PidfPbfRnDExicipientRequirement> objExicipientlist = new();
               
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
                    //_pidfPbfRnDExicipientRequirementRepository.AddRangeAsync(objExicipientlist);
                    //await _unitOfWork.SaveChangesAsync();
                }

                #endregion RND Excipient Add Update

                #region RND Packaging Add Update

                List<PidfPbfRnDPackagingMaterial> objPackaginglist = new();
                
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
                    //_pidfPbfRnDPackagingMaterialRepository.AddRangeAsync(objPackaginglist);
                    //await _unitOfWork.SaveChangesAsync();
                }

                #endregion RND Packaging Add Update

                #region Tooling Change Part Add Update

                List<PidfPbfRnDToolingChangepart> objToolingChangePartCostlist = new();
                                
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
                    //_pidfPbfRndToolingChangePartCostRepository.AddRangeAsync(objToolingChangePartCostlist);
                    //await _unitOfWork.SaveChangesAsync();
                }

                #endregion Tooling Change Part Add Update

                #region Capex and Miscellaneous Expenses Add Update

                List<PidfPbfRnDCapexMiscellaneousExpense> objCapexMiscellaneouslist = new();
                               
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
                    //_pidfPbfRndCapexMiscellaneousExpenseRepository.AddRangeAsync(objCapexMiscellaneouslist);
                    //await _unitOfWork.SaveChangesAsync();
                }

                #endregion Capex and Miscellaneous Expenses Add Update

                #region Plant Support Cost Add Update

                List<PidfPbfRnDPlantSupportCost> objPlantSupportCostlist = new();
                               
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
                    //_pidfPbfRndPlantSupportCostRepository.AddRangeAsync(objPlantSupportCostlist);
                    //await _unitOfWork.SaveChangesAsync();
                }

                #endregion Plant Support Cost Add Update

                #region Reference Product Detail Add Update

                List<PidfPbfRnDReferenceProductDetail> objReferenceProductDetaillist = new();
                               
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
                    //_pidfPbfRndReferenceProductDetailRepository.AddRangeAsync(objReferenceProductDetaillist);
                    //await _unitOfWork.SaveChangesAsync();
                }

                #endregion Reference Product Detail Add Update

                #region Filling Expenses Add Update

                List<PidfPbfRnDFillingExpense> objFillinfExpenseslist = new();
                               
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
                    //_pidfPbfRndFillingExpenseRepository.AddRangeAsync(objFillinfExpenseslist);
                    //await _unitOfWork.SaveChangesAsync();
                }

                #endregion Filling Expenses Add Update

                #region Man Power Cost Add Update

                List<PidfPbfRnDManPowerCost> objManPowerCostlist = new();
                               
                //Save manpowercost Entities
                if (pbfentity.RNDManPowerCosts != null && pbfentity.RNDManPowerCosts.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDManPowerCosts)
                    {
                        PidfPbfRnDManPowerCost objmanpowercost = new PidfPbfRnDManPowerCost();
                        objmanpowercost = _mapperFactory.Get<RNDManPowerCost, PidfPbfRnDManPowerCost>(item);
                        objmanpowercost.PbfgeneralId = pbfgeneralid;
                        objmanpowercost.CreatedDate = DateTime.Now;
                        objmanpowercost.CreatedBy = loggedInUserId;
                        objManPowerCostlist.Add(objmanpowercost);
                    }
                    //_pidfPbfRndPidfPbfRnDManPowerCostRepository.AddRangeAsync(objManPowerCostlist);
                    //await _unitOfWork.SaveChangesAsync();
                }

                #endregion Man Power Cost Add Update

                #endregion RND Add Update
                //PidfPbfGeneral objPIDFGeneralupdate;
                var objPIDFGeneralupdate = _pidfPbfGeneralRepository.GetAllQuery().Where(x => x.Pidfpbfid == pbfentity.Pidfpbfid && x.BusinessUnitId == pbfentity.BusinessUnitId).FirstOrDefault();
                if (objPIDFGeneralupdate != null)
                {
                    //objPIDFGeneralupdate = _mapperFactory.Get<PBFFormEntity, PidfPbfGeneral>(pbfentity);

                    //objPIDFGeneralupdate.BusinessUnitId = pbfentity.BusinessUnitId;
                    objPIDFGeneralupdate.Capex = pbfentity.Capex;
                    objPIDFGeneralupdate.TotalExpense = pbfentity.TotalExpense;
                    objPIDFGeneralupdate.ProjectComplexity = pbfentity.ProjectComplexity;
                    objPIDFGeneralupdate.ProductTypeId = pbfentity.GeneralProductTypeId;
                    objPIDFGeneralupdate.TestLicenseAvailability = pbfentity.TestLicenseAvailability;
                    objPIDFGeneralupdate.BudgetTimelineSubmissionDate = pbfentity.BudgetTimelineSubmissionDate;
                    objPIDFGeneralupdate.ProjectDevelopmentInitialDate = pbfentity.ProjectDevelopmentInitialDate;
                    objPIDFGeneralupdate.FormulationGlid = pbfentity.FormulationGLId;
                    objPIDFGeneralupdate.AnalyticalGlid = pbfentity.AnalyticalGLId;
                    objPIDFGeneralupdate.PidfPbfGeneralStrengths = _objPidfPbfGeneralStrength;
                    objPIDFGeneralupdate.PidfPbfClinicals = objClinicallist;
                    objPIDFGeneralupdate.PidfPbfAnalyticals = objAnalyticallist;
                    objPIDFGeneralupdate.PidfPbfAnalyticalAmvcosts = objAnalyticalAmvcosts;
                    objPIDFGeneralupdate.PidfPbfRnDMasters = objrndMasters;
                    objPIDFGeneralupdate.PidfPbfRndBatchSizes = objBatchSizelist;
                    objPIDFGeneralupdate.PidfPbfRnDApirequirements = objApirequirementlist;
                    objPIDFGeneralupdate.PidfPbfRnDExicipientRequirements = objExicipientlist;
                    objPIDFGeneralupdate.PidfPbfRnDPackagingMaterials = objPackaginglist;
                    objPIDFGeneralupdate.PidfPbfRnDToolingChangeparts = objToolingChangePartCostlist;
                    objPIDFGeneralupdate.PidfPbfRnDCapexMiscellaneousExpenses = objCapexMiscellaneouslist;
                    objPIDFGeneralupdate.PidfPbfRnDPlantSupportCosts = objPlantSupportCostlist;
                    objPIDFGeneralupdate.PidfPbfRnDReferenceProductDetails = objReferenceProductDetaillist;
                    objPIDFGeneralupdate.PidfPbfRnDFillingExpenses = objFillinfExpenseslist;
                    objPIDFGeneralupdate.PidfPbfRnDManPowerCosts = objManPowerCostlist;
                    DeleteGeneralChildRecords(objPIDFGeneralupdate.PbfgeneralId);
                    _pidfPbfGeneralRepository.UpdateAsync(objPIDFGeneralupdate);
                    await _unitOfWork.SaveChangesAsync();
                    pbfgeneralid = objPIDFGeneralupdate.PbfgeneralId;
                }
                else
                {
                    PidfPbfGeneral objPIDFGeneraladd = new PidfPbfGeneral();
                    objPIDFGeneraladd.Pidfpbfid = pidfpbfid;
                    objPIDFGeneraladd.BusinessUnitId = pbfentity.BusinessUnitId;
                    objPIDFGeneraladd.Capex = pbfentity.Capex;
                    objPIDFGeneraladd.TotalExpense = pbfentity.TotalExpense;
                    objPIDFGeneraladd.ProjectComplexity = pbfentity.ProjectComplexity;
                    objPIDFGeneraladd.ProductTypeId = pbfentity.GeneralProductTypeId;
                    objPIDFGeneraladd.TestLicenseAvailability = pbfentity.TestLicenseAvailability;
                    objPIDFGeneraladd.BudgetTimelineSubmissionDate = pbfentity.BudgetTimelineSubmissionDate;
                    objPIDFGeneraladd.ProjectDevelopmentInitialDate = pbfentity.ProjectDevelopmentInitialDate;
                    objPIDFGeneraladd.FormulationGlid = pbfentity.FormulationGLId;
                    objPIDFGeneraladd.AnalyticalGlid = pbfentity.AnalyticalGLId;
                    objPIDFGeneraladd.CreatedBy = loggedInUserId;
                    objPIDFGeneraladd.CreatedDate = DateTime.Now;
                    objPIDFGeneraladd.PidfPbfGeneralStrengths = _objPidfPbfGeneralStrength;
                    objPIDFGeneraladd.PidfPbfClinicals = objClinicallist;
                    objPIDFGeneraladd.PidfPbfAnalyticals = objAnalyticallist;
                    objPIDFGeneraladd.PidfPbfAnalyticalAmvcosts = objAnalyticalAmvcosts;
                    objPIDFGeneraladd.PidfPbfRnDMasters = objrndMasters;
                    objPIDFGeneraladd.PidfPbfRndBatchSizes = objBatchSizelist;
                    objPIDFGeneraladd.PidfPbfRnDApirequirements = objApirequirementlist;
                    objPIDFGeneraladd.PidfPbfRnDExicipientRequirements = objExicipientlist;
                    objPIDFGeneraladd.PidfPbfRnDPackagingMaterials = objPackaginglist;
                    objPIDFGeneraladd.PidfPbfRnDToolingChangeparts = objToolingChangePartCostlist;
                    objPIDFGeneraladd.PidfPbfRnDCapexMiscellaneousExpenses = objCapexMiscellaneouslist;
                    objPIDFGeneraladd.PidfPbfRnDPlantSupportCosts = objPlantSupportCostlist;
                    objPIDFGeneraladd.PidfPbfRnDReferenceProductDetails = objReferenceProductDetaillist;
                    objPIDFGeneraladd.PidfPbfRnDFillingExpenses = objFillinfExpenseslist;
                    objPIDFGeneraladd.PidfPbfRnDManPowerCosts = objManPowerCostlist;
                    _pidfPbfGeneralRepository.AddAsync(objPIDFGeneraladd);
                    await _unitOfWork.SaveChangesAsync();
                    pbfgeneralid = objPIDFGeneraladd.PbfgeneralId;
                }

                #endregion Section PBF General Add Update
                return pbfgeneralid;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return pbfgeneralid;
            }
        }

        public void DeleteGeneralChildRecords(long pbfgeneralid)
        {

            try
            {
                var generalStrength = _pidfPbfGeneralStrengthRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (generalStrength.Count > 0)
                {
                    foreach (var item in generalStrength)
                    {
                        _pidfPbfGeneralStrengthRepository.Remove(item);
                    }
                }

                var clinical = _pidfPbfClinicalRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (clinical.Count > 0)
                {
                    foreach (var item in clinical)
                    {
                        _pidfPbfClinicalRepository.Remove(item);
                    }
                    //await _unitOfWork.SaveChangesAsync();
                }
                var analytical = _pidfPbfAnalyticalRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (analytical.Count > 0)
                {
                    foreach (var item in analytical)
                    {
                        _pidfPbfAnalyticalRepository.Remove(item);
                    }
                    // await _unitOfWork.SaveChangesAsync();
                }
                var analyticalCost = _PidfPbfAnalyticalAmvcostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (analyticalCost.Count > 0)
                {
                    foreach (var item in analyticalCost)
                    {
                       var amvstrengthmapping =  _pidfPbfAnalyticalAmvcostStrengthMappingRepository.GetAllQuery().Where(x => x.TotalAmvcostId == item.TotalAmvcostId).ToList();
                        if (amvstrengthmapping.Count > 0)
                        {
                            foreach (var it in amvstrengthmapping)
                            {
                                _pidfPbfAnalyticalAmvcostStrengthMappingRepository.Remove(it);
                            }
                        }
                        _PidfPbfAnalyticalAmvcostRepository.Remove(item);
                    }
                    // await _unitOfWork.SaveChangesAsync();
                }
                var rndmaster = _pidfPbfRnDMasterRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (rndmaster.Count > 0)
                {
                    foreach (var item in rndmaster)
                    {
                        _pidfPbfRnDMasterRepository.Remove(item);
                    }
                }

                var batchsize = _pidfPbfRndBatchSizeRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (batchsize.Count > 0)
                {
                    foreach (var item in batchsize)
                    {
                        _pidfPbfRndBatchSizeRepository.Remove(item);
                    }
                   // await _unitOfWork.SaveChangesAsync();
                }
                var apirequirement = _pidfPbfRndApirequirementRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (apirequirement.Count > 0)
                {
                    foreach (var item in apirequirement)
                    {
                        _pidfPbfRndApirequirementRepository.Remove(item);
                    }
                    //await _unitOfWork.SaveChangesAsync();
                }
                var exicipient = _pidfPbfRnDExicipientRequirementRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (exicipient.Count > 0)
                {
                    foreach (var item in exicipient)
                    {
                        _pidfPbfRnDExicipientRequirementRepository.Remove(item);
                    }
                    //await _unitOfWork.SaveChangesAsync();
                }
                var packging = _pidfPbfRnDPackagingMaterialRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (packging.Count > 0)
                {
                    foreach (var item in packging)
                    {
                        _pidfPbfRnDPackagingMaterialRepository.Remove(item);
                    }
                    //await _unitOfWork.SaveChangesAsync();
                }
                var toolongchangepart = _pidfPbfRndToolingChangePartCostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (toolongchangepart.Count > 0)
                {
                    foreach (var item in toolongchangepart)
                    {
                        _pidfPbfRndToolingChangePartCostRepository.Remove(item);
                    }
                    //await _unitOfWork.SaveChangesAsync();
                }
                var capexandmiscellaneous = _pidfPbfRndCapexMiscellaneousExpenseRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (capexandmiscellaneous.Count > 0)
                {
                    foreach (var item in capexandmiscellaneous)
                    {
                        _pidfPbfRndCapexMiscellaneousExpenseRepository.Remove(item);
                    }
                    //await _unitOfWork.SaveChangesAsync();
                }
                var plantsupportcost = _pidfPbfRndPlantSupportCostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (plantsupportcost.Count > 0)
                {
                    foreach (var item in plantsupportcost)
                    {
                        _pidfPbfRndPlantSupportCostRepository.Remove(item);
                    }
                   // await _unitOfWork.SaveChangesAsync();
                }
                var referenceporduct = _pidfPbfRndReferenceProductDetailRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (referenceporduct.Count > 0)
                {
                    foreach (var item in referenceporduct)
                    {
                        _pidfPbfRndReferenceProductDetailRepository.Remove(item);
                    }
                   // await _unitOfWork.SaveChangesAsync();
                }
                var fillingexpenses = _pidfPbfRndFillingExpenseRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (fillingexpenses.Count > 0)
                {
                    foreach (var item in fillingexpenses)
                    {
                        _pidfPbfRndFillingExpenseRepository.Remove(item);
                    }
                    //await _unitOfWork.SaveChangesAsync();
                }
                var manpowercost = _pidfPbfRndPidfPbfRnDManPowerCostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (manpowercost.Count > 0)
                {
                    foreach (var item in manpowercost)
                    {
                        _pidfPbfRndPidfPbfRnDManPowerCostRepository.Remove(item);
                    }
                    //await _unitOfWork.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                _ExceptionService.LogException(ex);
            }
        }

        #endregion Private Methods
    }
}