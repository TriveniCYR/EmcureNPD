﻿using EmcureNPD.Business.Core.Interface;
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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class CommercialFormService : ICommercialFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IMasterBusinessUnitService _businessUnitService;
        private readonly IMasterCountryService _countryService;
        private readonly IMasterAuditLogService _auditLogService;
        private readonly IPidfProductStrengthService _productStrengthService;
        private readonly INotificationService _notificationService;
        private readonly IExceptionService _ExceptionService;
        private IRepository<PidfIpd> _repository { get; set; }
        private IRepository<PidfCommercial> _commercialrepository { get; set; }
        private IRepository<PidfCommercialYear> _commercialYearrepository { get; set; }
        private IRepository<PidfIpdPatentDetail> _ipdParentRepository { get; set; }
        private IRepository<MasterRegion> _regionRepository { get; set; }
        private IRepository<MasterUserRegionMapping> _userRegionRepository { get; set; }
        private IRepository<MasterRegionCountryMapping> _userRegionCountryRepository { get; set; }
        private IRepository<PidfIpdRegion> _ipdRegionRepository { get; set; }
        private IRepository<PidfIpdCountry> _ipdCountryRepository { get; set; }
        private IRepository<Pidf> _pidfrepository { get; set; }
        private IRepository<MasterBusinessUnit> _businessUnitrepository { get; set; }
        private IRepository<MasterCountry> _countryrepository { get; set; }
        private IRepository<PidfproductStrength> _pidfProductStrengthrepository { get; set; }
        private IRepository<MasterFinalSelection> _finalSelectionrepository { get; set; }

        public CommercialFormService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory,
            IMasterBusinessUnitService businessUnitService, IMasterCountryService countryService,
            INotificationService notificationService,
            IMasterAuditLogService auditLogService, IPidfProductStrengthService productStrengthService, IExceptionService exceptionService)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _businessUnitService = businessUnitService;
            _countryService = countryService;
            _repository = _unitOfWork.GetRepository<PidfIpd>();
            _ipdParentRepository = unitOfWork.GetRepository<PidfIpdPatentDetail>();
            _auditLogService = auditLogService;
            _regionRepository = _unitOfWork.GetRepository<MasterRegion>();
            _userRegionRepository = _unitOfWork.GetRepository<MasterUserRegionMapping>();
            _userRegionCountryRepository = _unitOfWork.GetRepository<MasterRegionCountryMapping>();
            _ipdRegionRepository = unitOfWork.GetRepository<PidfIpdRegion>();
            _ipdCountryRepository = unitOfWork.GetRepository<PidfIpdCountry>();
            _pidfrepository = unitOfWork.GetRepository<Pidf>();
            _productStrengthService = productStrengthService;
            _commercialrepository = _unitOfWork.GetRepository<PidfCommercial>();
            _commercialYearrepository = _unitOfWork.GetRepository<PidfCommercialYear>();
            _finalSelectionrepository = _unitOfWork.GetRepository<MasterFinalSelection>();
            _notificationService = notificationService;
            _businessUnitrepository = _unitOfWork.GetRepository<MasterBusinessUnit>();
            _pidfProductStrengthrepository = _unitOfWork.GetRepository<PidfproductStrength>();
            _countryrepository = _unitOfWork.GetRepository<MasterCountry>();
            _ExceptionService = exceptionService;
        }

        //public async Task<IPDEntity> FillDropdown()
        //{
        //    var PIDForm = new IPDEntity
        //    {
        //        MasterBusinessUnitEntities = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList(),
        //        MasterCountries = _countryService.GetAll().Result.Where(xx => xx.IsActive).ToList(),
        //    };
        //    return PIDForm;
        //}
        public async Task<DBOperation> AddUpdateCommercialPIDF(PIDFCommercialEntity entitycommPIDF)
        {
            var loggedInUserID = entitycommPIDF.CreatedBy;
            var listYear = new List<PidfCommercialYear>();
            int i = 1;
            foreach (var year in entitycommPIDF.PidfCommercialYears)
            {
                var Yeardata = _mapperFactory.Get<PidfCommercialYearEntity, PidfCommercialYear>(year);
                Yeardata.PackagingTypeId = year.CommercialPackagingTypeId;
                Yeardata.PidfcommercialYearId = 0;
                Yeardata.PidfcommercialId = 0;
                Yeardata.YearIndex = i;
                listYear.Add(Yeardata);
                i++;
            }
            var IsUpdateData = (_commercialrepository.Get(x => x.Pidfid == entitycommPIDF.Pidfid) == null);
            Expression<Func<PidfCommercial, bool>> expr = u => u.BusinessUnitId == entitycommPIDF.BusinessUnitId && u.Pidfid == entitycommPIDF.Pidfid && u.PidfproductStrengthId == entitycommPIDF.PidfproductStrengthId;
            var objFetchData = await _commercialrepository.GetAsync(expr);
            var OldObjpidfCommercial = objFetchData;
            if (objFetchData == null)
            {
                var NewCommPIDF = new PidfCommercial();

                NewCommPIDF.Pidfid = entitycommPIDF.Pidfid;
                NewCommPIDF.BusinessUnitId = entitycommPIDF.BusinessUnitId;
                NewCommPIDF.PidfproductStrengthId = entitycommPIDF.PidfproductStrengthId;
                NewCommPIDF.MarketSizeInUnit = entitycommPIDF.MarketSizeInUnit;
                NewCommPIDF.ShelfLife = entitycommPIDF.ShelfLife;
                NewCommPIDF.CreatedDate = DateTime.Now;
                NewCommPIDF.CreatedBy = entitycommPIDF.CreatedBy;
                NewCommPIDF.IsDeleted = false;

                NewCommPIDF.PidfCommercialYears = listYear;
                _commercialrepository.AddAsync(NewCommPIDF);
                await _unitOfWork.SaveChangesAsync();
                if (IsUpdateData)
                {
                    //await _auditLogService.CreateAuditLog<PidfCommercial>(Utility.Audit.AuditActionType.Update,
                    //                ModuleEnum.PIDF, OldObjpidfCommercial, NewCommPIDF, loggedInUserID);
                }
            }
            else
            {
                //Remove all Already mapped Years data
                var CommercialYears = await _commercialYearrepository.GetAllAsync(x => x.PidfcommercialId == objFetchData.PidfcommercialId);
                foreach (var item in CommercialYears)
                    _commercialYearrepository.Remove(item);
                //await _unitOfWork.SaveChangesAsync();

                objFetchData.PidfCommercialYears = listYear;
                objFetchData.PidfproductStrengthId = entitycommPIDF.PidfproductStrengthId;
                objFetchData.MarketSizeInUnit = entitycommPIDF.MarketSizeInUnit;
                objFetchData.ModifyBy = entitycommPIDF.CreatedBy;
                objFetchData.MarketSizeInUnit = entitycommPIDF.MarketSizeInUnit;
                objFetchData.ShelfLife = entitycommPIDF.ShelfLife;
                objFetchData.ModifyDate = DateTime.Now;
                _commercialrepository.UpdateAsync(objFetchData);
                await _unitOfWork.SaveChangesAsync();
                //var isSuccess = await _auditLogService.CreateAuditLog<PidfCommercial>(Utility.Audit.AuditActionType.Update,
                //ModuleEnum.PIDF, OldObjpidfCommercial, objFetchData, loggedInUserID);
            }
            var _StatusID = (entitycommPIDF.SaveType == "Sv") ? Master_PIDFStatus.CommercialSubmitted : Master_PIDFStatus.CommercialInProgress;
            await _auditLogService.UpdatePIDFStatusCommon(entitycommPIDF.Pidfid, (int)_StatusID, entitycommPIDF.CreatedBy);
            await _notificationService.CreateNotification(entitycommPIDF.Pidfid, (int)_StatusID, string.Empty, string.Empty, entitycommPIDF.CreatedBy);
            return DBOperation.Success;
        }

        public async Task<PIDFCommercialEntity> GetCommercialFormData(long pidfId, int buid, int? strengthid)
        {
            Expression<Func<PidfCommercial, bool>> expr;
            if (strengthid != null)
            {
                expr = u => u.BusinessUnitId == buid && u.Pidfid == pidfId && u.PidfproductStrengthId == strengthid;
            }
            else
            {
                expr = u => u.BusinessUnitId == buid && u.Pidfid == pidfId;
            }
            //var data = _mapperFactory.Get<Pidfipd, PIDFormEntity>(await _repository.GetAsync(id));

            dynamic objData = (dynamic)await _commercialrepository.FindAllAsync(expr);
            var data = new PIDFCommercialEntity();
            if (objData != null && objData.Count > 0)
            {
                data = _mapperFactory.Get<PidfCommercial, PIDFCommercialEntity>(objData[0]);
                data.PidfproductStrengthId = objData[0].PidfproductStrengthId;

                var years = await _commercialYearrepository.GetAllAsync(x => x.PidfcommercialId == data.PidfcommercialId);
                foreach (var year in years)
                {
                    var Yeardata = _mapperFactory.Get<PidfCommercialYear, PidfCommercialYearEntity>(year);
                    Yeardata.CommercialPackagingTypeId = year.PackagingTypeId;
                    data.PidfCommercialYears.Add(Yeardata);
                }
            }

            data.MasterBusinessUnitEntities = _mapperFactory.GetList<MasterBusinessUnit, MasterBusinessUnitEntity>
                (_businessUnitrepository.GetAllQuery().Where(xx => xx.IsActive).ToList());
            data.MasterStrengthEntities = _mapperFactory.GetList<PidfproductStrength, PidfProductStregthEntity>
                (_pidfProductStrengthrepository.GetAllQuery().Where(x => x.Pidfid == pidfId).ToList());
            data.BusinessUnitId = buid;
            data.Pidfid = pidfId;
            Pidf objPidf = await _pidfrepository.GetAsync(pidfId);
            data.StatusId = objPidf.StatusId;

            return data;
        }

        public async Task<IEnumerable<dynamic>> GetAllRegion(int userId)
        {
            var dataRegion = _mapperFactory.GetList<MasterRegion, RegionEntity>(await _regionRepository.GetAllAsync());
            var dataUserRegion = _mapperFactory.GetList<MasterUserRegionMapping, UserRegionMappingEntity>(await _userRegionRepository.FindAllAsync(xx => xx.UserId == userId));

            if (dataRegion.Any())
            {
                var person = (from p in dataUserRegion
                              join m in dataRegion on p.RegionId equals m.RegionId
                              select new
                              {
                                  RegionId = p.RegionId,
                                  RegionName = m.RegionName,
                              }).ToList();
                return person;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<dynamic>> GetCountryRefByRegionIds(string regionIds)
        {
            //int[] intIDs = new[] { 1, 2 };

            string[] regionList = regionIds.Split(",");
            if (regionList.Length > 0)
            {
                int[] intIDs = new int[regionList.Length];

                for (int i = 0; i < regionList.Length; i++)
                {
                    intIDs[i] = Convert.ToInt32(regionList[i]);
                }

                var dataCountry = _countryrepository.GetAllQuery().Where(xx => xx.IsActive).ToList();

                var dataRegionCountry = _mapperFactory.GetList<MasterRegionCountryMapping, MasterRegionCountryMapping>(await _userRegionCountryRepository.FindAllAsync(xx => intIDs.Contains(xx.RegionId)));

                if (dataCountry.Any())
                {
                    var countryList = (from p in dataCountry
                                       join m in dataRegionCountry on p.CountryId equals m.CountryId
                                       select new
                                       {
                                           CountryId = p.CountryId,
                                           CountryName = p.CountryName,
                                       }).ToList();
                    return countryList;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public async Task<DataTableResponseModel> GetAllIPDPIDFList(DataTableAjaxPostModel model)
        {
            string ColumnName = (model.order.Count > 0 ? model.columns[model.order[0].column].data : string.Empty);
            string SortDir = (model.order.Count > 0 ? model.order[0].dir : string.Empty);

            SqlParameter[] osqlParameter = {
                new SqlParameter("@StatusId", Master_PIDFStatus.PIDFApproved),
                new SqlParameter("@CurrentPageNumber", model.start),
                    new SqlParameter("@PageSize", model.length),
                    new SqlParameter("@SortColumn", ColumnName),
                    new SqlParameter("@SortDirection", SortDir),
                    new SqlParameter("@SearchText", model.search.value)
            };

            var PIDFList = await _repository.GetBySP("stp_npd_GetIpdPIDFList", System.Data.CommandType.StoredProcedure, osqlParameter);

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

        public async Task<DBOperation> ApproveRejectIpdPidf(EntryApproveRej oApprRej)
        {
            if (oApprRej != null && oApprRej.PidfIds.Count > 0)
            {
                int saveTId = 0;
                if (oApprRej.SaveType == "R")
                    saveTId = 5;
                else
                    saveTId = 6;

                for (int i = 0; i < oApprRej.PidfIds.Count; i++)
                {
                    Pidf objPidf = await _pidfrepository.GetAsync(oApprRej.PidfIds[i].pidfId);
                    objPidf.LastStatusId = objPidf.StatusId;
                    objPidf.StatusId = saveTId;
                    _pidfrepository.UpdateAsync(objPidf);

                    await _unitOfWork.SaveChangesAsync();
                }
                //var isSuccess = await _auditLogService.CreateAuditLog<EntryApproveRej>(Utility.Audit.AuditActionType.Update,
                //   Utility.Enums.ModuleEnum.IPD, oApprRej, oApprRej, 0);

                return DBOperation.Success;
            }
            else
            {
                return DBOperation.NotFound;
            }
        }

        public async Task<List<MasterFinalSelectionEntity>> GetAllFinalSelection()
        {
            var list = await _finalSelectionrepository.GetAllAsync(x => x.IsActive == true);
            return _mapperFactory.GetList<MasterFinalSelection, MasterFinalSelectionEntity>(list.ToList());
        }
    }
}