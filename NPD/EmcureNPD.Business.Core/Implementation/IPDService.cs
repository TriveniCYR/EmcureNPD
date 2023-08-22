using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Core.ServiceImplementations;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class IPDService : IIPDService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IMasterBusinessUnitService _businessUnitService;
        private readonly IMasterCountryService _countryService;
        private readonly IMasterAuditLogService _auditLogService;
        private readonly IConfiguration _configuration;
        private readonly INotificationService _notificationService;

        private IRepository<PidfIpd> _repository { get; set; }
        private IRepository<PidfIpdPatentDetail> _ipdParentRepository { get; set; }
        private IRepository<MasterRegion> _regionRepository { get; set; }
        private IRepository<MasterUserRegionMapping> _userRegionRepository { get; set; }
        private IRepository<MasterRegionCountryMapping> _userRegionCountryRepository { get; set; }
        private IRepository<PidfIpdRegion> _ipdRegionRepository { get; set; }
        private IRepository<PidfIpdCountry> _ipdCountryRepository { get; set; }
        private IRepository<Pidf> _pidfrepository { get; set; }
        private IRepository<PidfMedical> _pidfMedicalrepository { get; set; }
        private IRepository<PidfMedicalFile> _pidfMedicalFilerepository { get; set; }
        private IRepository<MasterBusinessUnit> _businessUnitrepository { get; set; }
        private IRepository<MasterCountry> _countryrepository { get; set; }
        private IRepository<MasterPatentStrategy> _patentStrategyrepository { get; set; }
        private IRepository<MasterUserCountryMapping> _masterUserCountryMappingrepository { get; set; }
        private readonly IHelper _helper;

        public IPDService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IMasterBusinessUnitService businessUnitService,
            IMasterCountryService countryService, IMasterAuditLogService auditLogService, IConfiguration configuration,
            INotificationService notificationService, IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _businessUnitService = businessUnitService;
            _countryService = countryService;
            _auditLogService = auditLogService;
            _notificationService = notificationService;
            _helper = helper;

            _repository = _unitOfWork.GetRepository<PidfIpd>();
            _ipdParentRepository = unitOfWork.GetRepository<PidfIpdPatentDetail>();
            _regionRepository = _unitOfWork.GetRepository<MasterRegion>();
            _userRegionRepository = _unitOfWork.GetRepository<MasterUserRegionMapping>();
            _userRegionCountryRepository = _unitOfWork.GetRepository<MasterRegionCountryMapping>();
            _ipdRegionRepository = unitOfWork.GetRepository<PidfIpdRegion>();
            _ipdCountryRepository = unitOfWork.GetRepository<PidfIpdCountry>();
            _pidfrepository = unitOfWork.GetRepository<Pidf>();
            _pidfMedicalrepository = unitOfWork.GetRepository<PidfMedical>();
            _pidfMedicalFilerepository = unitOfWork.GetRepository<PidfMedicalFile>();
            _configuration = configuration;
            _businessUnitrepository = _unitOfWork.GetRepository<MasterBusinessUnit>();
            _countryrepository = _unitOfWork.GetRepository<MasterCountry>();
            _masterUserCountryMappingrepository = _unitOfWork.GetRepository<MasterUserCountryMapping>();
            _patentStrategyrepository = _unitOfWork.GetRepository<MasterPatentStrategy>();
        }

        public async Task<IPDEntity> FillDropdown()
        {
            var IPD = new IPDEntity
            {
                MasterBusinessUnitEntities = _mapperFactory.GetList<MasterBusinessUnit, MasterBusinessUnitEntity>
                (_businessUnitrepository.GetAllQuery().Where(xx => xx.IsActive).ToList()),

                MasterCountries = _mapperFactory.GetList<MasterCountry, MasterCountryEntity>
                (_countryrepository.GetAllQuery().Where(xx => xx.IsActive).ToList())
            };
            return IPD;
        }

        public async Task<DBOperation> AddUpdateIPD(IPDEntity entityIPD)
        {
            //PidfIpd objIPD = await _repository.GetAsync(x=>x.BusinessUnitId== entityIPD.SelectedTabBusinessUnit && x.Pidfid== entityIPD.PIDFID);
            PidfIpd objIPD;
            IPDEntity oldIPDFEntity;
            if (entityIPD.IPDID > 0)
            {
                objIPD = await _repository.GetAsync(entityIPD.IPDID);
                if (objIPD != null)
                {
                    entityIPD.ModifyBy = Convert.ToInt32(entityIPD.CreatedBy);
                    objIPD.ModifyBy = Convert.ToInt32(entityIPD.CreatedBy);
                    objIPD.ModifyDate = DateTime.Now;
                    oldIPDFEntity = _mapperFactory.Get<PidfIpd, IPDEntity>(objIPD);
                    oldIPDFEntity.StatusId = entityIPD.StatusId;
                    oldIPDFEntity.LogInId = entityIPD.LogInId;
                    oldIPDFEntity.SaveType = entityIPD.SaveType;
                    oldIPDFEntity.ProjectName = entityIPD.ProjectName;

                    objIPD = _mapperFactory.Get<IPDEntity, PidfIpd>(entityIPD);

                    _repository.UpdateAsync(objIPD);

                    await _unitOfWork.SaveChangesAsync();

                    if (entityIPD.pidf_IPD_PatentDetailsEntities != null && entityIPD.pidf_IPD_PatentDetailsEntities.Count() > 0)
                    {
                        var productStrengthList = _ipdParentRepository.GetAllQuery().Where(x => x.Ipdid == entityIPD.IPDID);
                        oldIPDFEntity.pidf_IPD_PatentDetailsEntities = new List<PIDF_IPD_PatentDetailsEntity>();
                        foreach (var item in productStrengthList)
                        {
                            oldIPDFEntity.pidf_IPD_PatentDetailsEntities.Add(_mapperFactory.Get<PidfIpdPatentDetail, PIDF_IPD_PatentDetailsEntity>(item));
                            _ipdParentRepository.Remove(item);
                        }

                        foreach (var item in entityIPD.pidf_IPD_PatentDetailsEntities)
                        {
                            if (item.CountryId != null && item.PatentNumber != null && item.Type != null)
                            {
                                PidfIpdPatentDetail pidf_ipd_PDetail;
                                item.IPDID = objIPD.Ipdid;
                                pidf_ipd_PDetail = _mapperFactory.Get<PIDF_IPD_PatentDetailsEntity, PidfIpdPatentDetail>(item);
                                pidf_ipd_PDetail.PatentType = (short?)IPDPatenDetailsType.PatenDetailsForFormulation;
                                _ipdParentRepository.AddAsync(pidf_ipd_PDetail);
                            }
                        }
                        foreach (var item in entityIPD.pidf_IPD_PatentDetailsEntitiesAPI)
                        {
                            if (item.CountryId != null && item.PatentNumber != null && item.Type != null)
                            {
                                PidfIpdPatentDetail pidf_ipd_PDetail;
                                item.IPDID = objIPD.Ipdid;
                                pidf_ipd_PDetail = _mapperFactory.Get<PIDF_IPD_PatentDetailsEntity, PidfIpdPatentDetail>(item);
                                pidf_ipd_PDetail.PatentType = (short?)IPDPatenDetailsType.PatientDetailsForAPI;
                                _ipdParentRepository.AddAsync(pidf_ipd_PDetail);
                            }
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                    if (!string.IsNullOrEmpty(entityIPD.RegionIds))
                    {
                        var regionRemove = _ipdRegionRepository.GetAllQuery().Where(x => x.Ipdid == entityIPD.IPDID);
                        foreach (var item in regionRemove)
                        {
                            _ipdRegionRepository.Remove(item);
                        }
                        string[] regionList = entityIPD.RegionIds.Split(",");
                        if (regionList.Length > 0)
                        {
                            for (int i = 0; i < regionList.Length; i++)
                            {
                                PidfIpdRegion pidfIpdRegion;
                                RegionEntity regionEntity = new RegionEntity();
                                regionEntity.RegionId = Convert.ToInt32(regionList[i]);
                                pidfIpdRegion = _mapperFactory.Get<RegionEntity, PidfIpdRegion>(regionEntity);
                                pidfIpdRegion.Ipdid = objIPD.Ipdid;
                                pidfIpdRegion.CreatedDate = DateTime.Now;
                                _ipdRegionRepository.AddAsync(pidfIpdRegion);
                            }
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                    if (!string.IsNullOrEmpty(entityIPD.CountryIds))
                    {
                        var countryRemove = _ipdCountryRepository.GetAllQuery().Where(x => x.Ipdid == entityIPD.IPDID);
                        foreach (var item in countryRemove)
                        {
                            _ipdCountryRepository.Remove(item);
                        }
                        string[] countryList = entityIPD.CountryIds.Split(",");
                        if (countryList.Length > 0)
                        {
                            for (int i = 0; i < countryList.Length; i++)
                            {
                                PidfIpdCountry pidfIpdCountry;
                                MasterCountry countryEntity = new MasterCountry();
                                countryEntity.CountryId = Convert.ToInt32(countryList[i]);
                                pidfIpdCountry = _mapperFactory.Get<MasterCountry, PidfIpdCountry>(countryEntity);
                                pidfIpdCountry.Ipdid = objIPD.Ipdid;
                                pidfIpdCountry.CreatedDate = DateTime.Now;
                                _ipdCountryRepository.AddAsync(pidfIpdCountry);
                            }
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                    if (objIPD.Pidfid == 0)
                        return DBOperation.Error;

                    var isSuccess = await _auditLogService.CreateAuditLog<IPDEntity>(Utility.Audit.AuditActionType.Update,
                       ModuleEnum.IPD, oldIPDFEntity, entityIPD, Convert.ToInt32(entityIPD.IPDID));
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                oldIPDFEntity = _mapperFactory.Get<PidfIpd, IPDEntity>(new PidfIpd { });

                objIPD = _mapperFactory.Get<IPDEntity, PidfIpd>(entityIPD);
                oldIPDFEntity.StatusId = entityIPD.StatusId;
                oldIPDFEntity.LogInId = entityIPD.LogInId;
                oldIPDFEntity.SaveType = entityIPD.SaveType;
                oldIPDFEntity.ProjectName = entityIPD.ProjectName;
                objIPD.CreatedDate = DateTime.Now;
                _repository.AddAsync(objIPD);

                await _unitOfWork.SaveChangesAsync();

                var id = objIPD.Ipdid;

                if (entityIPD.pidf_IPD_PatentDetailsEntities != null && entityIPD.pidf_IPD_PatentDetailsEntities.Count() > 0)
                {
                    oldIPDFEntity.pidf_IPD_PatentDetailsEntities = new List<PIDF_IPD_PatentDetailsEntity>();
                    foreach (var item in entityIPD.pidf_IPD_PatentDetailsEntities)
                    {
                        PidfIpdPatentDetail pidf_ipd_PDetail;
                        item.IPDID = id;
                        pidf_ipd_PDetail = _mapperFactory.Get<PIDF_IPD_PatentDetailsEntity, PidfIpdPatentDetail>(item);
                        pidf_ipd_PDetail.Ipdid = id;
                        pidf_ipd_PDetail.PatentType = (short?)IPDPatenDetailsType.PatenDetailsForFormulation;
                        _ipdParentRepository.AddAsync(pidf_ipd_PDetail);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
                //For PAtent Details API
                if (entityIPD.pidf_IPD_PatentDetailsEntitiesAPI != null && entityIPD.pidf_IPD_PatentDetailsEntitiesAPI.Count() > 0)
                {
                    oldIPDFEntity.pidf_IPD_PatentDetailsEntitiesAPI = new List<PIDF_IPD_PatentDetailsEntity>();
                    foreach (var item in entityIPD.pidf_IPD_PatentDetailsEntitiesAPI)
                    {
                        PidfIpdPatentDetail pidf_ipd_PDetail;
                        item.IPDID = id;
                        pidf_ipd_PDetail = _mapperFactory.Get<PIDF_IPD_PatentDetailsEntity, PidfIpdPatentDetail>(item);
                        pidf_ipd_PDetail.Ipdid = id;
                        pidf_ipd_PDetail.PatentType = (short?)IPDPatenDetailsType.PatientDetailsForAPI;
                        _ipdParentRepository.AddAsync(pidf_ipd_PDetail);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
                if (!string.IsNullOrEmpty(entityIPD.RegionIds))
                {
                    var regionRemove = _ipdRegionRepository.GetAllQuery().Where(x => x.Ipdid == entityIPD.IPDID);
                    foreach (var item in regionRemove)
                    {
                        _ipdRegionRepository.Remove(item);
                    }
                    string[] regionList = entityIPD.RegionIds.Split(",");
                    if (regionList.Length > 0)
                    {
                        for (int i = 0; i < regionList.Length; i++)
                        {
                            PidfIpdRegion pidfIpdRegion;
                            RegionEntity regionEntity = new RegionEntity();
                            regionEntity.RegionId = Convert.ToInt32(regionList[i]);
                            pidfIpdRegion = _mapperFactory.Get<RegionEntity, PidfIpdRegion>(regionEntity);
                            pidfIpdRegion.Ipdid = objIPD.Ipdid;
                            pidfIpdRegion.CreatedDate = DateTime.Now;
                            _ipdRegionRepository.AddAsync(pidfIpdRegion);
                        }
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                if (!string.IsNullOrEmpty(entityIPD.CountryIds))
                {
                    var countryRemove = _ipdCountryRepository.GetAllQuery().Where(x => x.Ipdid == entityIPD.IPDID);
                    foreach (var item in countryRemove)
                    {
                        _ipdCountryRepository.Remove(item);
                    }
                    string[] countryList = entityIPD.CountryIds.Split(",");
                    if (countryList.Length > 0)
                    {
                        for (int i = 0; i < countryList.Length; i++)
                        {
                            PidfIpdCountry pidfIpdCountry;
                            MasterCountry countryEntity = new MasterCountry();
                            countryEntity.CountryId = Convert.ToInt32(countryList[i]);
                            pidfIpdCountry = _mapperFactory.Get<MasterCountry, PidfIpdCountry>(countryEntity);
                            pidfIpdCountry.Ipdid = objIPD.Ipdid;
                            pidfIpdCountry.CreatedDate = DateTime.Now;
                            _ipdCountryRepository.AddAsync(pidfIpdCountry);
                        }
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                if (objIPD.Ipdid == 0)
                    return DBOperation.Error;
            }
            var loggedInUser = _helper.GetLoggedInUser();
            await _auditLogService.UpdatePIDFStatusCommon(entityIPD.PIDFID, (int)entityIPD.StatusId, _helper.GetLoggedInUser().UserId);
            await _notificationService.CreateNotification(entityIPD.PIDFID, (int)entityIPD.StatusId, string.Empty, string.Empty, loggedInUser.UserId);
            return DBOperation.Success;
        }

        public async Task<IPDEntity> GetIPDFormData(long pidfId, int buid)
        {
            Expression<Func<PidfIpd, bool>> expr = (buid == -1) ? u => u.Pidfid == pidfId : u => u.BusinessUnitId == buid && u.Pidfid == pidfId;

            var dynamicObj = (DataTable) await GetCountryByIsInterestedCountry(buid.ToString(), pidfId.ToString()); // Business ID hardcoded to 1
            List<MasterCountryEntity> CountryListForPatentDetails = dynamicObj.DataTableToList<MasterCountryEntity>();
            //data.pidf_IPD_PatentDetailsCountries = CountryListForPatentDetails;
            List<PIDF_IPD_PatentDetailsEntity> objPatentDetails = new();
            dynamic objData = (dynamic)await _repository.FindAllAsync(expr);
            IPDEntity data = new IPDEntity();
            if (objData != null && objData.Count > 0)
            {
                data = _mapperFactory.Get<PidfIpd, IPDEntity>(objData[0]);
                data.pidf_IPD_PatentDetailsEntities = _mapperFactory.GetList<PidfIpdPatentDetail, PIDF_IPD_PatentDetailsEntity>(_ipdParentRepository.GetAllQuery().Where(x => x.Ipdid == data.IPDID && x.PatentType == (short?)IPDPatenDetailsType.PatenDetailsForFormulation).ToList());
                data.pidf_IPD_PatentDetailsEntitiesAPI = _mapperFactory.GetList<PidfIpdPatentDetail, PIDF_IPD_PatentDetailsEntity>(_ipdParentRepository.GetAllQuery().Where(x => x.Ipdid == data.IPDID && x.PatentType == (short?)IPDPatenDetailsType.PatientDetailsForAPI).ToList());
                data.RegionIds = string.Join(",", _ipdRegionRepository.GetAllQuery().Where(x => x.Ipdid == data.IPDID).Select(x => x.RegionId.ToString()));
                data.RegionId = data.RegionIds;
                data.CountryIds = string.Join(",", _ipdCountryRepository.GetAllQuery().Where(x => x.Ipdid == data.IPDID).Select(x => x.CountryId.ToString()));
                data.CountryId = data.RegionIds;


            }
            if (CountryListForPatentDetails != null && CountryListForPatentDetails.Count > 0)
            {
                foreach (var country in CountryListForPatentDetails)
                {
                    var listitem = new PIDF_IPD_PatentDetailsEntity();
                    listitem.CountryId = country.CountryId;
                    objPatentDetails.Add(listitem);
                }
            }
            if(data.pidf_IPD_PatentDetailsEntities==null || data.pidf_IPD_PatentDetailsEntities.Count == 0)
            {
                data.pidf_IPD_PatentDetailsEntities = objPatentDetails;
            }
            if (data.pidf_IPD_PatentDetailsEntitiesAPI == null || data.pidf_IPD_PatentDetailsEntitiesAPI.Count == 0)
            {
                data.pidf_IPD_PatentDetailsEntitiesAPI = objPatentDetails;
            }

            data.MasterBusinessUnitEntities = _mapperFactory.GetList<MasterBusinessUnit, MasterBusinessUnitEntity>
                (_businessUnitrepository.GetAllQuery().Where(xx => xx.IsActive).ToList());

            Pidf objPidf = await _pidfrepository.GetAsync(pidfId);
            data.StatusId = objPidf.StatusId;
            
            return data;
        }

        public async Task<IEnumerable<dynamic>> GetAllRegion(int userId)
        {
            var dataRegion = _mapperFactory.GetList<MasterRegion, RegionEntity>(await _regionRepository.GetAllAsync(x => x.IsActive == true));
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
            var loggedinUser = _helper.GetLoggedInUser();

            string[] regionList = regionIds.Split(",");
            if (regionList.Length > 0)
            {
                int[] intIDs = new int[regionList.Length];

                for (int i = 0; i < regionList.Length; i++)
                {
                    intIDs[i] = Convert.ToInt32(regionList[i]);
                }

                var dataCountry = _countryService.GetAll().Result.ToList();
                var userCountrymapping = _masterUserCountryMappingrepository.GetAllQuery().Where(x => x.UserId == loggedinUser.UserId).ToList();

                var dataRegionCountry = _mapperFactory.GetList<MasterRegionCountryMapping, MasterRegionCountryMapping>(await _userRegionCountryRepository.FindAllAsync(xx => intIDs.Contains(xx.RegionId)));

                if (dataCountry.Any())
                {
                    var countryList = (from p in dataCountry
                                       join m in dataRegionCountry on p.CountryId equals m.CountryId
                                       join u in userCountrymapping on p.CountryId equals u.CountryId

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

        public async Task<List<MasterCountryEntity>> GetAllCountryListForPatentDetails(int BussinessUnitID)
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@BUID", BussinessUnitID)
            };
            var PIDFList = await _repository.GetBySP("stp_npd_GetIpdPatentDetailsBUssinessUnitWiseCountryList", System.Data.CommandType.StoredProcedure, osqlParameter);

            var TotalRecord = (PIDFList != null && PIDFList.Rows.Count > 0 ? Convert.ToInt32(PIDFList.Rows[0]["TotalRecord"]) : 0);
            var TotalCount = (PIDFList != null && PIDFList.Rows.Count > 0 ? Convert.ToInt32(PIDFList.Rows[0]["TotalCount"]) : 0);

            List<MasterCountryEntity> objList = PIDFList.DataTableToList<MasterCountryEntity>();            

            return objList;
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
                    await _notificationService.CreateNotification(objPidf.Pidfid, (int)objPidf.StatusId, string.Empty, string.Empty, (int)objPidf.CreatedBy);
                }
                var isSuccess = await _auditLogService.CreateAuditLog<EntryApproveRej>(Utility.Audit.AuditActionType.Update,
                   Utility.Enums.ModuleEnum.IPD, oApprRej, oApprRej, 0);

                return DBOperation.Success;
            }
            else
            {
                return DBOperation.NotFound;
            }
        }

        //public async Task<DBOperation> Medical(PIDFMedicalViewModel medicalModel, IFormFileCollection files, string path)
        //{
        //	PidfMedical objPIDFMedical;
        //	PidfMedicalFile objPIDFMedicalFile;
        //	PIDFMedicalViewModel oldPIDFFEntity;
        //	if (medicalModel.PidfmedicalId > 0)
        //	{
        //		objPIDFMedical = await _pidfMedicalrepository.GetAsync(medicalModel.PidfmedicalId);
        //		if (objPIDFMedical != null)
        //		{
        //			int i = 0;
        //			oldPIDFFEntity = _mapperFactory.Get<PidfMedical, PIDFMedicalViewModel>(objPIDFMedical);
        //			objPIDFMedical = _mapperFactory.Get<PIDFMedicalViewModel, PidfMedical>(medicalModel);
        //			if (files.Count() != 0)
        //			{
        //				string us = FileValidation(files[i]);
        //				if (us == null)
        //				{
        //					_pidfMedicalrepository.UpdateAsync(objPIDFMedical);
        //					await _unitOfWork.SaveChangesAsync();
        //				}
        //				else
        //				{
        //					return DBOperation.InvalidFile;
        //				}
        //			}
        //			else if (files.Count() == 0 && medicalModel.FileName.Length != 0)
        //			{
        //				_pidfMedicalrepository.UpdateAsync(objPIDFMedical);
        //				await _unitOfWork.SaveChangesAsync();
        //			}
        //			else
        //			{
        //				return DBOperation.Error;
        //			}
        //			var MedicalFile = _pidfMedicalFilerepository.GetAllQuery().Where(x => x.PidfmedicalId == medicalModel.PidfmedicalId).ToList();
        //			foreach (var item in MedicalFile)
        //			{
        //				if (medicalModel.FileName.Length != 0 && i < medicalModel.FileName.Count())
        //				{
        //					var uniqueFileName = Path.GetFileNameWithoutExtension(medicalModel.FileName[i])
        //					   + Guid.NewGuid().ToString().Substring(0, 4)
        //					   + Path.GetExtension(medicalModel.FileName[i]);
        //					PidfMedicalFile medicalFiles = new PidfMedicalFile
        //					{
        //						FileName = uniqueFileName,
        //						CreatedDate = DateTime.Now,
        //						PidfmedicalId = Convert.ToInt64(objPIDFMedical.PidfmedicalId),
        //						PidfmedicalFileId = Convert.ToInt64(item.PidfmedicalFileId),
        //						CreatedBy = (int)objPIDFMedical.CreatedBy,
        //					};
        //					var fullPath = path + "\\" + item.FileName;
        //					var itmFileName = "Medical\\" + item.FileName;
        //					if (!medicalModel.FileName.Contains(itmFileName))
        //					{
        //						if (System.IO.File.Exists(fullPath))
        //						{
        //							System.IO.File.Delete(fullPath);
        //						}
        //						PidfMedicalFile medicalFile = new PidfMedicalFile
        //						{
        //							PidfmedicalFileId = Convert.ToInt64(item.PidfmedicalFileId),
        //						};
        //						_pidfMedicalFilerepository.Remove(medicalFile);
        //						if (files.Count() != 0)
        //						{
        //							string us = FileValidation(files[i]);
        //							if (us == null)
        //							{
        //								await FileUpload(files[i], path, uniqueFileName);
        //								_pidfMedicalFilerepository.UpdateAsync(medicalFiles);
        //							}
        //							else
        //							{
        //								return DBOperation.InvalidFile;

        //							}
        //						}

        //					}
        //					i++;
        //				}
        //				else if (medicalModel.FileName.Length != 0)
        //				{
        //					PidfMedicalFile medicalFiles = new PidfMedicalFile
        //					{
        //						PidfmedicalFileId = Convert.ToInt64(item.PidfmedicalFileId),
        //					};
        //					//var file = item.FileName.Substring(7);
        //					var fullPath = path + "\\" + item.FileName;

        //					if (System.IO.File.Exists(fullPath))
        //					{
        //						System.IO.File.Delete(fullPath);
        //					}
        //					_pidfMedicalFilerepository.Remove(medicalFiles);
        //					i++;
        //				}
        //			}

        //			if (medicalModel.FileName.Length != 0 && i < medicalModel.FileName.Count())
        //			{
        //				foreach (var filename in i != 0 ? medicalModel.FileName.Skip(i) : medicalModel.FileName)
        //				{
        //					var uniqueFileName = Path.GetFileNameWithoutExtension(medicalModel.FileName[i])
        //					   + Guid.NewGuid().ToString().Substring(0, 4)
        //					   + Path.GetExtension(medicalModel.FileName[i]);
        //					PidfMedicalFile medicalFiles = new PidfMedicalFile
        //					{
        //						FileName = uniqueFileName,
        //						CreatedDate = DateTime.Now,
        //						PidfmedicalId = Convert.ToInt64(objPIDFMedical.PidfmedicalId),
        //						CreatedBy = (int)objPIDFMedical.CreatedBy,
        //					};
        //					if (files.Count() != 0)
        //					{
        //						string us = FileValidation(files[i]);
        //						if (us == null)
        //						{
        //							await FileUpload(files[i], path, uniqueFileName);
        //							_pidfMedicalFilerepository.AddAsync(medicalFiles);
        //						}
        //						else
        //						{
        //							return DBOperation.InvalidFile;
        //						}
        //					}
        //					i++;
        //				}
        //			}
        //			//status update in PIDF
        //			await _auditLogService.UpdatePIDFStatusCommon(medicalModel.Pidfid, (int)Master_PIDFStatus.MedicalSubmitted, medicalModel.CreatedBy);
        //			//test to update notification
        //                  await _notificationService.UpdateNotification(13, "testTitleUpdate", "testDescriptionUpdate", medicalModel.CreatedBy);

        //                  var isSuccess = await _auditLogService.CreateAuditLog<PIDFMedicalViewModel>(medicalModel.PidfmedicalId > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
        //				   Utility.Enums.ModuleEnum.Medical, oldPIDFFEntity, medicalModel, Convert.ToInt32(objPIDFMedical.PidfmedicalId));
        //			return DBOperation.Success;
        //		}
        //		else
        //		{
        //			return DBOperation.NotFound;
        //		}
        //	}
        //	else if (medicalModel.PidfmedicalId == 0)
        //	{
        //		int i = 0;
        //		var medical = _mapperFactory.Get<PIDFMedicalViewModel, PidfMedical>(medicalModel);
        //		medical.MedicalOpinion = medicalModel.MedicalOpinion;
        //		medical.Remark = medicalModel.Remark;
        //		medical.CreatedDate = DateTime.Now;
        //		if (files.Count() != 0)
        //		{
        //			string us = FileValidation(files[i]);
        //			if (us == null)
        //			{
        //				_pidfMedicalrepository.AddAsync(medical);
        //				await _unitOfWork.SaveChangesAsync();
        //			}
        //			else
        //			{
        //				return DBOperation.InvalidFile;
        //			}
        //		}
        //		else if (files.Count() == 0 && medicalModel.FileName != null)
        //		{
        //			_pidfMedicalrepository.AddAsync(medical);
        //			await _unitOfWork.SaveChangesAsync();
        //		}
        //		else
        //		{
        //			return DBOperation.Error;
        //		}

        //		//var medicalFile = _mapperFactory.Get<PIDFMedicalViewModel, PidfMedicalFile>(medicalModel);
        //		if (medicalModel.FileName != null)
        //		{
        //			foreach (var filename in medicalModel.FileName)
        //			{
        //				var uniqueFileName = Path.GetFileNameWithoutExtension(filename)
        //						   + Guid.NewGuid().ToString().Substring(0, 4)
        //						   + Path.GetExtension(filename);
        //				PidfMedicalFile medicalFiles = new PidfMedicalFile
        //				{
        //					FileName = uniqueFileName,
        //					CreatedDate = DateTime.Now,
        //					PidfmedicalId = Convert.ToInt64(medical.PidfmedicalId),
        //					CreatedBy = (int)medical.CreatedBy,
        //				};
        //				if (files.Count() != 0)
        //				{
        //					string us = FileValidation(files[i]);
        //					if (us == null)
        //					{
        //						await FileUpload(files[i], path, uniqueFileName);
        //						_pidfMedicalFilerepository.AddAsync(medicalFiles);
        //					}
        //					else
        //					{
        //						return DBOperation.InvalidFile;
        //					}
        //				}
        //				i++;
        //			}
        //		}
        //		//Status Update in PIDF
        //              await _auditLogService.UpdatePIDFStatusCommon(medicalModel.Pidfid, (int)Master_PIDFStatus.MedicalSubmitted, medicalModel.CreatedBy);

        //              //test To create notification
        //              await _notificationService.CreateNotification((int)medicalModel.Pidfid, (int)Master_PIDFStatus.MedicalSubmitted, "testTitleCreate", "testDescriptionCreate", medicalModel.CreatedBy);

        //              return DBOperation.Success;
        //	}

        //	else
        //	{
        //		return DBOperation.Error;
        //	}

        //}

        public async Task FileUpload(IFormFile files, string path, string uniqueFileName)
        {
            if (files != null)
            {
                //     var uniqueFileName = Path.GetFileNameWithoutExtension(file.FileName)
                //+ Guid.NewGuid().ToString().Substring(0, 4)
                //+ Path.GetExtension(file.FileName);
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

        public async Task<PIDFMedicalViewModel> GetPIDFMedicalData(long pidfId)
        {
            Expression<Func<PidfMedical, bool>> expr = u => u.Pidfid == pidfId;
            dynamic objData = (dynamic)await _pidfMedicalrepository.FindAllAsync(expr);
            PIDFMedicalViewModel data = new PIDFMedicalViewModel();
            if (objData != null && objData.Count > 0)
            {
                data = _mapperFactory.Get<PidfMedical, PIDFMedicalViewModel>(objData[0]);
                var medicalFileData = _pidfMedicalFilerepository.GetAllQuery().Where(x => x.PidfmedicalId == data.PidfmedicalId).ToList();
                int i = 0;
                data.FileName = new string[medicalFileData.Count];
                foreach (var item in medicalFileData)
                {
                    data.PidfmedicalId = item.PidfmedicalId;
                    data.PidfmedicalFileId = item.PidfmedicalFileId;
                    data.FileName[i] = item.FileName;
                    i++;
                }
            }
            return data;
        }

        public async Task<dynamic> GetCountryByBussinessUnitIds(string BUId)
        {
            dynamic DropdownObjects = new ExpandoObject();
            SqlParameter[] osqlParameter = {
                new SqlParameter("@BUId", BUId)
            };
            DataSet dsDropdownOptions = await _repository.GetDataSetBySP("stp_npd_GetIPDPatentDetailsCountryList", System.Data.CommandType.StoredProcedure, osqlParameter);

            DropdownObjects = dsDropdownOptions.Tables[0];

            return DropdownObjects;
        }
        public async Task<dynamic> GetCountryByIsInterestedCountry(string BUId,string PidfId)
        {
            dynamic DropdownObjects = new ExpandoObject();
            SqlParameter[] osqlParameter = {
                new SqlParameter("@BUId", BUId),
                new SqlParameter("@PIDFID", PidfId)
            };
            DataSet dsDropdownOptions = await _repository.GetDataSetBySP("stp_npd_GetCountryListByIsInterested", System.Data.CommandType.StoredProcedure, osqlParameter);

            DropdownObjects = dsDropdownOptions.Tables[0];

            return DropdownObjects;
        }

        public async Task<dynamic> GetPatentStrategy()
        {
            dynamic DropdownObjects = new ExpandoObject();

            DropdownObjects = await _patentStrategyrepository.GetAllAsync();

            return DropdownObjects;
        }
    }
}