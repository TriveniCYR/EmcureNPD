using Dapper;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.IO;
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
        private readonly IConfiguration _configuration;
        private IRepository<PidfPbf> _pbfRepository { get; set; }
        private IRepository<Pidf> _repository { get; set; }
        private IRepository<PidfPbfReferenceProductDetail> _repositoryPidfPbfReferenceProductDetail { get; set; }
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
        private IRepository<PidfPbfHeadWiseBudget> _pidfPbfHeadWiseBudgetRepository { get; set; }
        private IRepository<PidfPbfPhaseWiseBudget> _pidfPbfPhaseWiseBudgetRepository { get; set; }
        private IRepository<MasterPlantLine> _MasterPlantLineRepository { get; set; }
        private IRepository<PidfPbfRa> _pidfPbfRRepositiry { get; set; }
        private IRepository<MasterTypeOfSubmission> _masterTypeOfSubmission { get; set; }

        private IRepository<PidfPbfGeneralRnd> _repositoryPidfPbfGeneralRnd { get; set; }
        private IRepository<PidfPbfRnDPackSizeStability> _repositoryPidfPbfRnDPackSizeStability { get; set; }
        private IRepository<MasterNationApproval> _masterNationApproval { get; set; }
        private IRepository<MasterNationApprovalCountryMapping> _masterNationApprovalCountryMpping { get; set; }
        private IRepository<MasterCountry> _masterCountry { get; set; }
        private IRepository<PbfGeneralTdp> _PbfGeneralTdp { get; set; }
        public PBFService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, INotificationService notificationService, IMasterAuditLogService auditLogService,

            IHelper helper, IExceptionService exceptionService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _auditLogService = auditLogService;
            _helper = helper;
            _notificationService = notificationService;
            _ExceptionService = exceptionService;
            _configuration = configuration;
            _repositoryPidfPbfReferenceProductDetail = _unitOfWork.GetRepository<PidfPbfReferenceProductDetail>();
            _repository = _unitOfWork.GetRepository<Pidf>();
            _pbfRepository = _unitOfWork.GetRepository<PidfPbf>();
            _MasterPlantLineRepository = _unitOfWork.GetRepository<MasterPlantLine>();
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
            _pidfPbfHeadWiseBudgetRepository = _unitOfWork.GetRepository<PidfPbfHeadWiseBudget>();
            _pidfPbfPhaseWiseBudgetRepository = _unitOfWork.GetRepository<PidfPbfPhaseWiseBudget>();
            _pidfPbfRRepositiry = _unitOfWork.GetRepository<PidfPbfRa>();
            _masterTypeOfSubmission = _unitOfWork.GetRepository<MasterTypeOfSubmission>();
            _repositoryPidfPbfGeneralRnd = _unitOfWork.GetRepository<PidfPbfGeneralRnd>();
            _repositoryPidfPbfRnDPackSizeStability = _unitOfWork.GetRepository<PidfPbfRnDPackSizeStability>();
            _masterNationApproval = _unitOfWork.GetRepository<MasterNationApproval>();
            _masterNationApprovalCountryMpping = _unitOfWork.GetRepository<MasterNationApprovalCountryMapping>();
            _masterCountry = _unitOfWork.GetRepository<MasterCountry>();
            _PbfGeneralTdp = _unitOfWork.GetRepository<PbfGeneralTdp>();
        }

        public async Task<dynamic> FillDropdown(int PIDFId, int selectedbusinessunit)
        {
            dynamic DropdownObjects = new ExpandoObject();

            var loggedInUserId = _helper.GetLoggedInUser().UserId;

            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", loggedInUserId),
                new SqlParameter("@selectedbusinessunit", selectedbusinessunit),
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
                if (dbresult.Tables[2] != null && dbresult.Tables[2].Rows.Count > 0)
                {
                    data.OralName = dbresult.Tables[2].Rows[0]["OralName"].ToString();
                }
            }
            return data;
        }
        public async Task<PidfPbfGeneralRndEntity> GetPidfPbfGeneralRnd(long pidfId, long PbfId, long PbfRndDetailsId = 0, long BusinessUnitId = 0)
        {
            var data = new PidfPbfGeneralRndEntity();
            SqlParameter[] osqlParameter = {
                       new SqlParameter("@PbfRndDetailsId", PbfRndDetailsId),
                       new SqlParameter("@PidfId", pidfId),
                       new SqlParameter("@PbfId", PbfId),
                       new SqlParameter("@BusinessUnitId",BusinessUnitId)
                   };

            var dbresult = await _pbfRepository.GetDataSetBySP("SpGetPIDF_PBF_General_RND", System.Data.CommandType.StoredProcedure, osqlParameter);
            if (dbresult != null)
            {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                {
                    data = dbresult.Tables[0].DataTableToList<PidfPbfGeneralRndEntity>()[0];

                }
            }
            return data;
        }
        //GetGeneralPackSizeStability
        public async Task<PidfProductStrengthGeneralRanD> GetGeneralPackSizeStability(long pidfId, int BUId)
        {
            var data = new PidfProductStrengthGeneralRanD();
            try
            {
                SqlParameter[] osqlParameter = {
                       new SqlParameter("@PIDFId", pidfId),
                        new SqlParameter("@BUId", BUId)
                   };

                var dbresult = await _pbfRepository.GetDataSetBySP("GetPackSizeStabilityData", System.Data.CommandType.StoredProcedure, osqlParameter);
                if (dbresult != null)
                {
                    if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                    {

                        data.PidfProductStrengthGeneralRanDList = dbresult.Tables[0].DataTableToList<PidfProductStrengthGeneralRanD>();
                    }
                    if (dbresult.Tables[1] != null && dbresult.Tables[1].Rows.Count > 0)
                    {
                        data.PidfPackSizeGeneralRanDList = dbresult.Tables[1].DataTableToList<PidfPackSizeGeneralRanD>();

                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public async Task<PidfProductStrengthGeneralRanD> GetCountryWisePackSizeStabilityData(long pidfId, int BUId, int countryid)
        {
            var data = new PidfProductStrengthGeneralRanD();
            try
            {
                SqlParameter[] osqlParameter = {
                       new SqlParameter("@PIDFId", pidfId),
                        new SqlParameter("@BUId", BUId),
                         new SqlParameter("@CountryId", countryid),
                   };

                var dbresult = await _pbfRepository.GetDataSetBySP("GetCountryWisePackSizeStabilityData", System.Data.CommandType.StoredProcedure, osqlParameter);
                if (dbresult != null)
                {
                    if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                    {

                        data.PidfProductStrengthGeneralRanDList = dbresult.Tables[0].DataTableToList<PidfProductStrengthGeneralRanD>();
                    }
                    if (dbresult.Tables[1] != null && dbresult.Tables[1].Rows.Count > 0)
                    {
                        data.PidfPackSizeGeneralRanDList = dbresult.Tables[1].DataTableToList<PidfPackSizeGeneralRanD>();

                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                return data;
            }
        }
        public async Task<PidfProductStrengthGeneralRanD> GetStrengthForPBFTDP(long pidfId,int BUId)
        {
            var data = new PidfProductStrengthGeneralRanD();
            try
            {
                SqlParameter[] osqlParameter = {
                       new SqlParameter("@PIDFId", pidfId),
                       new SqlParameter("@BUId", BUId)
                   };

                var dbresult = await _pbfRepository.GetDataSetBySP("GetStrengthForPBF_TDP", System.Data.CommandType.StoredProcedure, osqlParameter);
                if (dbresult != null)
                {
                    if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                    {

                        data.PidfProductStrengthGeneralRanDList = dbresult.Tables[0].DataTableToList<PidfProductStrengthGeneralRanD>();
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public async Task<CountyForBussinessUnitAndPIDF> GetCountyForBussinessUnitAndPIDF(long pidfId, int BUId)
        {
            var data = new CountyForBussinessUnitAndPIDF();
            try
            {
                SqlParameter[] osqlParameter = {
                       new SqlParameter("@PIDFID", pidfId),
                        new SqlParameter("@BUId", BUId)
                   };

                var dbresult = await _pbfRepository.GetDataSetBySP("stp_npd_GetCountryListByIsInterested", System.Data.CommandType.StoredProcedure, osqlParameter);
                if (dbresult != null)
                {
                    if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                    {

                        data.CountyForBussinessUnitAndPIDFList = dbresult.Tables[0].DataTableToList<CountyForBussinessUnitAndPIDF>();
                    }

                }
                return data;
            }
            catch (Exception ex)
            {
                return data;
            }
        }
        public async Task<DBOperation> AddUpdatePBFDetails(PBFFormEntity pbfEntity, IFormFileCollection files,string _webrootPath)
        {
            try
            {
                //Dummy function to same PIDFPBF Data
                long pbfgeneralid = 0;
                

                var loggedInUserId = _helper.GetLoggedInUser().UserId;
                pbfgeneralid = await SavePidfAndPBFCommanDetailsnew1(pbfEntity.Pidfid, pbfEntity,files,_webrootPath);

                if (pbfgeneralid > 0)
                {
                   
                    // var isSuccess = await _auditLogService.CreateAuditLog<PBFFormEntity>(pbfEntity.Pidfpbfid > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
                    //Utility.Enums.ModuleEnum.PBF, pbfEntity, pbfEntity, Convert.ToInt32(pbfEntity.Pidfid));
                    await _unitOfWork.SaveChangesAsync();
                    var _StatusID = (pbfEntity.SaveType == "Save") ? Master_PIDFStatus.PBFSubmitted : Master_PIDFStatus.PBFInProgress;
                    if (pbfEntity.PBFnextbutton == false)
                    {
                        await _auditLogService.UpdatePIDFStatusCommon(pbfEntity.Pidfid, (int)_StatusID, loggedInUserId);
                        await _notificationService.CreateNotification(pbfEntity.Pidfid, (int)_StatusID, string.Empty, string.Empty, loggedInUserId);
                    }
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

        public async Task<dynamic> PBFAllTabDetails(int PIDFId, int BUId, int pbfId = 0, int PbfRndDetailsId = 0, string APIurl=null)
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
            DropdownObjects.HeadWiseBudget = dsDropdownOptions.Tables[20];
            DropdownObjects.PBFReferenceProductDetail = dsDropdownOptions.Tables[21];
            DropdownObjects.RNDExicipientPrototype = dsDropdownOptions.Tables[22];
            DropdownObjects.PidfPbfGeneralRnd = await GetPidfPbfGeneralRnd(PIDFId, pbfId, PbfRndDetailsId, BUId);
            DropdownObjects.PidfPbfGeneralPackSizeStability = await GetGeneralPackSizeStability(PIDFId, BUId);
            DropdownObjects.GetStrengthForPBFTDP = await GetStrengthForPBFTDP(PIDFId, BUId);
            DropdownObjects.GetTDPList = await GetTDT(PIDFId, APIurl);
            DropdownObjects.GetCountyForBussinessUnitAndPIDF = await GetCountyForBussinessUnitAndPIDF(PIDFId, BUId);
            return DropdownObjects;
        }


        public async Task<List<MasterPlantLineEntity>> GetLineByPlantId(int id)
        {
            var dbObj = await _MasterPlantLineRepository.GetAllAsync(x => x.PlantId == id);
            return _mapperFactory.GetList<MasterPlantLine, MasterPlantLineEntity>(dbObj.ToList());
        }
        public async Task<List<PidfPbfRaEntity>> GetRa(int PidfId, int PifdPbfId, int BuId)
        {
            var dbObj = await _pidfPbfRRepositiry.GetAllAsync(x => x.Pidfid == PidfId && x.Pbfid == PifdPbfId && x.BuId == BuId);
            return _mapperFactory.GetList<PidfPbfRa, PidfPbfRaEntity>(dbObj.ToList());
        }
        public async Task<List<MasterTypeOfSubmissionEntity>> GetTypeOfSubmission()
        {
            var dbObj = await _masterTypeOfSubmission.GetAllAsync();
            return _mapperFactory.GetList<MasterTypeOfSubmission, MasterTypeOfSubmissionEntity>(dbObj.ToList());
        }
        public async Task<List<MasterNationApprovalEntity>> GetNationApprovals()
        {
            var MasterNationApprovaldbObj = await _masterNationApproval.GetAllAsync();
            var MasterCountryMappingdbObj = await _masterNationApprovalCountryMpping.GetAllAsync();
            var MasterCountrydbObj = await _masterCountry.GetAllAsync();
            var query = from nationApproval in MasterNationApprovaldbObj
                        join countryMapping in MasterCountryMappingdbObj
                            on nationApproval.NationApprovalId equals countryMapping.NationApprovalId
                        join country in MasterCountrydbObj
                            on countryMapping.CountryId equals country.CountryId
                        orderby nationApproval.NationApprovalId
                        select new
                        {
                            nationApproval.NationApprovalId,
                            nationApproval.MinEop,
                            nationApproval.MaxEop,
                            country.CountryId,
                            country.CountryName
                        };

            var result = new List<MasterNationApprovalEntity>();
            var groupedData = query.GroupBy(item => new { item.NationApprovalId, item.MinEop, item.MaxEop });

            foreach (var group in groupedData)
            {
                var entity = new MasterNationApprovalEntity
                {
                    NationApprovalId = group.Key.NationApprovalId,
                    MinEOP = group.Key.MinEop,
                    MaxEOP = group.Key.MaxEop,
                    CountryDetails = group.Select(item => new CountryDetails
                    {
                        CountryId = item.CountryId,
                        CountryName = item.CountryName
                    }).ToList()
                };
                result.Add(entity);
            }
            return result.ToList();
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

                if (pidfpbfid > 0 && pbfentity.MarketMappingId != null && pbfentity.MarketMappingId.Length > 0)
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
                        objMM.BusinessUnitId = (int)item;
                        objMM.Pidfpbfid = pidfpbfid;
                        objMM.CreatedBy = loggedInUserId;
                        objMM.CreatedDate = DateTime.Now;
                        objmapping.Add(objMM);
                    }
                    _pidfPbfMarketMappingRepository.AddRange(objmapping);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion Marketting Mapping Add Update



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
                #region Update PBF Reference Product Details
                await SaveUpdateReferenceProductDetails(pbfgeneralid, pbfentity);
                #endregion Update PBF Reference Product Details

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
                        objrndMaster.ApirequirementVendorName = pbfentity.RNDMasterEntities.ApirequirementVendorName;

                        objrndMaster.ManHourRate = pbfentity.RNDMasterEntities.ManHourRate;
                        objrndMaster.PlanSupportCostRsPerDay = pbfentity.RNDMasterEntities.PlanSupportCostRsPerDay;

                        objrndMaster.PlantId = pbfentity.RNDMasterEntities.PlantId_Tab;
                        objrndMaster.LineId = pbfentity.RNDMasterEntities.PBFLine;
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
                        objRndMaster.PlantId = pbfentity.RNDMasterEntities.PlantId_Tab;
                        objRndMaster.LineId = pbfentity.RNDMasterEntities.PBFLine;

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
        private async Task<long> SaveUpdateReferenceProductDetails(long PbfGeneralId, PBFFormEntity pbfentity)
        {
            var New_ObjProductRefDetails = new PidfPbfReferenceProductDetail();

            var ObjProductRefDetails = _repositoryPidfPbfReferenceProductDetail.GetAllQuery().
                Where(x => x.Pidfid == pbfentity.Pidfid && x.BusinessUnitId == pbfentity.BusinessUnitId).ToList();
            if (ObjProductRefDetails != null)
            {
                foreach (var item in ObjProductRefDetails)
                {
                    _repositoryPidfPbfReferenceProductDetail.Remove(item);
                }
                await _unitOfWork.SaveChangesAsync();
            }
            try
            {
                New_ObjProductRefDetails.BusinessUnitId = pbfentity.BusinessUnitId;
                //New_ObjProductRefDetails.PbfgeneralId = 0;
                New_ObjProductRefDetails.Pidfid = pbfentity.Pidfid;
                New_ObjProductRefDetails.Rfdbrand = pbfentity.BrandName;
                New_ObjProductRefDetails.Rfdindication = pbfentity.RFDIndication;
                New_ObjProductRefDetails.Rfdapplicant = pbfentity.RFDApplicant;
                New_ObjProductRefDetails.RfdcountryId = pbfentity.RFDCountryId;
                New_ObjProductRefDetails.Rfdinnovators = pbfentity.RFDInnovators;
                New_ObjProductRefDetails.RfdinitialRevenuePotential = pbfentity.RFDInitialRevenuePotential;
                New_ObjProductRefDetails.RfdpriceDiscounting = pbfentity.RFDPriceDiscounting;
                New_ObjProductRefDetails.RfdcommercialBatchSize = pbfentity.RFDCommercialBatchSize;
                _repositoryPidfPbfReferenceProductDetail.AddAsync(New_ObjProductRefDetails);
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return 0;
            }
            return New_ObjProductRefDetails.PidfpbfreferenceProductdetailId;

        }
        private async Task<long> SaveGeneralRandDDetails(PBFFormEntity pbfentity)
        {
            var objPidfGeneralRnd = new PidfPbfGeneralRnd();
            long PidfPbfId = 0;
            try
            {
                var loggedInUserId = _helper.GetLoggedInUser().UserId;
                var pidfPbfDetails = _pbfRepository.GetAllQuery().Where(x => x.Pidfid == pbfentity.Pidfid).FirstOrDefault();
                PidfPbfId = pbfentity.Pidfpbfid == 0 ? pidfPbfDetails.Pidfpbfid : pbfentity.Pidfpbfid;

                var objPidfGeneralRndDetails = _repositoryPidfPbfGeneralRnd.GetAllQuery().
              Where(x => x.PidfId == pbfentity.Pidfid && x.PbfId == PidfPbfId && x.PbfRndDetailsId == pbfentity.PidfPbfGeneralRnd.PbfRndDetailsId).FirstOrDefault();
                if (objPidfGeneralRndDetails != null)
                {

                    objPidfGeneralRndDetails.PidfId = pbfentity.Pidfid;
                    objPidfGeneralRndDetails.PbfId = PidfPbfId;
                    objPidfGeneralRndDetails.BusinessUnitId = pbfentity.BusinessUnitId;
                    objPidfGeneralRndDetails.RndResponsiblePerson = pbfentity.PidfPbfGeneralRnd.RndResponsiblePerson;
                    objPidfGeneralRndDetails.TypeOfDevelopmentDate = pbfentity.PidfPbfGeneralRnd.TypeOfDevelopmentDate;
                    objPidfGeneralRndDetails.PivotalBatchesManufacturedCompleted = pbfentity.PidfPbfGeneralRnd.PivotalBatchesManufacturedCompleted;
                    objPidfGeneralRndDetails.StabilityResultsDayZero = pbfentity.PidfPbfGeneralRnd.StabilityResultsDayZero;
                    objPidfGeneralRndDetails.StabilityResultsThreeMonth = pbfentity.PidfPbfGeneralRnd.StabilityResultsThreeMonth;
                    objPidfGeneralRndDetails.StabilityResultsSixMonth = pbfentity.PidfPbfGeneralRnd.StabilityResultsSixMonth;
                    objPidfGeneralRndDetails.NonStandardProduct = pbfentity.PidfPbfGeneralRnd.NonStandardProduct;
                    objPidfGeneralRndDetails.Pivotals = pbfentity.PidfPbfGeneralRnd.Pivotals;
                    objPidfGeneralRndDetails.BatchSizes = pbfentity.PidfPbfGeneralRnd.BatchSizes;
                    objPidfGeneralRndDetails.NoMofBatchesPerStrength = pbfentity.PidfPbfGeneralRnd.NoMofBatchesPerStrength;
                    objPidfGeneralRndDetails.SiteTransferDate = pbfentity.PidfPbfGeneralRnd.SiteTransferDate;
                    objPidfGeneralRndDetails.ApiOrderedDate = pbfentity.PidfPbfGeneralRnd.ApiOrderedDate;
                    objPidfGeneralRndDetails.ApiReceivedDate = pbfentity.PidfPbfGeneralRnd.ApiReceivedDate;
                    objPidfGeneralRndDetails.FinalFormulationApproved = pbfentity.PidfPbfGeneralRnd.FinalFormulationApproved;
                    objPidfGeneralRndDetails.UpdatedOn = DateTime.Now;
                    objPidfGeneralRndDetails.CreatedBy = loggedInUserId;

                    _repositoryPidfPbfGeneralRnd.UpdateAsync(objPidfGeneralRndDetails);


                }
                else
                {
                    objPidfGeneralRnd.PidfId = pbfentity.Pidfid;
                    objPidfGeneralRnd.PbfId = PidfPbfId;
                    objPidfGeneralRnd.BusinessUnitId = pbfentity.BusinessUnitId;
                    objPidfGeneralRnd.RndResponsiblePerson = pbfentity.PidfPbfGeneralRnd.RndResponsiblePerson;
                    objPidfGeneralRnd.TypeOfDevelopmentDate = pbfentity.PidfPbfGeneralRnd.TypeOfDevelopmentDate;
                    objPidfGeneralRnd.PivotalBatchesManufacturedCompleted = pbfentity.PidfPbfGeneralRnd.PivotalBatchesManufacturedCompleted;
                    objPidfGeneralRnd.StabilityResultsDayZero = pbfentity.PidfPbfGeneralRnd.StabilityResultsDayZero;
                    objPidfGeneralRnd.StabilityResultsThreeMonth = pbfentity.PidfPbfGeneralRnd.StabilityResultsThreeMonth;
                    objPidfGeneralRnd.StabilityResultsSixMonth = pbfentity.PidfPbfGeneralRnd.StabilityResultsSixMonth;
                    objPidfGeneralRnd.NonStandardProduct = pbfentity.PidfPbfGeneralRnd.NonStandardProduct;
                    objPidfGeneralRnd.Pivotals = pbfentity.PidfPbfGeneralRnd.Pivotals;
                    objPidfGeneralRnd.BatchSizes = pbfentity.PidfPbfGeneralRnd.BatchSizes;
                    objPidfGeneralRnd.NoMofBatchesPerStrength = pbfentity.PidfPbfGeneralRnd.NoMofBatchesPerStrength;
                    objPidfGeneralRnd.SiteTransferDate = pbfentity.PidfPbfGeneralRnd.SiteTransferDate;
                    objPidfGeneralRnd.ApiOrderedDate = pbfentity.PidfPbfGeneralRnd.ApiOrderedDate;
                    objPidfGeneralRnd.ApiReceivedDate = pbfentity.PidfPbfGeneralRnd.ApiReceivedDate;
                    objPidfGeneralRnd.FinalFormulationApproved = pbfentity.PidfPbfGeneralRnd.FinalFormulationApproved;
                    objPidfGeneralRnd.CreatedOn = DateTime.Now;
                    objPidfGeneralRnd.CreatedBy = loggedInUserId;

                    _repositoryPidfPbfGeneralRnd.AddAsync(objPidfGeneralRnd);
                }


                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return 0;
            }
            // return objPidfGeneralRnd.PbfRndDetailsId;
            return PidfPbfId;

        }
        private async Task<long> SavePackSizeStability(List<PidfPbfRnDPackSizeStabilityEntity> pbfentities, long pbfgeneralid,long pidfid,int countryId)
        {
            try
            {
                var loggedInUserId = _helper.GetLoggedInUser().UserId;
             
                foreach (var pbfentity in pbfentities)
                {
                    var FinalpbfGeneralid = pbfentity.PbfgeneralId == 0 ? pbfgeneralid : pbfentity.PbfgeneralId;

                    foreach (var item in pbfentity.PackSizes)
                    {
                        var objPackSizeStabilityDetails = _repositoryPidfPbfRnDPackSizeStability.GetAllQuery().
               Where(x => x.Pidfid == pidfid && x.PbfgeneralId == FinalpbfGeneralid && x.PackSizeStabilityId==item.PackSizeStabilityId && x.CountryId==countryId).FirstOrDefault();

                        if (objPackSizeStabilityDetails != null && item.Value != null)
                        {
                           
                            objPackSizeStabilityDetails.Pidfid = pidfid;
                            objPackSizeStabilityDetails.PbfgeneralId = FinalpbfGeneralid;
                            objPackSizeStabilityDetails.StrengthId = pbfentity.StrengthId;
                            objPackSizeStabilityDetails.PackSizeId = item.PackSizeId;
                            objPackSizeStabilityDetails.Value = item.Value;
                            objPackSizeStabilityDetails.CreatedOn = DateTime.Now;
                            objPackSizeStabilityDetails.CreatedBy = loggedInUserId;
                            objPackSizeStabilityDetails.CountryId = countryId;                            objPackSizeStabilityDetails.CreatedOn = DateTime.Now;
                            objPackSizeStabilityDetails.CreatedBy = loggedInUserId;
                            _repositoryPidfPbfRnDPackSizeStability.UpdateAsync(objPackSizeStabilityDetails);
                        }
                        else if (item.Value != null)
                        {
                           
                            var newPackSizeStability = new PidfPbfRnDPackSizeStability
                            {
                                Pidfid = pidfid,
                                PbfgeneralId = FinalpbfGeneralid,
                                StrengthId = pbfentity.StrengthId,
                                CreatedOn = DateTime.Now,
                                CreatedBy = loggedInUserId,
                                PackSizeId = item.PackSizeId,
                                Value = item.Value,
                                CountryId=countryId
                            };

                            _repositoryPidfPbfRnDPackSizeStability.AddAsync(newPackSizeStability);
                        }
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                return 0;
            }
            return pbfentities.FirstOrDefault()?.Pidfid ?? 0;
        }


        private async Task<long> SaveTDT(PBFFormEntity pbfentity, long pbfgeneralid, IFormFileCollection files, string _webrootPath)
        {
            try
            {
                var loggedInUserId = _helper.GetLoggedInUser().UserId;
                var path = Path.Combine(_webrootPath, "Uploads\\PIDF\\TDP");
                string emcureuniqueFileName = null;
                string innovatoruniqueFileName = null;
                IFormFile emcureFile=null;
                IFormFile innovatorFile=null;

                if (files.Count > 0)
                {
                    emcureFile = files[0];
                    emcureuniqueFileName = Path.GetFileNameWithoutExtension(emcureFile.FileName)
                        + Guid.NewGuid().ToString().Substring(0, 4)
                        + Path.GetExtension(emcureFile.FileName);

                    if (files.Count > 1)
                    {
                        innovatorFile = files[1];
                        innovatoruniqueFileName = Path.GetFileNameWithoutExtension(innovatorFile.FileName)
                            + Guid.NewGuid().ToString().Substring(0, 4)
                            + Path.GetExtension(innovatorFile.FileName);
                    }
                }
                foreach (var item in pbfentity.PbfGeneralTdpEntity)
                {
                    foreach (var innovator in item.InnovatorData)
                    {
                        await SaveTDTSection(innovator, pbfgeneralid, loggedInUserId, pbfentity.Pidfid, pbfentity.Approch, false , innovator.PidfproductStrngthId, pbfentity.FormulaterResponsiblePerson, pbfentity.PbfGeneralTdpEntity[0].SecondaryPackaging, pbfentity.PbfGeneralTdpEntity[0].PrimaryPackaging, pbfentity.PbfGeneralTdpEntity[0].ShelfLife, pbfentity.PbfGeneralTdpEntity[0].StorageHandling,innovatorFile, innovatoruniqueFileName,path);
                    }

                    foreach (var emcure in item.EmcureData)
                    {
                        await SaveTDTSection(emcure, pbfgeneralid, loggedInUserId, pbfentity.Pidfid, pbfentity.Approch, true, emcure.PidfproductStrngthId, pbfentity.FormulaterResponsiblePerson, pbfentity.PbfGeneralTdpEntity[1].SecondaryPackaging, pbfentity.PbfGeneralTdpEntity[1].PrimaryPackaging, pbfentity.PbfGeneralTdpEntity[1].ShelfLife, pbfentity.PbfGeneralTdpEntity[1].StorageHandling, emcureFile, emcureuniqueFileName, path);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
            return pbfentity.Pidfpbfid;
        }

        private async Task SaveTDTSection(TdpSectionModel section, long pbfgeneralid, int loggedInUserId, long pidfid, string approch, bool isEmcure, long? PidfproductStrngthId,string FormulaterResponsiblePerson , string SecondaryPackaging, string PrimaryPackaging,  string ShelfLife, string StorageHandling,IFormFile file,string filename, string path)
        {
            var objPbfGeneralTdp =  _PbfGeneralTdp.GetAllQuery()
                .Where(x => x.TradeDressProposalId == section.TradeDressProposalId)
                .FirstOrDefault();

            if (objPbfGeneralTdp != null)
            {
                // Update existing record
                objPbfGeneralTdp.Pidfid = pidfid;
                objPbfGeneralTdp.Approch = approch;
                objPbfGeneralTdp.PbfId = pidfid;
                objPbfGeneralTdp.PidfpbfGeneralId = pbfgeneralid;
                objPbfGeneralTdp.PidfproductStrngthId = PidfproductStrngthId;
                objPbfGeneralTdp.FormulaterResponsiblePerson = FormulaterResponsiblePerson;
                objPbfGeneralTdp.Description = section.Description;
                objPbfGeneralTdp.Shape = section.Shape;
                objPbfGeneralTdp.Color = section.Color;
                objPbfGeneralTdp.Engraving = section.Engraving;
                if (file != null && file.Length > 0)
                {
                    objPbfGeneralTdp.Packaging = filename;
                    await FileUpload(file, path, filename);
                }
                else if (objPbfGeneralTdp.Packaging!=null && file==null)
                {
                    objPbfGeneralTdp.Packaging = objPbfGeneralTdp.Packaging;
                }
                else
                {
                    objPbfGeneralTdp.Packaging = null;
                }
                objPbfGeneralTdp.ShelfLife = ShelfLife;
                objPbfGeneralTdp.StorageHandling = StorageHandling;
                objPbfGeneralTdp.PrimaryPackaging = PrimaryPackaging;
                objPbfGeneralTdp.SecondryPackaging = SecondaryPackaging;
                objPbfGeneralTdp.IsEmcure = isEmcure;
                objPbfGeneralTdp.CreatedDate = DateTime.Now;
                objPbfGeneralTdp.CreatedBy = loggedInUserId;
                _PbfGeneralTdp.UpdateAsync(objPbfGeneralTdp);
            }
            else
            {
                // Create a new record
                PbfGeneralTdp objPbfGeneralTdpAdd = new();
                objPbfGeneralTdpAdd.Pidfid = pidfid;
                objPbfGeneralTdpAdd.Approch = approch;
                objPbfGeneralTdpAdd.PbfId = pidfid;
                objPbfGeneralTdpAdd.PidfpbfGeneralId = pbfgeneralid;
                objPbfGeneralTdpAdd.PidfproductStrngthId = PidfproductStrngthId;
                objPbfGeneralTdpAdd.FormulaterResponsiblePerson = FormulaterResponsiblePerson;
                objPbfGeneralTdpAdd.Description = section.Description;
                objPbfGeneralTdpAdd.Shape = section.Shape;
                objPbfGeneralTdpAdd.Color = section.Color;
                objPbfGeneralTdpAdd.Engraving = section.Engraving;
                if (file != null && file.Length > 0)
                {
                    objPbfGeneralTdpAdd.Packaging = filename;
                    await FileUpload(file, path, filename);
                }
                else
                {
                    objPbfGeneralTdpAdd.Packaging = null;
                }
                objPbfGeneralTdpAdd.ShelfLife = ShelfLife;
                objPbfGeneralTdpAdd.StorageHandling = StorageHandling;
                objPbfGeneralTdpAdd.PrimaryPackaging = PrimaryPackaging;
                objPbfGeneralTdpAdd.SecondryPackaging = SecondaryPackaging;
                objPbfGeneralTdpAdd.IsEmcure = isEmcure;
                objPbfGeneralTdpAdd.CreatedDate = DateTime.Now;
                objPbfGeneralTdpAdd.CreatedBy = loggedInUserId;
                _PbfGeneralTdp.AddAsync(objPbfGeneralTdpAdd);
            }
        }

        private async Task<PbfGeneralTdpEntity> GetTDT(long PIDFID,string APIurl)
        {
         
            var dbObj = await _PbfGeneralTdp.GetAllAsync(x => x.Pidfid == PIDFID);
            if (dbObj.Count() > 0)
            {
                string baseURL = APIurl + "/Uploads/PIDF/TDP";
                var innovatorfilename = dbObj.Where(x => x.IsEmcure == false).Select(x => x.Packaging).FirstOrDefault();
                var innovatorfullPath = baseURL + "/" + innovatorfilename;
                var innovatorData = dbObj
                    .Where(x => x.IsEmcure == false).OrderByDescending(x=>x.PidfproductStrngthId)
                    .Select(x => new TdpSectionModel
                    {

                        Description = x.Description,
                        Shape = x.Shape,
                        Color = x.Color,
                        Engraving = x.Engraving,
                        TradeDressProposalId = x.TradeDressProposalId,
                        PackagingInnovator = innovatorfilename == null ? null : innovatorfullPath,
                        IsEmcure = false,
                        PrimaryPackaging=x.PrimaryPackaging,
                        SecondaryPackaging=x.SecondryPackaging,
                        ShelfLife=x.ShelfLife,
                        StorageHandling=x.StorageHandling


                    })
                    .ToList();
                var emcurefilename = dbObj.Where(x => x.IsEmcure == true).Select(x => x.Packaging).FirstOrDefault();
                var emcurefullPath = baseURL + "/" + emcurefilename;
                var emcureData = dbObj
                    .Where(x => x.IsEmcure == true).OrderByDescending(x => x.PidfproductStrngthId)
                    .Select(x => new TdpSectionModel
                    {

                        Description = x.Description,
                        Shape = x.Shape,
                        Color = x.Color,
                        Engraving = x.Engraving,
                        TradeDressProposalId = x.TradeDressProposalId,
                        PackagingEmcure = emcurefilename == null ? null : emcurefullPath,
                        IsEmcure = true,
                        PrimaryPackaging = x.PrimaryPackaging,
                        SecondaryPackaging = x.SecondryPackaging,
                        ShelfLife = x.ShelfLife,
                        StorageHandling = x.StorageHandling
                    })
                    .ToList();

                var tdpEntity = new PbfGeneralTdpEntity
                {
                    PidfproductStrngthId = dbObj.FirstOrDefault().PidfproductStrngthId,
                    FormulaterResponsiblePerson = dbObj.FirstOrDefault().FormulaterResponsiblePerson,
                    Approch = dbObj.FirstOrDefault().Approch,
                    InnovatorData = innovatorData,
                    EmcureData = emcureData
                };

                return tdpEntity;
            }
            else
            {
                return new PbfGeneralTdpEntity();
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

                if (pidfpbfid > 0 && pbfentity.MarketMappingId != null && pbfentity.MarketMappingId.Length > 0)
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
                        objMM.BusinessUnitId = (int)item;
                        objMM.Pidfpbfid = pidfpbfid;
                        objMM.CreatedBy = loggedInUserId;
                        objMM.CreatedDate = DateTime.Now;
                        objmapping.Add(objMM);
                    }
                    _pidfPbfMarketMappingRepository.AddRange(objmapping);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion Marketting Mapping Add Update



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
                        objrndMaster.ApirequirementVendorName = pbfentity.RNDMasterEntities.ApirequirementVendorName;
                        objrndMaster.ManHourRate = pbfentity.RNDMasterEntities.ManHourRate;
                        objrndMaster.PlanSupportCostRsPerDay = pbfentity.RNDMasterEntities.PlanSupportCostRsPerDay;
                        objrndMaster.PlantId = pbfentity.RNDMasterEntities.PlantId_Tab;
                        objrndMaster.LineId = pbfentity.RNDMasterEntities.PBFLine;
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
                        objRndMaster.PlantId = pbfentity.RNDMasterEntities.PlantId_Tab;
                        objRndMaster.LineId = pbfentity.RNDMasterEntities.PBFLine;
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
                            batchsize.Salt = item.salt;
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

                #region Update PBF Reference Product Details
                await SaveUpdateReferenceProductDetails(pbfgeneralid, pbfentity);
                #endregion Update PBF Reference Product Details
                #region Update PBF genegral R&D Details

                await SaveGeneralRandDDetails(pbfentity);
                #endregion

                return pbfgeneralid;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return pbfgeneralid;
            }
        }

        public async Task<long> SavePidfAndPBFCommanDetailsnew1(long pidfid, PBFFormEntity pbfentity, IFormFileCollection files, string _webrootPath)
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

                if (pidfpbfid > 0 && pbfentity.MarketMappingId != null && pbfentity.MarketMappingId.Length > 0)
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
                        objMM.BusinessUnitId = (int)item;
                        objMM.Pidfpbfid = pidfpbfid;
                        objMM.CreatedBy = loggedInUserId;
                        objMM.CreatedDate = DateTime.Now;
                        objmapping.Add(objMM);
                    }
                    _pidfPbfMarketMappingRepository.AddRange(objmapping);
                    await _unitOfWork.SaveChangesAsync();
                }

                #endregion Marketting Mapping Add Update

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

                #region Update PBF Reference Product Details
                await SaveUpdateReferenceProductDetails(pbfgeneralid, pbfentity);
                #endregion Update PBF Reference Product Details
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
                    objMaster.PlantId = pbfentity.RNDMasterEntities.PlantId_Tab;
                    objMaster.LineId = pbfentity.RNDMasterEntities.PBFLine;
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

                #region Head Wise Budget Add Update

                List<PidfPbfHeadWiseBudget> objHeadWiseBudgetlist = new();

                //Save headwisebudget Entities
                if (pbfentity.RNDHeadWiseBudgets != null && pbfentity.RNDHeadWiseBudgets.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDHeadWiseBudgets)
                    {
                        PidfPbfHeadWiseBudget objheadwisebudget = new PidfPbfHeadWiseBudget();
                        objheadwisebudget = _mapperFactory.Get<RNDHeadWiseBudget, PidfPbfHeadWiseBudget>(item);
                        objheadwisebudget.PbfgeneralId = pbfgeneralid;
                        objheadwisebudget.CreatedDate = DateTime.Now;
                        objheadwisebudget.CreatedBy = loggedInUserId;
                        objHeadWiseBudgetlist.Add(objheadwisebudget);
                    }
                }

                #endregion Head Wise Budget Add Update

                #region Phase Wise Budget Add Update

                List<PidfPbfPhaseWiseBudget> objPhaseWiseBudgetlist = new();

                //Save headwisebudget Entities
                if (pbfentity.RNDPhaseWiseBudgets != null && pbfentity.RNDPhaseWiseBudgets.Count() > 0)
                {
                    foreach (var item in pbfentity.RNDPhaseWiseBudgets)
                    {
                        PidfPbfPhaseWiseBudget objphasewisebudget = new PidfPbfPhaseWiseBudget();
                        objphasewisebudget = _mapperFactory.Get<RNDPhaseWiseBudget, PidfPbfPhaseWiseBudget>(item);
                        objphasewisebudget.PbfgeneralId = pbfgeneralid;
                        objphasewisebudget.CreatedDate = DateTime.Now;
                        objphasewisebudget.CreatedBy = loggedInUserId;
                        objPhaseWiseBudgetlist.Add(objphasewisebudget);
                    }
                }

                #endregion Head Wise Budget Add Update

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
                    objPIDFGeneralupdate.PidfPbfHeadWiseBudgets = objHeadWiseBudgetlist;
                    objPIDFGeneralupdate.PidfPbfPhaseWiseBudgets = objPhaseWiseBudgetlist;
                    objPIDFGeneralupdate.BestudyResults = pbfentity.BestudyResults;
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
                    objPIDFGeneraladd.PidfPbfHeadWiseBudgets = objHeadWiseBudgetlist;
                    objPIDFGeneraladd.PidfPbfPhaseWiseBudgets = objPhaseWiseBudgetlist;
                    objPIDFGeneraladd.BestudyResults = pbfentity.BestudyResults;
                    _pidfPbfGeneralRepository.AddAsync(objPIDFGeneraladd);
                    await _unitOfWork.SaveChangesAsync();
                    pbfgeneralid = objPIDFGeneraladd.PbfgeneralId;
                }
                #endregion Section PBF General Add Update
                #region Update PBF genegral R&D Details

                var Pidfpbfid = await SaveGeneralRandDDetails(pbfentity);
                #endregion
                #region RA Add Update
                await AddUpdateRa(pbfentity.RaEntities, loggedInUserId, pbfentity.Pidfid, Pidfpbfid, pbfentity.BusinessUnitId);
                #endregion

                #region Update PBF genegral PackSizeStability Details
                if (pbfentity.PidfPbfRnDPackSizeStability != null)
                {
                    await SavePackSizeStability(pbfentity.PidfPbfRnDPackSizeStability, pbfentity.PBFGeneralId, pbfentity.Pidfid, pbfentity.selectedCountry);
                }
                #endregion
                #region TDT
                await SaveTDT(pbfentity, pbfgeneralid,files,_webrootPath);
                #endregion
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
                }
                var analytical = _pidfPbfAnalyticalRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (analytical.Count > 0)
                {
                    foreach (var item in analytical)
                    {
                        _pidfPbfAnalyticalRepository.Remove(item);
                    }
                }
                var analyticalCost = _PidfPbfAnalyticalAmvcostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (analyticalCost.Count > 0)
                {
                    foreach (var item in analyticalCost)
                    {
                        var amvstrengthmapping = _pidfPbfAnalyticalAmvcostStrengthMappingRepository.GetAllQuery().Where(x => x.TotalAmvcostId == item.TotalAmvcostId).ToList();
                        if (amvstrengthmapping.Count > 0)
                        {
                            foreach (var it in amvstrengthmapping)
                            {
                                _pidfPbfAnalyticalAmvcostStrengthMappingRepository.Remove(it);
                            }
                        }
                        _PidfPbfAnalyticalAmvcostRepository.Remove(item);
                    }
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
                }
                var apirequirement = _pidfPbfRndApirequirementRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (apirequirement.Count > 0)
                {
                    foreach (var item in apirequirement)
                    {
                        _pidfPbfRndApirequirementRepository.Remove(item);
                    }
                }
                var exicipient = _pidfPbfRnDExicipientRequirementRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (exicipient.Count > 0)
                {
                    foreach (var item in exicipient)
                    {
                        _pidfPbfRnDExicipientRequirementRepository.Remove(item);
                    }
                }
                var packging = _pidfPbfRnDPackagingMaterialRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (packging.Count > 0)
                {
                    foreach (var item in packging)
                    {
                        _pidfPbfRnDPackagingMaterialRepository.Remove(item);
                    }
                }
                var toolongchangepart = _pidfPbfRndToolingChangePartCostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (toolongchangepart.Count > 0)
                {
                    foreach (var item in toolongchangepart)
                    {
                        _pidfPbfRndToolingChangePartCostRepository.Remove(item);
                    }
                }
                var capexandmiscellaneous = _pidfPbfRndCapexMiscellaneousExpenseRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (capexandmiscellaneous.Count > 0)
                {
                    foreach (var item in capexandmiscellaneous)
                    {
                        _pidfPbfRndCapexMiscellaneousExpenseRepository.Remove(item);
                    }
                }
                var plantsupportcost = _pidfPbfRndPlantSupportCostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (plantsupportcost.Count > 0)
                {
                    foreach (var item in plantsupportcost)
                    {
                        _pidfPbfRndPlantSupportCostRepository.Remove(item);
                    }
                }
                var referenceporduct = _pidfPbfRndReferenceProductDetailRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (referenceporduct.Count > 0)
                {
                    foreach (var item in referenceporduct)
                    {
                        _pidfPbfRndReferenceProductDetailRepository.Remove(item);
                    }
                }
                var fillingexpenses = _pidfPbfRndFillingExpenseRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (fillingexpenses.Count > 0)
                {
                    foreach (var item in fillingexpenses)
                    {
                        _pidfPbfRndFillingExpenseRepository.Remove(item);
                    }
                }
                var manpowercost = _pidfPbfRndPidfPbfRnDManPowerCostRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (manpowercost.Count > 0)
                {
                    foreach (var item in manpowercost)
                    {
                        _pidfPbfRndPidfPbfRnDManPowerCostRepository.Remove(item);
                    }
                }
                var headwisebudget = _pidfPbfHeadWiseBudgetRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (headwisebudget.Count > 0)
                {
                    foreach (var item in headwisebudget)
                    {
                        _pidfPbfHeadWiseBudgetRepository.Remove(item);
                    }
                }
                var phasewisebudget = _pidfPbfPhaseWiseBudgetRepository.GetAllQuery().Where(x => x.PbfgeneralId == pbfgeneralid).ToList();
                if (phasewisebudget.Count > 0)
                {
                    foreach (var item in phasewisebudget)
                    {
                        _pidfPbfPhaseWiseBudgetRepository.Remove(item);
                    }
                }

            }
            catch (Exception ex)
            {
                _ExceptionService.LogException(ex);
            }
        }
        public async Task<bool> AddUpdateRa(List<PidfPbfRaEntity> ls, int CreatedBy, long pidfId, long pbfid = 0, int BusinessUnitId = 0)
        {
            try
            {
                // Remove Deleted RA Table Row from Database
                var dbObj = await _pidfPbfRRepositiry.GetAllAsync(x => x.Pidfid == pidfId && x.BuId == BusinessUnitId);
                foreach (var item in dbObj)
                {
                    bool IsExist = ls.Any(x => x.Pidfpbfraid == item.Pidfpbfraid);
                    if (!IsExist)
                    {
                        _pidfPbfRRepositiry.Remove(item);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }


                SqlConnection con = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
                int count = 0;
                var recordsTable = new DataTable();
                recordsTable.Columns.Add("PIDFPBFRAId", typeof(double));
                recordsTable.Columns.Add("PIDFId", typeof(double));
                recordsTable.Columns.Add("PBFId", typeof(double));
                recordsTable.Columns.Add("CountryIdBuId", typeof(int));
                recordsTable.Columns.Add("PivotalBatchManufactured", typeof(DateTime));
                recordsTable.Columns.Add("LastDataFromRnD", typeof(DateTime));
                recordsTable.Columns.Add("BEFinalReport", typeof(DateTime));
                recordsTable.Columns.Add("BuId", typeof(int));
                recordsTable.Columns.Add("TypeOfSubmissionId", typeof(int));
                recordsTable.Columns.Add("DossierReadyDate", typeof(DateTime));
                recordsTable.Columns.Add("EarliestSubmissionDExcl", typeof(DateTime));
                recordsTable.Columns.Add("EarliestLaunchDExcl", typeof(DateTime));
                recordsTable.Columns.Add("LasDateToRegulatory", typeof(DateTime));
                recordsTable.Columns.Add("CreatedOn", typeof(DateTime));
                recordsTable.Columns.Add("UpdatedOn", typeof(DateTime));
                recordsTable.Columns.Add("DeletedOn", typeof(DateTime));
                recordsTable.Columns.Add("CreatedBy", typeof(int));
                recordsTable.Columns.Add("EndOfProcedureDate", typeof(DateTime));
                recordsTable.Columns.Add("CountryApprovalDate", typeof(DateTime));
                DataRow row;
                foreach (var item in ls)
                {

                    if (pidfId > 0)
                    {
                        row = recordsTable.NewRow();
                        row["PIDFPBFRAId"] = item.Pidfpbfraid;
                        row["PIDFId"] = pidfId;
                        row["PBFId"] = pbfid;
                        row["CountryIdBuId"] = item.CountryIdBuId == 0 ? DBNull.Value : item.CountryIdBuId;
                        row["PivotalBatchManufactured"] = item.PivotalBatchManufactured == null ? DBNull.Value : item.PivotalBatchManufactured;
                        row["LastDataFromRnD"] = item.LastDataFromRnD == null ? DBNull.Value : item.LastDataFromRnD;
                        row["BEFinalReport"] = item.BefinalReport == null ? DBNull.Value : item.BefinalReport;
                        row["BuId"] = BusinessUnitId; //item.BuId == 0 ? 0 : item.BuId;
                        row["TypeOfSubmissionId"] = item.TypeOfSubmissionId == null || item.TypeOfSubmissionId == 0 ? DBNull.Value : item.TypeOfSubmissionId;
                        row["DossierReadyDate"] = item.DossierReadyDate == null ? DBNull.Value : item.DossierReadyDate;
                        row["EarliestSubmissionDExcl"] = item.EarliestSubmissionDexcl == null ? DBNull.Value : item.EarliestSubmissionDexcl;
                        row["EarliestLaunchDExcl"] = item.EarliestLaunchDexcl == null ? DBNull.Value : item.EarliestLaunchDexcl;
                        row["LasDateToRegulatory"] = item.LasDateToRegulatory == null ? DBNull.Value : item.LasDateToRegulatory;
                        row["CreatedOn"] = item.Pidfpbfraid > 0 ? DBNull.Value : DateTime.Now;
                        row["UpdatedOn"] = item.Pidfpbfraid > 0 ? DateTime.Now : DBNull.Value;
                        row["DeletedOn"] = item.Pidfpbfraid > 0 ? DateTime.Now : DBNull.Value;
                        row["CreatedBy"] = CreatedBy;
                        row["EndOfProcedureDate"] = item.EndOfProcedureDate == null ? DBNull.Value : item.EndOfProcedureDate;
                        row["CountryApprovalDate"] = item.CountryApprovalDate == null ? DBNull.Value : item.CountryApprovalDate;
                        recordsTable.Rows.Add(row);
                    }
                }
                if (recordsTable.Rows.Count > 0)
                {
                    recordsTable.EndLoadData();
                    var data = new DynamicParameters(new
                    {
                        table = recordsTable.AsTableValuedParameter("Type_PIDF_PBF_RA")
                    });
                    data.Add("@Success", "", direction: ParameterDirection.Output);
                    count = await con.ExecuteAsync("AddUpdatePIDF_Pbf_ra", data, commandType: CommandType.StoredProcedure);
                    if (count > 0 && data.Get<string>("Success").Trim() == "success")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return false;
            }
        }
        public async Task<dynamic> GetPBFRADates(RaCalculatedDates calculatedDates)
        {
            var loggedInUserId = _helper.GetLoggedInUser().UserId;
            // var data = new RaCalculatedDates();
            try
            {
                SqlParameter[] osqlParameter = {
                      new SqlParameter("@PIDFId", calculatedDates.PIDFId),
                      new SqlParameter("@BusinessUnitId", calculatedDates.BusinessUnitId),
                      new SqlParameter("@UserId", loggedInUserId),
                      new SqlParameter("@CountryId", calculatedDates.CountryId),
                      new SqlParameter("@TypeOfSubmissionId", calculatedDates.TypeOfSubmissionId),
                      new SqlParameter("@DossierReadyDate", calculatedDates.DossierReadyDate),
                      new SqlParameter("@PivotalBatchManufactured", calculatedDates.PivotalBatchManufactured),
                      new SqlParameter("@LastDataFromRnD", calculatedDates.LastDataFromRnD),
                      new SqlParameter("@BEFinalReport", calculatedDates.BEFinalReport),
                      new SqlParameter("@SMStabilityResultsSixMonth",calculatedDates.StabilityResultsSixMonth)
                   };

                var dbresult = await _pbfRepository.GetDataSetBySP("stp_npd_GetPBFRADates", System.Data.CommandType.StoredProcedure, osqlParameter);

                return dbresult;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return null;
            }

        }
        public async Task<dynamic> FileUpload(IFormFile files, string path, string uniqueFileName)
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
            return 1;
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
        #endregion Private Methods
    }
}