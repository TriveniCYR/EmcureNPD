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
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using static EmcureNPD.Utility.Enums.GeneralEnum;
using Microsoft.Extensions.Configuration;

namespace EmcureNPD.Business.Core.Implementation
{
    public class PIDFormService : IPIDFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IMasterBusinessUnitService _businessUnitService;
        private readonly IMasterCountryService _countryService;
        private readonly IMasterAuditLogService _auditLogService;
        private readonly IConfiguration _configuration;

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
        public PIDFormService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IMasterOralService oralService, IMasterUnitofMeasurementService unitofMeasurementService, IMasterDosageFormService dosageFormService, IMasterPackagingTypeService packagingTypeService, IMasterBusinessUnitService businessUnitService, IMasterCountryService countryService, IMasterAuditLogService auditLogService, IConfiguration configuration)
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
            _pidfMedicalrepository = unitOfWork.GetRepository<PidfMedical>();
            _pidfMedicalFilerepository = unitOfWork.GetRepository<PidfMedicalFile>();
            _configuration = configuration;

        }

        public async Task<PIDFormEntity> FillDropdown()
        {
            var PIDForm = new PIDFormEntity
            {
                MasterBusinessUnitEntities = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList(),
                MasterCountries = _countryService.GetAll().Result.Where(xx => xx.IsActive).ToList(),
            };
            return PIDForm;
        }
        public async Task<DBOperation> AddUpdateIPD(PIDFormEntity entityIPD)
        {
            PidfIpd objIPD;
            PIDFormEntity oldIPDFEntity;
            if (entityIPD.IPDID > 0)
            {
                objIPD = await _repository.GetAsync(entityIPD.IPDID);
                if (objIPD != null)
                {
                    entityIPD.ModifyBy = Convert.ToInt32(entityIPD.CreatedBy);
                    objIPD.ModifyBy = Convert.ToInt32(entityIPD.CreatedBy);
                    objIPD.ModifyDate = DateTime.Now;
                    oldIPDFEntity = _mapperFactory.Get<PidfIpd, PIDFormEntity>(objIPD);
                    oldIPDFEntity.StatusId = entityIPD.StatusId;
                    oldIPDFEntity.LogInId = entityIPD.LogInId;
                    oldIPDFEntity.SaveType = entityIPD.SaveType;
                    oldIPDFEntity.ProjectName = entityIPD.ProjectName;

                    objIPD = _mapperFactory.Get<PIDFormEntity, PidfIpd>(entityIPD);

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
                            PidfIpdPatentDetail pidf_ipd_PDetail;
                            item.IPDID = objIPD.Ipdid;
                            pidf_ipd_PDetail = _mapperFactory.Get<PIDF_IPD_PatentDetailsEntity, PidfIpdPatentDetail>(item);

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

                    Pidf objPidf = await _pidfrepository.GetAsync(entityIPD.PIDFID);
                    objPidf.LastStatusId = objPidf.StatusId;
                    objPidf.StatusId = Convert.ToInt32(entityIPD.StatusId);
                    _pidfrepository.UpdateAsync(objPidf);
                    await _unitOfWork.SaveChangesAsync();

                    if (objIPD.Pidfid == 0)
                        return DBOperation.Error;

                    var isSuccess = await _auditLogService.CreateAuditLog<PIDFormEntity>(entityIPD.IPDID > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
                       Utility.Enums.ModuleEnum.IPD, oldIPDFEntity, entityIPD, Convert.ToInt32(entityIPD.IPDID));

                    return DBOperation.Success;
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                oldIPDFEntity = _mapperFactory.Get<PidfIpd, PIDFormEntity>(new PidfIpd { });

                objIPD = _mapperFactory.Get<PIDFormEntity, PidfIpd>(entityIPD);
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

                Pidf objPidf = await _pidfrepository.GetAsync(entityIPD.PIDFID);
                objPidf.LastStatusId = objPidf.StatusId;
                objPidf.StatusId = Convert.ToInt32(entityIPD.StatusId);
                _pidfrepository.UpdateAsync(objPidf);
                await _unitOfWork.SaveChangesAsync();
                if (objIPD.Ipdid == 0)
                    return DBOperation.Error;

                var isSuccess = await _auditLogService.CreateAuditLog<PIDFormEntity>(entityIPD.IPDID > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
                       Utility.Enums.ModuleEnum.IPD, oldIPDFEntity, entityIPD, Convert.ToInt32(objIPD.Ipdid));


                return DBOperation.Success;
            }
        }

        public async Task<PIDFormEntity> GetIPDFormData(long pidfId, int buid)
        {

            //var data = _mapperFactory.Get<Pidfipd, PIDFormEntity>(await _repository.GetAsync(id));
            Expression<Func<PidfIpd, bool>> expr = u => u.BusinessUnitId == buid && u.Pidfid == pidfId;
            dynamic objData = (dynamic)await _repository.FindAllAsync(expr);
            PIDFormEntity data = new PIDFormEntity();
            if (objData != null && objData.Count > 0)
            {
                data = _mapperFactory.Get<PidfIpd, PIDFormEntity>(objData[0]);
                data.pidf_IPD_PatentDetailsEntities = _mapperFactory.GetList<PidfIpdPatentDetail, PIDF_IPD_PatentDetailsEntity>(_ipdParentRepository.GetAll().Where(x => x.Ipdid == data.IPDID).ToList());
                data.RegionIds = string.Join(",", _ipdRegionRepository.GetAllQuery().Where(x => x.Ipdid == data.IPDID).Select(x => x.RegionId.ToString()));
                data.RegionId = data.RegionIds;
                data.CountryIds = string.Join(",", _ipdCountryRepository.GetAllQuery().Where(x => x.Ipdid == data.IPDID).Select(x => x.CountryId.ToString()));
                data.CountryId = data.RegionIds;
            }
            data.MasterBusinessUnitEntities = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList();


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


                var dataCountry = _countryService.GetAll().Result.Where(xx => xx.IsActive).ToList();

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
                var isSuccess = await _auditLogService.CreateAuditLog<EntryApproveRej>(Utility.Audit.AuditActionType.Update,
                   Utility.Enums.ModuleEnum.IPD, oApprRej, oApprRej, 0);

                return DBOperation.Success;
            }
            else
            {
                return DBOperation.NotFound;
            }


        }

        public async Task<DBOperation> Medical(PIDFMedicalViewModel medicalModel)
        {
            PidfMedical objPIDFMedical;
            PidfMedicalFile objPIDFMedicalFile;
            PIDFMedicalViewModel oldPIDFFEntity;
            if (medicalModel.PidfmedicalId > 0)
            {
                objPIDFMedical = await _pidfMedicalrepository.GetAsync(medicalModel.PidfmedicalId);
                if (objPIDFMedical != null)
                {
                    objPIDFMedical = _mapperFactory.Get<PIDFMedicalViewModel, PidfMedical>(medicalModel);
                    _pidfMedicalrepository.UpdateAsync(objPIDFMedical);
                    await _unitOfWork.SaveChangesAsync();
                    var MedicalFile = _pidfMedicalFilerepository.GetAll().Where(x => x.PidfmedicalId == medicalModel.PidfmedicalId).ToList();
                    int i = 0;
                    foreach (var item in MedicalFile)
                    {
                        PidfMedicalFile medicalFiles = new PidfMedicalFile
                        {
                            FileName = medicalModel.FileName[i],
                            CreatedDate = DateTime.Now,
                            PidfmedicalId = Convert.ToInt64(objPIDFMedical.PidfmedicalId),
                            PidfmedicalFileId = Convert.ToInt64(item.PidfmedicalFileId),
                            CreatedBy = (int)objPIDFMedical.CreatedBy,
                        };
                        _pidfMedicalFilerepository.UpdateAsync(medicalFiles);
                        i++;
                    }
                    if (i < medicalModel.FileName.Count())
                    {
                        foreach (var filename in medicalModel.FileName.Skip(i))
                        {
                            PidfMedicalFile medicalFiles = new PidfMedicalFile
                            {
                                FileName = filename,
                                CreatedDate = DateTime.Now,
                                PidfmedicalId = Convert.ToInt64(objPIDFMedical.PidfmedicalId),
                                CreatedBy = (int)objPIDFMedical.CreatedBy,
                            };
                            _pidfMedicalFilerepository.AddAsync(medicalFiles);
                        }
                    }
                    await _unitOfWork.SaveChangesAsync();
                    return DBOperation.Success;
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var medical = _mapperFactory.Get<PIDFMedicalViewModel, PidfMedical>(medicalModel);
                medical.MedicalOpinion = medicalModel.MedicalOpinion;
                medical.Remark = medicalModel.Remark;
                medical.CreatedDate = DateTime.Now;
                _pidfMedicalrepository.AddAsync(medical);
                await _unitOfWork.SaveChangesAsync();


                //var medicalFile = _mapperFactory.Get<PIDFMedicalViewModel, PidfMedicalFile>(medicalModel);
                foreach (var filename in medicalModel.FileName)
                {
                    PidfMedicalFile medicalFiles = new PidfMedicalFile
                    {
                        FileName = filename,
                        CreatedDate = DateTime.Now,
                        PidfmedicalId = Convert.ToInt64(medical.PidfmedicalId),
                        CreatedBy = (int)medical.CreatedBy,
                    };
                    _pidfMedicalFilerepository.AddAsync(medicalFiles);
                }
                await _unitOfWork.SaveChangesAsync();

                return DBOperation.Success;
            }

        }

        public async Task FileUpload(IFormFileCollection files, string path)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    string us = FileValidation(file);
                    if (us == null)
                    {
                        var uniqueFileName = Path.GetFileNameWithoutExtension(file.FileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(file.FileName);
                        string uploadFolder = Path.Combine(path, "Uploads\\Medical");
                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }
                        var filePath = Path.Combine(uploadFolder, uniqueFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);

                        }
                    }
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
                var medicalFileData = _pidfMedicalFilerepository.GetAll().Where(x => x.PidfmedicalId == data.PidfmedicalId).ToList();
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
    }
}
