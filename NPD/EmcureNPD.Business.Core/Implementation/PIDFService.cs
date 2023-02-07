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

        private IRepository<Pidf> _repository { get; set; }
        private IRepository<Pidfapidetail> _pidfApiRepository { get; set; }
        private IRepository<PidfproductStrength> _pidfProductStrength { get; set; }
        private IRepository<MasterUser> _masteUser { get; set; }
        private IRepository<MasterUser> _masteCountry { get; set; }
        //Market Extension & In House

        public PIDFService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IMasterOralService oralService, IMasterUnitofMeasurementService unitofMeasurementService, IMasterDosageFormService dosageFormService, IMasterPackagingTypeService packagingTypeService, IMasterBusinessUnitService businessUnitService, IMasterCountryService countryService, IMasterAPISourcingService masterAPISourcingService, IPidfApiDetailsService pidfApiDetailsService, IPidfProductStrengthService pidfProductStrengthService, IMasterDIAService masterDium, IMasterMarketExtensionService masterMarketExtensionService, IMasterAuditLogService auditLogService)
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
        }

        public async Task<dynamic> FillDropdown(int userid)
        {
            dynamic DropdownObjects = new ExpandoObject();

            DropdownObjects.MasterOrals = _oralService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterUnitofMeasurements = _unitofMeasurementService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterDosageForms = _dosageFormService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterPackagingTypes = _packagingTypeService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterBusinessUnits = GetBusinessUNitByUserId(userid).Result;
            DropdownObjects.MasterCountrys = GetCountryByUserId(userid).Result;
            DropdownObjects.MarketExtensions = _masterMarketExtensionService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.InHouses = new List<InHouseEntity> { new InHouseEntity { InHouseId = 1, InHouseName = "Yes" }, new InHouseEntity { InHouseId = 2, InHouseName = "No" } };
            DropdownObjects.MasterAPISourcing = _APISourcingService.GetAll().Result.Where(xx => xx.IsActive).ToList();
            DropdownObjects.MasterDIAs = _masterDIAService.GetAll().Result.Where(xx => xx.IsActive).ToList();

           
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

            var PIDFList = await _repository.GetBySP("stp_npd_GetPIDFList", System.Data.CommandType.StoredProcedure, osqlParameter);

            var TotalRecord = (PIDFList != null && PIDFList.Rows.Count > 0 ? Convert.ToInt32(PIDFList.Rows[0]["TotalRecord"]) : 0);
            var TotalCount = (PIDFList != null && PIDFList.Rows.Count > 0 ? Convert.ToInt32(PIDFList.Rows[0]["TotalCount"]) : 0);

            DataTableResponseModel oDataTableResponseModel = new DataTableResponseModel(model.draw, TotalRecord, TotalCount, PIDFList.DataTableToList<PIDFListEntity>());

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
            PIDFEntity oldPIDFEntity;
            if (entityPIDF.PIDFID > 0)
            {
                objPIDF = await _repository.GetAsync(entityPIDF.PIDFID);
                if (objPIDF != null)
                {
                    objPIDF.IsActive = true;
                    objPIDF.CreatedBy = 14;
                    oldPIDFEntity = _mapperFactory.Get<Pidf, PIDFEntity>(objPIDF);
                    objPIDF = _mapperFactory.Get<PIDFEntity, Pidf>(entityPIDF);
                    _repository.UpdateAsync(objPIDF);

                    await _unitOfWork.SaveChangesAsync();

                    if (entityPIDF.pidfApiDetailEntities != null && entityPIDF.pidfApiDetailEntities.Count() > 0)
                    {
                        var apiDetailsList = _pidfApiRepository.GetAllQuery().Where(x => x.Pidfid == entityPIDF.PIDFID);
                        foreach (var item in apiDetailsList)
                        {
                            _pidfApiRepository.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();

                        foreach (var apiDetails in entityPIDF.pidfApiDetailEntities)
                        {
                            Pidfapidetail pidfapidetail;
                            apiDetails.Pidfid = entityPIDF.PIDFID;
                            apiDetails.ModifyDate = DateTime.Now;
                            apiDetails.ModifyBy = 1;
                            pidfapidetail = _mapperFactory.Get<PidfApiDetailEntity, Pidfapidetail>(apiDetails);
                            _pidfApiRepository.AddAsync(pidfapidetail);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                    if (entityPIDF.pidfProductStregthEntities != null && entityPIDF.pidfProductStregthEntities.Count() > 0)
                    {
                        var productStrengthList = _pidfProductStrength.GetAllQuery().Where(x => x.Pidfid == entityPIDF.PIDFID);
                        foreach (var item in productStrengthList)
                        {
                            _pidfProductStrength.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();

                        foreach (var productStrength in entityPIDF.pidfProductStregthEntities)
                        {
                            PidfproductStrength pidfProductStrength;
                            productStrength.Pidfid = entityPIDF.PIDFID;
                            productStrength.ModifyDate = DateTime.Now;
                            productStrength.ModifyBy = 1;
                            pidfProductStrength = _mapperFactory.Get<PidfProductStregthEntity, PidfproductStrength>(productStrength);
                            _pidfProductStrength.AddAsync(pidfProductStrength);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }

                    if (objPIDF.Pidfid == 0)
                        return DBOperation.Error;

                    var isSuccess = await _auditLogService.CreateAuditLog<PIDFEntity>(entityPIDF.PIDFID > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
                       Utility.Enums.ModuleEnum.PIDF, oldPIDFEntity, entityPIDF, Convert.ToInt32(objPIDF.Pidfid));

                    return DBOperation.Success;
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                List<PIDFEntity> Pidflist = new List<PIDFEntity>();
                Pidflist = await GetAll();
                var lastPidfId = Pidflist.Where(m => m.IsActive == true).OrderByDescending(m => m.PIDFID).First();
                entityPIDF.PIDFNO = "PIDF-00";
                entityPIDF.IsActive = true;
                oldPIDFEntity = _mapperFactory.Get<Pidf, PIDFEntity>(new Pidf { });
                objPIDF = _mapperFactory.Get<PIDFEntity, Pidf>(entityPIDF);
                _repository.AddAsync(objPIDF);

                var id = lastPidfId.PIDFID;
                id++;
                objPIDF.Pidfno = "PIDF-00" + id;                
                objPIDF.LastStatusId = (Int32)Master_PIDFStatus.PIDFInProgress;
                await _unitOfWork.SaveChangesAsync();

                _repository.UpdateAsync(objPIDF);

                foreach (var item in entityPIDF.pidfApiDetailEntities)
                {
                    Pidfapidetail pidfapidetail;
                    item.Pidfid = id;
                    item.ModifyDate = DateTime.Now;
                    item.ModifyBy = 1;
                    pidfapidetail = _mapperFactory.Get<PidfApiDetailEntity, Pidfapidetail>(item);
                    _pidfApiRepository.AddAsync(pidfapidetail);
                }
                foreach (var item in entityPIDF.pidfProductStregthEntities)
                {
                    PidfproductStrength pidfProductStrength;
                    item.Pidfid = id;
                    item.ModifyDate = DateTime.Now;
                    item.ModifyBy = 1;
                    pidfProductStrength = _mapperFactory.Get<PidfProductStregthEntity, PidfproductStrength>(item);
                    _pidfProductStrength.AddAsync(pidfProductStrength);
                }
                await _unitOfWork.SaveChangesAsync();

                if (objPIDF.Pidfid == 0)
                    return DBOperation.Error;

                var isSuccess = await _auditLogService.CreateAuditLog<PIDFEntity>(entityPIDF.PIDFID > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
                       Utility.Enums.ModuleEnum.PIDF, oldPIDFEntity, entityPIDF, Convert.ToInt32(objPIDF.Pidfid));


                return DBOperation.Success;
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
