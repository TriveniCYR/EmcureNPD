using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Http;
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
    public class PIDFService : IPIDFService
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
        private readonly IMasterAuditLogService _auditLogService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHelper _helper;

        private IRepository<Pidf> _repository { get; set; }
        private IRepository<Pidfapidetail> _pidfApiRepository { get; set; }
        private IRepository<PidfproductStrength> _pidfProductStrength { get; set; }
        private IRepository<MasterUser> _masteUser { get; set; }
        private IRepository<MasterUser> _masteCountry { get; set; }
        //Market Extension & In House

        public PIDFService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IMasterOralService oralService, IMasterUnitofMeasurementService unitofMeasurementService, IMasterDosageFormService dosageFormService, IMasterPackagingTypeService packagingTypeService, IMasterBusinessUnitService businessUnitService, IMasterCountryService countryService, IMasterAPISourcingService masterAPISourcingService, IPidfApiDetailsService pidfApiDetailsService, IPidfProductStrengthService pidfProductStrengthService, IMasterDIAService masterDium, IMasterMarketExtensionService masterMarketExtensionService, IMasterAuditLogService auditLogService, IHttpContextAccessor httpContextAccessor, IHelper helper)
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
            _masteUser = unitOfWork.GetRepository<MasterUser>();
            _masteCountry = unitOfWork.GetRepository<MasterUser>();
            _masterDIAService = masterDium;
            _masterMarketExtensionService = masterMarketExtensionService;
            _auditLogService = auditLogService;
            _httpContextAccessor = httpContextAccessor;
            _helper = helper;
        }

        public async Task<dynamic> FillDropdown(int userid)
        {
            dynamic DropdownObjects = new ExpandoObject();

            var loggedInUserId = _helper.GetLoggedInUser().UserId;

            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", loggedInUserId)
            };

            DataSet dsDropdownOptions = await _repository.GetDataSetBySP("SP_Fill_ddl_PIDF", System.Data.CommandType.StoredProcedure, osqlParameter);

            DropdownObjects.MasterOrals = dsDropdownOptions.Tables[0];
            DropdownObjects.MasterUnitofMeasurements = dsDropdownOptions.Tables[1];
            DropdownObjects.MasterDosageForms = dsDropdownOptions.Tables[2];
            DropdownObjects.MarketExtensions = dsDropdownOptions.Tables[3];
            DropdownObjects.MasterPackagingTypes = dsDropdownOptions.Tables[4];
            DropdownObjects.MasterBusinessUnits = dsDropdownOptions.Tables[5];
            DropdownObjects.MasterAPISourcing = dsDropdownOptions.Tables[6];
            DropdownObjects.MasterDIAs = dsDropdownOptions.Tables[7];

            //DropdownObjects.MasterCountrys = GetCountryByUserId(userid).Result;            
            DropdownObjects.InHouses = new List<InHouseEntity> { new InHouseEntity { InHouseId = 1, InHouseName = "Yes" }, new InHouseEntity { InHouseId = 2, InHouseName = "No" } };
            return DropdownObjects;
        }

        public async Task<List<MasterBusinessUnitEntity>> GetBusinessUNitByUserId(int userid)
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", userid)
            };

            var dbresult = await _masteUser.GetDataSetBySP("stp_npd_GetBusinessUnitByUserId", System.Data.CommandType.StoredProcedure, osqlParameter);

            dynamic _BUObjects = new ExpandoObject();
            if (dbresult != null)
            {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                {
                    _BUObjects = dbresult.Tables[0].DataTableToList<MasterBusinessUnitEntity>();
                }
            }
            return _BUObjects;
        }

        public async Task<List<MasterCountryEntity>> GetCountryByUserId(int userid)
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", userid)
            };

            var dbresult = await _masteCountry.GetDataSetBySP("stp_npd_GetCountryByUserId", System.Data.CommandType.StoredProcedure, osqlParameter);

            dynamic _CNObjects = new ExpandoObject();
            if (dbresult != null)
            {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                {
                    _CNObjects = dbresult.Tables[0].DataTableToList<MasterCountryEntity>();
                }
            }
            return _CNObjects;
        }

        public async Task<DataTableResponseModel> GetAllPIDFList(DataTableAjaxPostModel model)
        {
            var loggedInUserId = _helper.GetLoggedInUser().UserId;

            string ColumnName = (model.order.Count > 0 ? model.columns[model.order[0].column].data : string.Empty);
            string SortDir = (model.order.Count > 0 ? model.order[0].dir : string.Empty);

            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", loggedInUserId),
                new SqlParameter("@CurrentPageNumber", model.start),
                    new SqlParameter("@PageSize", model.length),
                    new SqlParameter("@SortColumn", ColumnName),
                    new SqlParameter("@SortDirection", SortDir),
                    new SqlParameter("@SearchText", model.search.value)
            };

            var PIDFList = await _repository.GetBySP("stp_npd_GetPIDFList", System.Data.CommandType.StoredProcedure, osqlParameter);

            var TotalRecord = (PIDFList != null && PIDFList.Rows.Count > 0 ? Convert.ToInt32(PIDFList.Rows[0]["TotalRecord"]) : 0);
            var TotalCount = (PIDFList != null && PIDFList.Rows.Count > 0 ? Convert.ToInt32(PIDFList.Rows[0]["TotalCount"]) : 0);

            PIDFList.Columns.Add("encpidfid", typeof(String));
            PIDFList.Columns.Add("encbud", typeof(String));

            for (int i = 0; i < PIDFList.Rows.Count; i++)
            {
                PIDFList.Rows[i]["encpidfid"] = UtilityHelper.Encrypt(Convert.ToString(PIDFList.Rows[i]["PIDFID"]));
                PIDFList.Rows[i]["encbud"] = UtilityHelper.Encrypt(Convert.ToString(PIDFList.Rows[i]["BusinessUnitId"]));
            }

            DataTableResponseModel oDataTableResponseModel = new DataTableResponseModel(model.draw, TotalRecord, TotalCount, PIDFList);

            return oDataTableResponseModel;
        }

        public async Task<DataTableResponseModel> GetCommonPIDFList(DataTableAjaxPostModel model, string ScreenName)
        {
            string ColumnName = (model.order.Count > 0 ? model.columns[model.order[0].column].data : string.Empty);
            string SortDir = (model.order.Count > 0 ? model.order[0].dir : string.Empty);

            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFID", 0),
                new SqlParameter("@CurrentPageNumber", model.start),
                    new SqlParameter("@PageSize", model.length),
                    new SqlParameter("@SortColumn", ColumnName),
                    new SqlParameter("@SortDirection", SortDir),
                    new SqlParameter("@SearchText", model.search.value)
            };

            var PIDFList = await _repository.GetBySP("stp_npd_Get" + ScreenName + "List", System.Data.CommandType.StoredProcedure, osqlParameter);
            var TotalRecord = (PIDFList != null && PIDFList.Rows.Count > 0 ? Convert.ToInt32(PIDFList.Rows[0]["TotalRecord"]) : 0);
            var TotalCount = (PIDFList != null && PIDFList.Rows.Count > 0 ? Convert.ToInt32(PIDFList.Rows[0]["TotalCount"]) : 0);

            List<IPDPIDFListEntity> objList = PIDFList.DataTableToList<IPDPIDFListEntity>();
            for (int i = 0; i < objList.Count(); i++)
            {
                objList[i].encpidfid = UtilityHelper.Encrypt(Convert.ToString(objList[i].PIDFID));
                objList[i].encbud = UtilityHelper.Encrypt(Convert.ToString(objList[i].BusinessUnitId));
            }
            DataTableResponseModel oDataTableResponseModel = new DataTableResponseModel(model.draw, TotalRecord, TotalCount, objList);
            return oDataTableResponseModel;
        }

        public async Task<DBOperation> AddUpdatePIDF(PIDFEntity entityPIDF)

        {
            Pidf objPIDF;
            try
            {
                var loggedInUserId = _helper.GetLoggedInUser().UserId;

                if (entityPIDF.PIDFID > 0)
                {
                    objPIDF = await _repository.GetAsync(entityPIDF.PIDFID);
                    if (objPIDF != null)
                    {
                        PIDFEntity _previousPIDFEntity = _mapperFactory.Get<Pidf, PIDFEntity>(objPIDF);

                        objPIDF = _mapperFactory.Get<PIDFEntity, Pidf>(entityPIDF);
                        _repository.UpdateAsync(objPIDF);

                        await _unitOfWork.SaveChangesAsync();

                        var apiDetailsList = _pidfApiRepository.GetAllQuery().Where(x => x.Pidfid == entityPIDF.PIDFID);
                        _pidfApiRepository.RemoveRange(apiDetailsList);

                        await _unitOfWork.SaveChangesAsync();

                        var productStrengthList = _pidfProductStrength.GetAllQuery().Where(x => x.Pidfid == entityPIDF.PIDFID);
                        _pidfProductStrength.RemoveRange(productStrengthList);

                        await _unitOfWork.SaveChangesAsync();

                        await SaveChildDetails(objPIDF.Pidfid, loggedInUserId, entityPIDF.pidfApiDetailEntities, entityPIDF.pidfProductStregthEntities);

                        var isSuccess = await _auditLogService.CreateAuditLog<PIDFEntity>(entityPIDF.PIDFID > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
                           Utility.Enums.ModuleEnum.PIDF, _previousPIDFEntity, entityPIDF, Convert.ToInt32(objPIDF.Pidfid));

                        return DBOperation.Success;
                    }
                    else
                    {
                        return DBOperation.NotFound;
                    }
                }
                else
                {
                    var _LastPIDFId = _repository.GetAllQuery().OrderByDescending(x => x.Pidfid).Select(x => x.Pidfid).FirstOrDefault();
                    objPIDF = _mapperFactory.Get<PIDFEntity, Pidf>(entityPIDF);
                    objPIDF.Pidfno = "PIDF-00" + (_LastPIDFId + 1);
                    objPIDF.IsActive = true;
                    objPIDF.CreatedBy = loggedInUserId;
                    objPIDF.CreatedDate = DateTime.Now;
                    objPIDF.ModifyDate = DateTime.Now;
                    objPIDF.ModifyBy = loggedInUserId;
                    objPIDF.StatusUpdatedBy = loggedInUserId;
                    objPIDF.StatusUpdatedDate = DateTime.Now;

                    _repository.AddAsync(objPIDF);

                    await _unitOfWork.SaveChangesAsync();

                    await SaveChildDetails(objPIDF.Pidfid, loggedInUserId, entityPIDF.pidfApiDetailEntities, entityPIDF.pidfProductStregthEntities);

                    return DBOperation.Success;
                }
            }
            catch (Exception ex)
            {
                return DBOperation.Error;
            }
        }

        private async Task<bool> SaveChildDetails(long Pidfid, int loggedInUserId, List<PidfApiDetailEntity> pidfApiDetailEntities, List<PidfProductStregthEntity> pidfProductStregthEntities)
        {
            try
            {
                List<Pidfapidetail> _APIDetailList = new List<Pidfapidetail>();
                if (pidfApiDetailEntities != null && pidfApiDetailEntities.Count() > 0)
                {
                    foreach (var item in pidfApiDetailEntities)
                    {
                        Pidfapidetail pidfapidetail = new Pidfapidetail();
                        item.Pidfid = Pidfid;
                        item.ModifyDate = DateTime.Now;
                        item.ModifyBy = loggedInUserId;
                        pidfapidetail = _mapperFactory.Get<PidfApiDetailEntity, Pidfapidetail>(item);
                        _APIDetailList.Add(pidfapidetail);
                    }
                    _pidfApiRepository.AddRangeAsync(_APIDetailList);
                    await _unitOfWork.SaveChangesAsync();
                }

                if (pidfProductStregthEntities != null && pidfProductStregthEntities.Count() > 0)
                {
                    List<PidfproductStrength> _ProductStrengthList = new List<PidfproductStrength>();
                    foreach (var item in pidfProductStregthEntities)
                    {
                        PidfproductStrength pidfProductStrength = new PidfproductStrength();
                        item.Pidfid = Pidfid;
                        item.ModifyDate = DateTime.Now;
                        item.ModifyBy = loggedInUserId;
                        pidfProductStrength = _mapperFactory.Get<PidfProductStregthEntity, PidfproductStrength>(item);
                        _ProductStrengthList.Add(pidfProductStrength);
                    }
                    _pidfProductStrength.AddRangeAsync(_ProductStrengthList);
                    await _unitOfWork.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PIDFEntity> GetById(int id)  
        {
            var ids = Convert.ToInt64(id);
            var data = _mapperFactory.Get<Pidf, PIDFEntity>(await _repository.GetAsync(ids));
            data.pidfApiDetailEntities = _mapperFactory.GetList<Pidfapidetail, PidfApiDetailEntity>(_pidfApiRepository.GetAll().Where(x => x.Pidfid == ids).ToList());
            data.pidfProductStregthEntities = _mapperFactory.GetList<PidfproductStrength, PidfProductStregthEntity>(_pidfProductStrength.GetAll().Where(x => x.Pidfid == ids).ToList());
            return data;
        }

        public async Task<List<PIDFEntity>> GetAll()
        {
            return _mapperFactory.GetList<Pidf, PIDFEntity>(await _repository.GetAllAsync());
        }

        public async Task<DBOperation> ApproveRejectDeletePidf(EntryApproveRej oApprRej)
        {
            if (oApprRej != null && oApprRej.PidfIds.Count > 0)
            {
                int saveTId = 0;
                if (oApprRej.SaveType == "R")
                {
                    if (string.IsNullOrEmpty(oApprRej.ScreenId))
                        saveTId = (Int32)Master_PIDFStatus.PIDFRejected;
                    else if (oApprRej.ScreenId == Convert.ToString((Int32)PIDFScreen.PIDF))
                        saveTId = (Int32)Master_PIDFStatus.PIDFRejected;
                    else if (oApprRej.ScreenId == Convert.ToString((Int32)PIDFScreen.IPD))
                        saveTId = (Int32)Master_PIDFStatus.IPDRejected;
                    else if (oApprRej.ScreenId == Convert.ToString((Int32)PIDFScreen.Finance))
                        saveTId = (Int32)Master_PIDFStatus.FinanceRejected;
                    else if (oApprRej.ScreenId == Convert.ToString((Int32)PIDFScreen.Management))
                        saveTId = (Int32)Master_PIDFStatus.ManagementRejected;

                    saveTId = (Int32)Master_PIDFStatus.PIDFRejected;
                }
                if (oApprRej.SaveType == "A")
                {
                    if (string.IsNullOrEmpty(oApprRej.ScreenId))
                        saveTId = (Int32)Master_PIDFStatus.PIDFApproved;
                    else if (oApprRej.ScreenId == Convert.ToString((Int32)PIDFScreen.PIDF))
                        saveTId = (Int32)Master_PIDFStatus.PIDFApproved;
                    else if (oApprRej.ScreenId == Convert.ToString((Int32)PIDFScreen.IPD))
                        saveTId = (Int32)Master_PIDFStatus.IPDApproved;
                    else if (oApprRej.ScreenId == Convert.ToString((Int32)PIDFScreen.Finance))
                        saveTId = (Int32)Master_PIDFStatus.FinanceApproved;
                    else if (oApprRej.ScreenId == Convert.ToString((Int32)PIDFScreen.Management))
                        saveTId = (Int32)Master_PIDFStatus.ManagementApproved;

                    saveTId = (Int32)Master_PIDFStatus.PIDFApproved;
                }

                for (int i = 0; i < oApprRej.PidfIds.Count; i++)
                {
                    Pidf objPidf = await _repository.GetAsync(oApprRej.PidfIds[i].pidfId);
                    //if (oApprRej.SaveType == "D")
                    //{
                    //    objPidf.IsActive = false;
                    //}
                    //else
                    //{
                    objPidf.LastStatusId = objPidf.StatusId;
                    objPidf.StatusId = saveTId;
                    //}

                    objPidf.StatusUpdatedBy = _helper.GetLoggedInUser().UserId;
                    objPidf.StatusUpdatedDate = DateTime.Now;

                    _repository.UpdateAsync(objPidf);

                    await _unitOfWork.SaveChangesAsync();
                }
                //var isSuccess = await _auditLogService.CreateAuditLog<EntryApproveRej>(oApprRej.SaveType == "D" ? Utility.Audit.AuditActionType.Delete : Utility.Audit.AuditActionType.Update,
                //   Utility.Enums.ModuleEnum.PIDF, oApprRej, oApprRej, 0);

                return DBOperation.Success;
            }
            else
            {
                return DBOperation.NotFound;
            }
        }

        //This common Function for All PIDF List Screens for ButtonClick of  Approve/Reject/Delete
        public async Task<DBOperation> CommonApproveRejectDeletePidf(EntryApproveRej oApprRej, string ScreenName)
        {
            if (oApprRej != null && oApprRej.PidfIds.Count > 0)
            {
                int saveTId = 0;
                if (oApprRej.SaveType == "R")
                    saveTId = 4;
                if (oApprRej.SaveType == "A")
                    saveTId = 3;

                for (int i = 0; i < oApprRej.PidfIds.Count; i++)
                {
                    Pidf objPidf = await _repository.GetAsync(oApprRej.PidfIds[i].pidfId);
                    if (oApprRej.SaveType == "D")
                    {
                        objPidf.IsActive = false;
                    }
                    else
                    {
                        objPidf.LastStatusId = objPidf.StatusId;
                        objPidf.StatusId = saveTId;
                    }
                    _repository.UpdateAsync(objPidf);

                    await _unitOfWork.SaveChangesAsync();
                }
                var isSuccess = await _auditLogService.CreateAuditLog<EntryApproveRej>(oApprRej.SaveType == "D" ? Utility.Audit.AuditActionType.Delete : Utility.Audit.AuditActionType.Update,
                   Utility.Enums.ModuleEnum.PIDF, oApprRej, oApprRej, 0);

                return DBOperation.Success;
            }
            else
            {
                return DBOperation.NotFound;
            }
        }
    }
}