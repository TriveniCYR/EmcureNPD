﻿using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Utility;
using Microsoft.Extensions.ObjectPool;
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

        private readonly IPidfApiDetailsService _PidfApiDetailsService;
        private readonly IPidfProductStrengthService _pidfProductStrengthService;

        private readonly IMasterAuditLogService _auditLogService;
        private readonly IHelper _helper;
        private readonly INotificationService _notificationService;
        private readonly IExceptionService _ExceptionService;
        private IRepository<Pidf> _repository { get; set; }
        private IRepository<Pidfapidetail> _pidfApiRepository { get; set; }
        private IRepository<PidfproductStrength> _pidfProductStrength { get; set; }
        private IRepository<Pidfimsdatum> _pidfPidfimsdata { get; set; }
        private IRepository<PidfproductStrengthCountryMapping> _strengthCountryMapping { get; set; }

        private IRepository<PidfBusinessUnitInterested> _pidfBusinessUnitInterested { get; set; }
        private IRepository<PidfBusinessUnit> _pidfBusinessUnit { get; set; }
        private IRepository<PidfBusinessUnitCountry> _pidfBusinessUnitCountry { get; set; }
        private IRepository<MasterUser> _user { get; set; }


        public PIDFService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory,
            IPidfApiDetailsService pidfApiDetailsService, IPidfProductStrengthService pidfProductStrengthService,
             INotificationService notificationService,
            IMasterAuditLogService auditLogService, IHelper helper, IExceptionService exceptionService)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _notificationService = notificationService;
            _PidfApiDetailsService = pidfApiDetailsService;
            _pidfProductStrengthService = pidfProductStrengthService;
            _auditLogService = auditLogService;
            _pidfPidfimsdata = unitOfWork.GetRepository<Pidfimsdatum>();
            _repository = _unitOfWork.GetRepository<Pidf>();
            _pidfApiRepository = unitOfWork.GetRepository<Pidfapidetail>();
            _pidfProductStrength = unitOfWork.GetRepository<PidfproductStrength>();
            _strengthCountryMapping = unitOfWork.GetRepository<PidfproductStrengthCountryMapping>();

            _pidfBusinessUnitInterested = unitOfWork.GetRepository<PidfBusinessUnitInterested>();
            _pidfBusinessUnit = unitOfWork.GetRepository<PidfBusinessUnit>();
            _pidfBusinessUnitCountry = unitOfWork.GetRepository<PidfBusinessUnitCountry>();
            _user = unitOfWork.GetRepository<MasterUser>();

            _helper = helper;
            _ExceptionService = exceptionService;
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
            DropdownObjects.MasterIndications = dsDropdownOptions.Tables[8];

            //DropdownObjects.MasterCountrys = GetCountryByUserId(userid).Result;
            DropdownObjects.InHouses = new List<InHouseEntity> { new InHouseEntity { InHouseId = 1, InHouseName = "Yes" }, new InHouseEntity { InHouseId = 2, InHouseName = "No" } };
            return DropdownObjects;
        }

        public async Task<dynamic> GetIsInterestedByPIDFandBU(int PIDFID, int BussinesUnitId)
        {
            dynamic DropdownObjects = new ExpandoObject();

            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFID", PIDFID),
                 new SqlParameter("@BUID", BussinesUnitId)
            };

            DataSet dsDropdownOptions = await _repository.GetDataSetBySP("std_npd_GetIsInterestedByPIDFandBU", System.Data.CommandType.StoredProcedure, osqlParameter);

            DropdownObjects.IsIntresetedStatusOfBU = dsDropdownOptions.Tables[0];

            return DropdownObjects;
        }

        public async Task<List<MasterBusinessUnitEntity>> GetBusinessUNitByUserId(int userid)
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", userid)
            };

            var dbresult = await _repository.GetDataSetBySP("stp_npd_GetBusinessUnitByUserId", System.Data.CommandType.StoredProcedure, osqlParameter);

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

            var dbresult = await _repository.GetDataSetBySP("stp_npd_GetCountryByUserId", System.Data.CommandType.StoredProcedure, osqlParameter);

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

        public async Task<DataTableResponseModel> GetAllPIDFList(DataTableAjaxPostModel model, int ScreenId)
        {
            var loggedInUserId = _helper.GetLoggedInUser().UserId;

            string ColumnName = (model.order != null && model.order.Count > 0 ? model.columns[model.order[0].column].data : string.Empty);
            string SortDir = (model.order != null && model.order.Count > 0 ? model.order[0].dir : string.Empty);
            string SearchText = (model.search != null ? model.search.value : string.Empty);
            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", loggedInUserId),
                new SqlParameter("@CurrentPageNumber", model.start),
                    new SqlParameter("@PageSize", model.length),
                    new SqlParameter("@SortColumn", ColumnName),
                    new SqlParameter("@SortDirection", SortDir),
                    new SqlParameter("@SearchText", SearchText),
                    new SqlParameter("@ScreenId", ScreenId),

                    new SqlParameter("@countryid", model.countryid),
                    new SqlParameter("@marketextenstionid", model.marketextenstionid),
                    new SqlParameter("@buid", model.buid),
                    new SqlParameter("@manufacturingid", model.manufacturingid),
                    new SqlParameter("@budgetlaunchdate", model.budgetlaunchdate),
                    new SqlParameter("@inhouse", model.inhouse),

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
            bool IsApprvedPIDF = false;
            //int? BusinessUnitIdForChildTable = entityPIDF.BusinessUnitId;
            try
            {
                var loggedInUserId = _helper.GetLoggedInUser().UserId;
                IsApprvedPIDF = _repository.Exists(x => x.Pidfid == entityPIDF.PIDFID && x.StatusId >= (int)Master_PIDFStatus.PIDFApproved);
                if (IsApprvedPIDF)
                {
                    bool IsSuccess = await SaveProductStrength_ExtendedCountry(entityPIDF.PIDFID, loggedInUserId, entityPIDF.pidfProductStregthEntities, entityPIDF.SelectedBusinessUnitId);
                    return DBOperation.Success;
                }

                if (entityPIDF.PIDFID > 0)
                {
                    objPIDF = await _repository.GetAsync(entityPIDF.PIDFID);
                    if (objPIDF != null)
                    {
                        if (!string.IsNullOrEmpty(entityPIDF.PIDFIsInterested) && (entityPIDF.PIDFIsInterested == "1" || entityPIDF.PIDFIsInterested == "0"))
                        {

                            var _objPIDFInterested = _pidfBusinessUnitInterested.GetAllQuery().Where(x => x.Pidfid == entityPIDF.PIDFID && x.BusinessUnitId == entityPIDF.SelectedBusinessUnitId).FirstOrDefault();

                            if (_objPIDFInterested == null)
                            {
                                _objPIDFInterested = new PidfBusinessUnitInterested();
                            }
                            _objPIDFInterested.IsInterested = (entityPIDF.PIDFIsInterested == "1" ? true : false);
                            _objPIDFInterested.CreatedDate = DateTime.Now;
                            _objPIDFInterested.CreatedBy = loggedInUserId;

                            if (_objPIDFInterested != null && _objPIDFInterested.Pidfid > 0)
                            {
                                _pidfBusinessUnitInterested.UpdateAsync(_objPIDFInterested);
                            }
                            else
                            {
                                _objPIDFInterested.Pidfid = entityPIDF.PIDFID;
                                _objPIDFInterested.BusinessUnitId = entityPIDF.SelectedBusinessUnitId;
                                _pidfBusinessUnitInterested.AddAsync(_objPIDFInterested);
                            }
                            await _unitOfWork.SaveChangesAsync();

                            if (_objPIDFInterested.IsInterested)
                            {
                                var _objPIDFBusinessUnit = _pidfBusinessUnit.GetAllQuery().Where(x => x.Pidfid == entityPIDF.PIDFID && x.BusinessUnitId == entityPIDF.SelectedBusinessUnitId).FirstOrDefault();

                                if (_objPIDFBusinessUnit != null && _objPIDFBusinessUnit.Pidfid > 0)
                                {
                                    _objPIDFBusinessUnit = _mapperFactory.Get<PIDFEntity, PidfBusinessUnit>(entityPIDF);
                                    _objPIDFBusinessUnit.BusinessUnitId = entityPIDF.SelectedBusinessUnitId;
                                    _objPIDFBusinessUnit.ModifyBy = loggedInUserId;
                                    _objPIDFBusinessUnit.ModifyDate = DateTime.Now;
                                    _pidfBusinessUnit.UpdateAsync(_objPIDFBusinessUnit);
                                }
                                else
                                {
                                    _objPIDFBusinessUnit = _mapperFactory.Get<PIDFEntity, PidfBusinessUnit>(entityPIDF);
                                    _objPIDFBusinessUnit.BusinessUnitId = entityPIDF.SelectedBusinessUnitId;
                                    _objPIDFBusinessUnit.CreatedBy = loggedInUserId;
                                    _objPIDFBusinessUnit.ModifyBy = entityPIDF.SelectedBusinessUnitId;
                                    _objPIDFBusinessUnit.ModifyDate = DateTime.Now;
                                    _objPIDFBusinessUnit.CreatedDate = DateTime.Now;
                                    _pidfBusinessUnit.AddAsync(_objPIDFBusinessUnit);
                                }
                                await _unitOfWork.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            if (objPIDF.BusinessUnitId != entityPIDF.SelectedBusinessUnitId)
                            {
                                var objPIDFBusinessUnit = _pidfBusinessUnit.GetAllQuery().Where(x => x.Pidfid == entityPIDF.PIDFID && x.BusinessUnitId == entityPIDF.SelectedBusinessUnitId).FirstOrDefault();

                                if (objPIDFBusinessUnit != null)
                                {

                                    objPIDFBusinessUnit.OralId = entityPIDF.OralId;
                                    objPIDFBusinessUnit.UnitofMeasurementId = entityPIDF.UnitofMeasurementId;
                                    objPIDFBusinessUnit.DosageFormId = entityPIDF.DosageFormId;
                                    objPIDFBusinessUnit.PackagingTypeId = entityPIDF.PackagingTypeId;
                                    objPIDFBusinessUnit.BrandName = entityPIDF.BrandName;
                                    objPIDFBusinessUnit.ApprovedGenerics = entityPIDF.ApprovedGenerics;
                                    objPIDFBusinessUnit.LaunchedGenerics = entityPIDF.LaunchedGenerics;
                                    objPIDFBusinessUnit.Rfdbrand = entityPIDF.RFDBrand;
                                    objPIDFBusinessUnit.Rfdapplicant = entityPIDF.RFDApplicant;
                                    objPIDFBusinessUnit.RfdcountryId = entityPIDF.RFDCountryId;
                                    objPIDFBusinessUnit.Rfdindication = entityPIDF.RFDIndication;
                                    objPIDFBusinessUnit.Rfdinnovators = entityPIDF.RFDInnovators;
                                    objPIDFBusinessUnit.RfdinitialRevenuePotential = entityPIDF.RFDInitialRevenuePotential;
                                    objPIDFBusinessUnit.RfdpriceDiscounting = entityPIDF.RFDPriceDiscounting;
                                    objPIDFBusinessUnit.RfdcommercialBatchSize = entityPIDF.RFDCommercialBatchSize;
                                    objPIDFBusinessUnit.Diaid = entityPIDF.Diaid;
                                    objPIDFBusinessUnit.IndicationId = entityPIDF.IndicationId;
                                    objPIDFBusinessUnit.MarketExtenstionId = entityPIDF.MarketExtenstionId;
                                    objPIDFBusinessUnit.TradeNameDate = entityPIDF.TradeNameDate;
                                    objPIDFBusinessUnit.TradeNameRequired = entityPIDF.TradeNameRequired;

                                    objPIDFBusinessUnit.ModifyBy = loggedInUserId;
                                    objPIDFBusinessUnit.ModifyDate = DateTime.Now;
                                    _pidfBusinessUnit.UpdateAsync(objPIDFBusinessUnit);
                                }
                                else
                                {
                                    objPIDFBusinessUnit = _mapperFactory.Get<PIDFEntity, PidfBusinessUnit>(entityPIDF);

                                    objPIDFBusinessUnit.CreatedBy = loggedInUserId;
                                    objPIDFBusinessUnit.CreatedDate = DateTime.Now;
                                    _pidfBusinessUnit.AddAsync(objPIDFBusinessUnit);
                                }
                                await _unitOfWork.SaveChangesAsync();
                            }
                            else
                            {

                                var CurrentStatus = objPIDF.StatusId;
                                var CurrentStatusDateTime = objPIDF.StatusUpdatedDate;
                                var CurrentStatusUpdatedBy = objPIDF.StatusUpdatedBy;

                                PIDFEntity _previousPIDFEntity = _mapperFactory.Get<Pidf, PIDFEntity>(objPIDF);
                                objPIDF = _mapperFactory.Get<PIDFEntity, Pidf>(entityPIDF);
                                objPIDF.ModifyBy = loggedInUserId;
                                objPIDF.ModifyDate = DateTime.Now;

                                if (IsApprvedPIDF)
                                {
                                    objPIDF.StatusId = CurrentStatus;
                                    objPIDF.StatusUpdatedBy = CurrentStatusUpdatedBy;
                                    objPIDF.StatusUpdatedDate = CurrentStatusDateTime;
                                }
                                else
                                {
                                    objPIDF.StatusUpdatedBy = loggedInUserId;
                                    objPIDF.StatusUpdatedDate = DateTime.Now;
                                }
                                _repository.UpdateAsync(objPIDF);

                                await _unitOfWork.SaveChangesAsync();

                                var isSuccess = await _auditLogService.CreateAuditLog<PIDFEntity>(entityPIDF.PIDFID > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
                                    Utility.Enums.ModuleEnum.PIDF, _previousPIDFEntity, entityPIDF, Convert.ToInt32(objPIDF.Pidfid));
                            }

                        }

                        var apiDetailsList = _pidfApiRepository.GetAllQuery().Where(x => x.Pidfid == entityPIDF.PIDFID && x.BusinessUnitId == entityPIDF.SelectedBusinessUnitId).ToList();
                        if (apiDetailsList != null && apiDetailsList.Count() > 0)
                        {
                            foreach (var item in apiDetailsList)
                            {
                                _pidfApiRepository.Remove(item);
                            }
                            await _unitOfWork.SaveChangesAsync();
                        }

                        if (!IsApprvedPIDF) // if PIDF is not apprved then only remove from ProductStrength child table
                        {

                            var productStrengthList = _pidfProductStrength.GetAllQuery().Where(x => x.Pidfid == entityPIDF.PIDFID && x.BusinessUnitId == entityPIDF.SelectedBusinessUnitId).ToList();

                            foreach (var item in productStrengthList)
                            {
                                var strengthCountryList = _strengthCountryMapping.GetAllQuery().Where(x => x.PidfproductStrengthId == item.PidfproductStrengthId).ToList();
                                foreach (var x in strengthCountryList)
                                {
                                    _strengthCountryMapping.Remove(x);
                                }
                            }
                            await _unitOfWork.SaveChangesAsync();

                            foreach (var item in productStrengthList)
                            {
                                _pidfProductStrength.Remove(item);
                            }
                            await _unitOfWork.SaveChangesAsync();

                        }

                        var imsDataList = _pidfPidfimsdata.GetAllQuery().Where(x => x.Pidfid == entityPIDF.PIDFID && x.BusinessUnitId == entityPIDF.SelectedBusinessUnitId).ToList();
                        foreach (var item in imsDataList)
                        {
                            _pidfPidfimsdata.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();

                        await SaveChildDetails(objPIDF.Pidfid, loggedInUserId, entityPIDF.pidfApiDetailEntities, entityPIDF.pidfProductStregthEntities, entityPIDF.IMSDataEntities, entityPIDF.SelectedBusinessUnitId);

                        await _notificationService.CreateNotification(objPIDF.Pidfid, entityPIDF.StatusId, string.Empty, string.Empty, loggedInUserId);
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
                    //BusinessUnitIdForChildTable = entityPIDF.BusinessUnitId;
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

                    await SaveChildDetails(objPIDF.Pidfid, loggedInUserId, entityPIDF.pidfApiDetailEntities, entityPIDF.pidfProductStregthEntities, entityPIDF.IMSDataEntities, entityPIDF.SelectedBusinessUnitId);

                    await _notificationService.CreateNotification(objPIDF.Pidfid, entityPIDF.StatusId, string.Empty, string.Empty, loggedInUserId);

                    return DBOperation.Success;
                }
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return DBOperation.Error;
            }
        }

        private async Task<bool> SaveProductStrength_ExtendedCountry(long Pidfid, int loggedInUserId, List<PidfProductStregthEntity> pidfProductStregthEntities, int? BusinessUnitId)
        {
            try
            {
                var ExistingObj = await _pidfProductStrength.GetAllAsync();
                if (pidfProductStregthEntities != null && pidfProductStregthEntities.Count() > 0)
                {
                    List<PidfproductStrength> _ProductStrengthList = new List<PidfproductStrength>();
                    foreach (var item in pidfProductStregthEntities)
                    {
                        if (!ExistingObj.Exists(x => x.PidfproductStrengthId == item.PidfproductStrengthId))
                        {
                            PidfproductStrength pidfProductStrength = new PidfproductStrength();
                            item.Pidfid = Pidfid;
                            item.ModifyDate = DateTime.Now;
                            item.ModifyBy = loggedInUserId;
                            item.PidfproductStrengthId = 0;
                            pidfProductStrength = _mapperFactory.Get<PidfProductStregthEntity, PidfproductStrength>(item);
                            pidfProductStrength.BusinessUnitId = BusinessUnitId;

                            // setup country against the pidf strength
                            if (item.CountryId != null)
                            {
                                foreach (var x in item.CountryId)
                                {
                                    pidfProductStrength.PidfproductStrengthCountryMappings.Add(new PidfproductStrengthCountryMapping { CountryId = x, ModifyBy = loggedInUserId, ModifyDate = DateTime.Now });
                                }
                            }

                            _ProductStrengthList.Add(pidfProductStrength);
                        }
                    }
                    _pidfProductStrength.AddRangeAsync(_ProductStrengthList);
                    await _unitOfWork.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return false;
            }
        }

        private async Task<bool> SaveChildDetails(long Pidfid, int loggedInUserId, List<PidfApiDetailEntity> pidfApiDetailEntities, List<PidfProductStregthEntity> pidfProductStregthEntities, List<IMSDataEntity> IMSDataEntities, int? BusinessUnitId)
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
                        pidfapidetail.BusinessUnitId = BusinessUnitId;
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
                        item.PidfproductStrengthId = 0;
                        pidfProductStrength = _mapperFactory.Get<PidfProductStregthEntity, PidfproductStrength>(item);
                        pidfProductStrength.BusinessUnitId = BusinessUnitId;

                        // setup country against the pidf strength
                        if (item.CountryId != null)
                        {
                            foreach (var x in item.CountryId)
                            {
                                pidfProductStrength.PidfproductStrengthCountryMappings.Add(new PidfproductStrengthCountryMapping { CountryId = x, ModifyBy = loggedInUserId, ModifyDate = DateTime.Now });
                            }
                        }

                        _ProductStrengthList.Add(pidfProductStrength);
                    }
                    _pidfProductStrength.AddRangeAsync(_ProductStrengthList);
                    await _unitOfWork.SaveChangesAsync();
                }
                if (IMSDataEntities != null && IMSDataEntities.Count() > 0)
                {
                    List<Pidfimsdatum> _IMSDataEntitiesList = new List<Pidfimsdatum>();
                    foreach (var item in IMSDataEntities)
                    {
                        Pidfimsdatum pidfImsData = new Pidfimsdatum();
                        item.Pidfid = Pidfid;
                        item.ModifyDate = DateTime.Now;
                        item.ModifyBy = loggedInUserId;
                        pidfImsData = _mapperFactory.Get<IMSDataEntity, Pidfimsdatum>(item);
                        pidfImsData.BusinessUnitId = BusinessUnitId;
                        _IMSDataEntitiesList.Add(pidfImsData);
                    }
                    _pidfPidfimsdata.AddRangeAsync(_IMSDataEntitiesList);
                    await _unitOfWork.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return false;
            }
        }
        public async Task<PIDFEntity> GetById(int id)
        {
            var ids = Convert.ToInt64(id);
            var data = _mapperFactory.Get<Pidf, PIDFEntity>(await _repository.GetAsync(ids));
            data.pidfApiDetailEntities = _mapperFactory.GetList<Pidfapidetail, PidfApiDetailEntity>(_pidfApiRepository.GetAllQuery().Where(x => x.Pidfid == ids).ToList());
            data.pidfProductStregthEntities = _mapperFactory.GetList<PidfproductStrength, PidfProductStregthEntity>(_pidfProductStrength.GetAllQuery().Where(x => x.Pidfid == ids).ToList());
            data.IMSDataEntities = _mapperFactory.GetList<Pidfimsdatum, IMSDataEntity>(_pidfPidfimsdata.GetAllQuery().Where(x => x.Pidfid == ids).ToList());

            return data;
        }
        public async Task<PIDFEntity> GetById_BUId(int id, int BusinessUnitId)
        {
            var ids = Convert.ToInt64(id);

            var data = _mapperFactory.Get<Pidf, PIDFEntity>(await _repository.GetAsync(ids));

            if (data != null)
            {
                if (data.BusinessUnitId != null && data.BusinessUnitId > 0)
                {
                    if (BusinessUnitId <= 0)
                    {
                        BusinessUnitId = Convert.ToInt32(data.BusinessUnitId);
                    }
                }
            }

            if (BusinessUnitId > 0 && data.BusinessUnitId != BusinessUnitId)
            {
                var _objPIDFInterested = _pidfBusinessUnitInterested.GetAllQuery().Where(x => x.Pidfid == id && x.BusinessUnitId == BusinessUnitId).FirstOrDefault();

                data.pidfInterested = new PIDFInterested();
                if (_objPIDFInterested != null)
                {
                    data.pidfInterested.IsInterested = _objPIDFInterested.IsInterested;
                    data.pidfInterested.CreatedDate = _objPIDFInterested.CreatedDate;
                    data.pidfInterested.CreatedByName = _user.GetAllQuery().Where(x => x.UserId == _objPIDFInterested.CreatedBy).Select(x => x.FullName).FirstOrDefault();

                    if (_objPIDFInterested.IsInterested)
                    {
                        data.InterestedCountries = _pidfBusinessUnitCountry.GetAllQuery().Where(x => x.Pidfid == id && x.BusinessUnitId == BusinessUnitId).Select(x => x.CountryId).ToArray();

                        var _objPIDFBusinessUnit = _pidfBusinessUnit.GetAllQuery().Where(x => x.Pidfid == id && x.BusinessUnitId == BusinessUnitId).FirstOrDefault();

                        if (_objPIDFBusinessUnit != null && _objPIDFBusinessUnit.PidfbusinessUnitId > 0)
                        {
                            data.OralId = _objPIDFBusinessUnit.OralId;
                            data.UnitofMeasurementId = _objPIDFBusinessUnit.UnitofMeasurementId;
                            data.DosageFormId = _objPIDFBusinessUnit.DosageFormId;
                            data.PackagingTypeId = _objPIDFBusinessUnit.PackagingTypeId;
                            data.BrandName = _objPIDFBusinessUnit.BrandName;
                            data.ApprovedGenerics = _objPIDFBusinessUnit.ApprovedGenerics;
                            data.LaunchedGenerics = _objPIDFBusinessUnit.LaunchedGenerics;
                            data.RFDBrand = _objPIDFBusinessUnit.Rfdbrand;
                            data.RFDApplicant = _objPIDFBusinessUnit.Rfdapplicant;
                            data.RFDCountryId = _objPIDFBusinessUnit.RfdcountryId;
                            data.RFDIndication = _objPIDFBusinessUnit.Rfdindication;
                            data.RFDInnovators = _objPIDFBusinessUnit.Rfdinnovators;
                            data.RFDInitialRevenuePotential = _objPIDFBusinessUnit.RfdinitialRevenuePotential;
                            data.RFDPriceDiscounting = _objPIDFBusinessUnit.RfdpriceDiscounting;
                            data.RFDCommercialBatchSize = _objPIDFBusinessUnit.RfdcommercialBatchSize;
                            data.Diaid = _objPIDFBusinessUnit.Diaid; 
                            data.IndicationId = _objPIDFBusinessUnit.IndicationId;
                            data.MarketExtenstionId = _objPIDFBusinessUnit.MarketExtenstionId;
                            data.TradeNameDate = _objPIDFBusinessUnit.TradeNameDate;
                            data.TradeNameRequired = (_objPIDFBusinessUnit.TradeNameRequired == null ? false : Convert.ToBoolean(_objPIDFBusinessUnit.TradeNameRequired));
                        }
                    }
                    else
                    {
                        BusinessUnitId = Convert.ToInt32(data.BusinessUnitId);
                    }
                }
                else
                {
                    BusinessUnitId = Convert.ToInt32(data.BusinessUnitId);
                }
            }

            data.pidfApiDetailEntities = _mapperFactory.GetList<Pidfapidetail, PidfApiDetailEntity>(_pidfApiRepository.GetAllQuery().Where(x => x.Pidfid == ids && x.BusinessUnitId == BusinessUnitId).ToList());

            data.pidfProductStregthEntities = _mapperFactory.GetList<PidfproductStrength, PidfProductStregthEntity>(_pidfProductStrength.GetAllQuery().Where(x => x.Pidfid == ids && x.BusinessUnitId == BusinessUnitId).ToList());

            if (data.pidfProductStregthEntities.Count() <= 0)
            {
                data.pidfProductStregthEntities.Add(new PidfProductStregthEntity { Pidfid = id, Strength = "" });
            }

            data.IMSDataEntities = _mapperFactory.GetList<Pidfimsdatum, IMSDataEntity>(_pidfPidfimsdata.GetAllQuery().Where(x => x.Pidfid == ids && x.BusinessUnitId == BusinessUnitId).ToList());

            for (int i = 0; i < data.pidfProductStregthEntities.Count; i++)
            {
                data.pidfProductStregthEntities[i].CountryId = _strengthCountryMapping.GetAllQuery().Where(x => x.PidfproductStrengthId == data.pidfProductStregthEntities[i].PidfproductStrengthId).Select(x => x.CountryId).ToArray();
            }


            SqlParameter[] osqlParameter = { new SqlParameter("@PIDFID", ids), new SqlParameter("@USERID", _helper.GetLoggedInUser().UserId) };

            DataSet dsDropdownOptions = await _repository.GetDataSetBySP("std_npd_GetIsAlloeToApprove", System.Data.CommandType.StoredProcedure, osqlParameter);

            var result = Convert.ToBoolean(dsDropdownOptions.Tables[0].Rows[0][0].ToString());
            data.IsAllowToApprove = result;
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
                    else if (oApprRej.ScreenId == Convert.ToString((Int32)PIDFScreen.PBF))
                        saveTId = (Int32)Master_PIDFStatus.PBFRejected;
                    else if (oApprRej.ScreenId == Convert.ToString((Int32)PIDFScreen.Finance))
                        saveTId = (Int32)Master_PIDFStatus.FinanceRejected;
                    else if (oApprRej.ScreenId == Convert.ToString((Int32)PIDFScreen.Management))
                        saveTId = (Int32)Master_PIDFStatus.ManagementRejected;
                    else
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
                    else
                        saveTId = (Int32)Master_PIDFStatus.PIDFApproved;
                }

                long pidf = 0; int by = _helper.GetLoggedInUser().UserId;
                for (int i = 0; i < oApprRej.PidfIds.Count; i++)
                {
                    Pidf objPidf;
                    if (oApprRej.PidfIds[i].pidfId > 0)
                    {
                        //Get current PDIF ID
                        if (pidf == 0)
                        {
                            pidf = oApprRej.PidfIds[i].pidfId;
                        }
                        objPidf = await _repository.GetAsync(pidf);
                        objPidf.LastStatusId = objPidf.StatusId;
                        objPidf.StatusId = saveTId;
                        objPidf.StatusRemark = oApprRej.Comment;
                        objPidf.StatusUpdatedBy = _helper.GetLoggedInUser().UserId;
                        objPidf.StatusUpdatedDate = DateTime.Now;

                        _repository.UpdateAsync(objPidf);
                        await _unitOfWork.SaveChangesAsync(); 

                        await _notificationService.CreateNotification(pidf, objPidf.StatusId, string.Empty, string.Empty, (int)objPidf.StatusUpdatedBy);
                    }
                }

                if (pidf != 0)
                {
                    await AddWorkflowtasks(pidf, by);

                    //Do after Approved or Rejection action
                    await AfterApproveRejectActions(pidf, saveTId);
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

        public async Task<dynamic> AddWorkflowtasks(long pidfId, int userId)
        {
            try
            {
                SqlParameter[] osqlParameter =
                {
                new SqlParameter("@PIDFID", pidfId),
                new SqlParameter("@UserId", userId)
            };

                var dbresult = await _repository.GetBySP("ProcAddWorkflowTasks", System.Data.CommandType.StoredProcedure, osqlParameter);
                return dbresult;
            }
            catch(Exception ex){
                return DBOperation.NotFound;
            }
        }

        /// <summary>
        /// Added by YReddy on 10/03/2023 for fixing PDIF after save changes
        /// </summary>
        /// <param name="pidfId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public async Task<dynamic> AfterApproveRejectActions(long pidfId, int statusId)
        {
            try
            {
                SqlParameter[] osqlParameter =
                {
                    new SqlParameter("@PIDFID",   pidfId),
                    new SqlParameter("@StatusId", statusId)
                };

                var dbresult = await _repository.GetBySP("stp_npd_AfterApproveRejectActions", System.Data.CommandType.StoredProcedure, osqlParameter);
                return dbresult;
            }
            catch (Exception ex)
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
                        objPidf.StatusRemark = oApprRej.Comment;
                    }
                    _repository.UpdateAsync(objPidf);

                    await _unitOfWork.SaveChangesAsync();
                    await _notificationService.CreateNotification(objPidf.Pidfid, objPidf.StatusId, string.Empty, string.Empty, (int)objPidf.StatusUpdatedBy);
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

        public async Task<dynamic> GetPIDFFilterFormData()
        {
            var loggedInUserId = _helper.GetLoggedInUser().UserId;

            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", loggedInUserId),
            };

            DataSet dsCommercial = await _repository.GetDataSetBySP("stp_npd_Get_PIDFListFilterDropdowndata", System.Data.CommandType.StoredProcedure, osqlParameter);

            dynamic DropdownObjects = new ExpandoObject();
            DropdownObjects.MasterCountry = dsCommercial.Tables[0];
            DropdownObjects.MasterMarketExtenstion = dsCommercial.Tables[1];
            DropdownObjects.MasterBusinessUnit = dsCommercial.Tables[2];
            DropdownObjects.MasterManufacturing = dsCommercial.Tables[3];
            DropdownObjects.BudgetLaunchDate = dsCommercial.Tables[4];
            return DropdownObjects;
        }
    }
}