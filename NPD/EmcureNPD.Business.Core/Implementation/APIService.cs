﻿using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class APIService : IAPIService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IMasterProductTypeService _masterProductTypeService;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private IRepository<PidfApiIpd> _pidf_API_IPD_repository { get; set; }
        private IRepository<PidfApiRnD> _pidf_API_RnD_repository { get; set; }
        private IRepository<PidfApiCharter> _pidf_API_Charter_repository { get; set; }
        private IRepository<PidfApiMaster> _pidf_API_Master_repository { get; set; }
        private IRepository<PidfApiCharterTimelineInMonth> _pidf_API_TimelineInMonth_repository { get; set; }
        private readonly IMasterAuditLogService _auditLogService;
        private IRepository<Pidf> _repository { get; set; }
        private readonly IHelper _helper;
        private readonly IMasterCountryService _countryService;
        private IRepository<Pidf> _pidfrepository { get; set; }
        private readonly INotificationService _notificationService;
        private readonly IExceptionService _ExceptionService;
        private IRepository<MasterApiOutsource> _masterAPIOutsource { get; set; }
        private IRepository<PidfApiOutsourceDatum> _pIDFAPIOutsource { get; set; }
        private IRepository<MasterApiInhouse> _masterAPIInhouse { get; set; }
        private IRepository<PidfApiInhouse> _pIDFAPIInhouse { get; set; }
        public APIService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory,
            Microsoft.Extensions.Configuration.IConfiguration configuration, IMasterProductTypeService masterProductTypeService,
             INotificationService notificationService,
        IMasterCountryService countryService, IMasterAuditLogService auditLogService, IHelper helper, IExceptionService exceptionService)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<Pidf>();
            _pidf_API_IPD_repository = _unitOfWork.GetRepository<PidfApiIpd>();
            _pidf_API_RnD_repository = _unitOfWork.GetRepository<PidfApiRnD>();
            _pidf_API_Charter_repository = _unitOfWork.GetRepository<PidfApiCharter>();
            _pidf_API_Master_repository = _unitOfWork.GetRepository<PidfApiMaster>();
            _pidf_API_TimelineInMonth_repository = _unitOfWork.GetRepository<PidfApiCharterTimelineInMonth>();
            _masterProductTypeService = masterProductTypeService;
            _masterAPIOutsource = _unitOfWork.GetRepository<MasterApiOutsource>();
            _pIDFAPIOutsource = _unitOfWork.GetRepository<PidfApiOutsourceDatum>();
            _masterAPIInhouse = _unitOfWork.GetRepository<MasterApiInhouse>();
            _pIDFAPIInhouse = _unitOfWork.GetRepository<PidfApiInhouse>();
            _auditLogService = auditLogService;
            _configuration = configuration;
            _helper = helper;
            _countryService = countryService;
            _pidfrepository = unitOfWork.GetRepository<Pidf>();
            _notificationService = notificationService;
            _ExceptionService = exceptionService;
        }

        //------------Start------API_Functions_Kuldip--------------------------
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

        private List<DM> FillObjData<VM, DM>(List<VM> _vmObj)
        {
            var _objPidfApiCharterTimelineInMonth = new List<DM>();
            foreach (var obj in _vmObj)
            {
                var _objTimelineInMonths = _mapperFactory.Get<VM, DM>(obj);
                _objPidfApiCharterTimelineInMonth.Add(_objTimelineInMonths);
            }
            return _objPidfApiCharterTimelineInMonth;
        }

        private void RemoveChildDataAPICharter(long APICharterId)
        {
            PIDFAPICharterFormEntity _oCharterEntity = new PIDFAPICharterFormEntity();
            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFAPICharterId", APICharterId)
            };
            var dbresult = _pidf_API_Charter_repository.GetDataSetBySP("stp_npd_RemoveChildDataAPICharter",
                System.Data.CommandType.StoredProcedure, osqlParameter);
        }

        //---------------End Child Method-----------------------------------------------

        public async Task<PIDFAPIIPDFormEntity> GetAPIIPDFormData(long pidfId, string HostValue)
        {
            PIDFAPIIPDFormEntity _oApiIpdData = new PIDFAPIIPDFormEntity();
            var _oAPIIPD = await _pidf_API_IPD_repository.GetAsync(x => x.Pidfid == pidfId);
            if (_oAPIIPD != null)
            {
                string baseURL = HostValue + "/Uploads/PIDF/APIIPD";
                var fullPath = baseURL + "/" + _oAPIIPD.MarketDetailsFileName;
                _oApiIpdData.DrugsCategory = _oAPIIPD.DrugsCategory;
                _oApiIpdData.ProductTypeId = _oAPIIPD.ProductTypeId;
                _oApiIpdData.APIIPDDetailsFormID = _oAPIIPD.PidfApiIpdId;
                _oApiIpdData.ProductStrength = _oAPIIPD.ProductStrength;
                _oApiIpdData.MarketDetailsFileName = (Convert.ToString(_oAPIIPD.MarketDetailsFileName) == "") ? "" : fullPath;
                _oApiIpdData.Pidfid = _oAPIIPD.Pidfid.ToString();
            }
            //_oApiIpdData.MasterCountries = _countryService.GetAll().Result.ToList();
            Pidf objPidf = await _pidfrepository.GetAsync(pidfId);
            _oApiIpdData.StatusId = objPidf.StatusId;
            return _oApiIpdData;
        }

        public async Task<APIIPDEntity> GetIPDByPIDF(long pidfId)
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFId", pidfId)
            };
            DataSet dsIPDDetail = await _repository.GetDataSetBySP("stp_npd_GetIPDDetailByPIDF", System.Data.CommandType.StoredProcedure, osqlParameter);
            APIIPDEntity oAPIIPDEntity = new APIIPDEntity();
            oAPIIPDEntity.IPDList = dsIPDDetail.Tables[0].DataTableToList<IPDEntity>();
            oAPIIPDEntity.IPD_PatentDetailsList = dsIPDDetail.Tables[1].DataTableToList<PIDF_IPD_PatentDetailsEntity>();
            if (dsIPDDetail.Tables[2].Rows.Count > 0)
            {
                oAPIIPDEntity.ProjectName = Convert.ToString(dsIPDDetail.Tables[2].Rows[0]["ProjectName"]);
            }
            return oAPIIPDEntity;
        }

        public async Task<PIDFAPIRnDFormEntity> GetAPIRnDFormData(long pidfId, string HostValue)
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
                string baseURL = HostValue + "/Uploads/PIDF/APIIPD";
                var fullPath = baseURL + "/" + _oAPIIpd.MarketDetailsFileName;

                _oApiRnDData.DrugsCategory = _oAPIIpd.DrugsCategory;
                _oApiRnDData.ProductTypeId = (int)_oAPIIpd.ProductTypeId;
                _oApiRnDData.ProductStrength = _oAPIIpd.ProductStrength;
                _oApiRnDData.MarketDetailsFileName = (Convert.ToString(_oAPIIpd.MarketDetailsFileName) == "") ? "" : fullPath;
                var _objProductType = await _masterProductTypeService.GetById((int)_oAPIIpd.ProductTypeId);
                if (_objProductType != null)
                    _oApiRnDData.ProductType = _objProductType.ProductTypeName;
            }
            _oApiRnDData.MasterCountries = _countryService.GetAll().Result.ToList();
            _oApiRnDData.PIDFAPIInhouseEntities= await GetAPICharterDataByPIDF(pidfId);
            Pidf objPidf = await _pidfrepository.GetAsync(pidfId);
            _oApiRnDData.StatusId = objPidf.StatusId;
            return _oApiRnDData;
        }

        public async Task<PIDFAPICharterFormEntity> GetAPICharterFormData(long pidfId, short IsCharter)
        {
            PIDFAPICharterFormEntity _oCharterEntity = new PIDFAPICharterFormEntity();
            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFID", pidfId)
            };
            string Sp_Name = (IsCharter == 1) ? "stp_npd_GetPIDFAPICharterData" : "stp_npd_GetPIDFAPICharterSummaryData";
            var dbresult = await _pidf_API_Charter_repository.GetDataSetBySP(Sp_Name,
                System.Data.CommandType.StoredProcedure, osqlParameter);

            // dynamic _CharterObjects = new ExpandoObject();
            List<CharterObject> _CharterObjects = new List<CharterObject>();
            if (dbresult != null)
            {
                _oCharterEntity.TimelineInMonths = dbresult.Tables[1].DataTableToList<TimelineInMonths>();
                _oCharterEntity.AnalyticalDepartment = dbresult.Tables[2].DataTableToList<AnalyticalDepartment>();
                _oCharterEntity.PRDDepartment = dbresult.Tables[3].DataTableToList<PRDDepartment>();
                _oCharterEntity.CapitalOtherExpenditure = dbresult.Tables[4].DataTableToList<CapitalOtherExpenditure>();
                _oCharterEntity.ManhourEstimates = dbresult.Tables[5].DataTableToList<ManhourEstimates>();
                _oCharterEntity.HeadwiseBudget = dbresult.Tables[6].DataTableToList<HeadwiseBudget>();
                _oCharterEntity.ManagmentTeams = dbresult.Tables[7].DataTableToList<ManagmentTeam>();
              
                if (Sp_Name == "stp_npd_GetPIDFAPICharterSummaryData")
                {
                    _oCharterEntity.PIDFAPIInhouse = dbresult.Tables[8].DataTableToList<PIDFAPIInhouse>();
                }
            }

            if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
            {
                _CharterObjects = dbresult.Tables[0].DataTableToList<CharterObject>();
                if (_CharterObjects.Count > 0)
                {
                    _oCharterEntity.APIGroupLeader = _CharterObjects[0].APIGroupLeader;
                    _oCharterEntity.ManHourRates = Convert.ToString(_CharterObjects[0].ManHourRates);
                    _oCharterEntity.PIDFAPICharterFormID = _CharterObjects[0].PIDF_API_CharterId;
                    _oCharterEntity.ProjectComplexityId = _CharterObjects[0].ProjectComplexityId;

                    _oCharterEntity.ProjectName = _CharterObjects[0].ProjectName;
                    _oCharterEntity.Market = _CharterObjects[0].Market;
                    _oCharterEntity.ProjectInitiationDate = _CharterObjects[0].ProjectInitiationDate;
                    _oCharterEntity.ProjectEndDate = _CharterObjects[0].ProjectEndDate;
                  
                }
            }
            Pidf objPidf = await _pidfrepository.GetAsync(pidfId);
            _oCharterEntity.StatusId = objPidf.StatusId;

            return _oCharterEntity;
        }

        public async Task<PIDFAPICharterFormEntity> GetAPICharterSummaryFormData(long pidfId)
        {
            PIDFAPICharterFormEntity _oCharterEntity = new PIDFAPICharterFormEntity();
            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFID", pidfId)
            };
            var dbresult = await _pidf_API_Charter_repository.GetDataSetBySP("stp_npd_GetAPICharterSummaryData",
                System.Data.CommandType.StoredProcedure, osqlParameter);

            // dynamic _CharterObjects = new ExpandoObject();
            List<CharterObject> _CharterObjects = new List<CharterObject>();
            if (dbresult != null)
            {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                {
                    _CharterObjects = dbresult.Tables[0].DataTableToList<CharterObject>();
                    _oCharterEntity.TimelineInMonths = dbresult.Tables[1].DataTableToList<TimelineInMonths>();
                    _oCharterEntity.AnalyticalDepartment = dbresult.Tables[2].DataTableToList<AnalyticalDepartment>();
                    _oCharterEntity.PRDDepartment = dbresult.Tables[3].DataTableToList<PRDDepartment>();
                    _oCharterEntity.CapitalOtherExpenditure = dbresult.Tables[4].DataTableToList<CapitalOtherExpenditure>();
                    _oCharterEntity.ManhourEstimates = dbresult.Tables[5].DataTableToList<ManhourEstimates>();
                    _oCharterEntity.HeadwiseBudget = dbresult.Tables[6].DataTableToList<HeadwiseBudget>();
                   
                }
            }

            if (_CharterObjects.Count > 0)
            {
                _oCharterEntity.APIGroupLeader = _CharterObjects[0].APIGroupLeader;
                _oCharterEntity.ManHourRates = Convert.ToString(_CharterObjects[0].ManHourRates);
                _oCharterEntity.PIDFAPICharterFormID = _CharterObjects[0].PIDF_API_CharterId;
                _oCharterEntity.ProjectComplexityId = _CharterObjects[0].ProjectComplexityId;
            }
            Pidf objPidf = await _pidfrepository.GetAsync(pidfId);
            _oCharterEntity.StatusId = objPidf.StatusId;
            return _oCharterEntity;
        }

        public async Task<DBOperation> AddUpdateAPIIPD(IFormCollection _oAPIIPD_Form, string _webrootPath)
        {
            var loggedInUser = _helper.GetLoggedInUser();
            bool hasNewUploadFile = true;
            string uniqueFileName = "";
            

            _oAPIIPD_Form.TryGetValue("Data", out StringValues Data);
            dynamic jsonObject = JsonConvert.DeserializeObject(Data);
            var path = Path.Combine(_webrootPath, "Uploads\\PIDF\\APIIPD");

            bool IskeepLastFile = (jsonObject.MarketDetailsFileName == null || jsonObject.MarketDetailsFileName == "") ? false : true;

            if ((_oAPIIPD_Form.Files == null || _oAPIIPD_Form.Files.Count <= 0))
                hasNewUploadFile = false;
            else
                IskeepLastFile = false;

            if (hasNewUploadFile)
            {
                var MarketCGIFile = _oAPIIPD_Form.Files[0];
                uniqueFileName = Path.GetFileNameWithoutExtension(MarketCGIFile.FileName)
                                + Guid.NewGuid().ToString().Substring(0, 4)
                                + Path.GetExtension(MarketCGIFile.FileName);

                //var fullPath = path + "\\" + MarketCGIFile.FileName;
                //var itmFileName = "APIIPD\\" + MarketCGIFile.FileName;

                // ErrorMsg = FileValidation(MarketCGIFile);
            }

            PIDFAPIIPDFormEntity _oAPIIPD = new PIDFAPIIPDFormEntity();

            _oAPIIPD.APIIPDDetailsFormID = jsonObject.APIIPDDetailsFormID;
            _oAPIIPD.MarketDetailsFileName = uniqueFileName;
            _oAPIIPD.DrugsCategory = jsonObject.DrugsCategory;
            _oAPIIPD.ProductStrength = jsonObject.ProductStrength;
            _oAPIIPD.ProductTypeId = jsonObject.ProductTypeId;
            _oAPIIPD.Pidfid = jsonObject.Pidfid;
            _oAPIIPD.LoggedInUserId = jsonObject.LoggedInUserId;
            _oAPIIPD.SaveType = jsonObject.SaveType;
            var _APIIPDDBEntity = new PidfApiIpd();
            if (_oAPIIPD.APIIPDDetailsFormID > 0)
            {
                PidfApiIpd lastApiIpd = await _pidf_API_IPD_repository.GetAsync(x => x.PidfApiIpdId == _oAPIIPD.APIIPDDetailsFormID);

                if (lastApiIpd != null)
                {
                    var OldObjAPIIPD = _mapperFactory.Get<PidfApiIpd, PIDFAPIIPDFormEntity>(lastApiIpd);
                    if (!IskeepLastFile)
                    {
                        var exsistingFilePath = path + "\\" + lastApiIpd.MarketDetailsFileName;
                        if (System.IO.File.Exists(exsistingFilePath))
                        {
                            System.IO.File.Delete(exsistingFilePath);
                        }
                        if (hasNewUploadFile)
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
                    await _unitOfWork.SaveChangesAsync();

                    var isSuccess = await _auditLogService.CreateAuditLog(Utility.Audit.AuditActionType.Update,
                    Utility.Enums.ModuleEnum.APIManagement, OldObjAPIIPD, _oAPIIPD, (int)_oAPIIPD.APIIPDDetailsFormID);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                if (hasNewUploadFile)
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
                await _unitOfWork.SaveChangesAsync();
            }
            var _StatusID = (_oAPIIPD.SaveType == "Save") ? Master_PIDFStatus.APISubmitted : Master_PIDFStatus.APIInProgress;
            await _auditLogService.UpdatePIDFStatusCommon(long.Parse(_oAPIIPD.Pidfid), (int)_StatusID, _oAPIIPD.LoggedInUserId);
            await _notificationService.CreateNotification(long.Parse(_oAPIIPD.Pidfid), (int)_StatusID, string.Empty, string.Empty, _oAPIIPD.LoggedInUserId);
            return DBOperation.Success;
        }

        public async Task<DBOperation> AddUpdateAPIRnD(PIDFAPIRnDFormEntity _oAPIRnD)
        {
            //PIDFAPIIPDFormEntity _exsistingAPIRnD = new PIDFAPIIPDFormEntity();
            var loggedInUser = _helper.GetLoggedInUser();
            await InsertAPIInterestedUserData(_oAPIRnD.PIDFAPIInhouseEntities, int.Parse(_oAPIRnD.Pidfid));
            if (_oAPIRnD.PIDFAPIRnDFormID > 0)
            {
                var lastApiRnD = _pidf_API_RnD_repository.GetAllQuery().First(x => x.PidfApiRnDId == _oAPIRnD.PIDFAPIRnDFormID);
                if (lastApiRnD != null)
                {
                    var OldObjAPiRnD = _mapperFactory.Get<PidfApiRnD, PIDFAPIRnDFormEntity>(lastApiRnD);
                    OldObjAPiRnD.APITargetRMC_CCPC = lastApiRnD.ApitargetRmcCcpc;
                    OldObjAPiRnD.MarketID = lastApiRnD.MarketExtenstionId;

                    lastApiRnD.PlantQc = _oAPIRnD.PlantQC;
                    lastApiRnD.Development = _oAPIRnD.Development;
                    lastApiRnD.Total = _oAPIRnD.Total;
                    lastApiRnD.Exhibit = _oAPIRnD.Exhibit;
                    lastApiRnD.ScaleUp = _oAPIRnD.ScaleUp;
                    lastApiRnD.MarketExtenstionId = _oAPIRnD.MarketID;
                    lastApiRnD.SponsorBusinessPartner = _oAPIRnD.SponsorBusinessPartner;
                    lastApiRnD.ApimarketPrice = _oAPIRnD.APIMarketPrice;
                    lastApiRnD.ApitargetRmcCcpc = _oAPIRnD.APITargetRMC_CCPC;
                    lastApiRnD.Pidfid = long.Parse(_oAPIRnD.Pidfid);
                    lastApiRnD.ModifyBy = _oAPIRnD.LoggedInUserId;
                    lastApiRnD.ModifyDate = DateTime.Now;
                    _pidf_API_RnD_repository.UpdateAsync(lastApiRnD);

                    var isSuccess = await _auditLogService.CreateAuditLog(Utility.Audit.AuditActionType.Update,
                       Utility.Enums.ModuleEnum.APIManagement, OldObjAPiRnD, _oAPIRnD, (int)_oAPIRnD.PIDFAPIRnDFormID);
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
            var _StatusID = (_oAPIRnD.SaveType == "Save") ? Master_PIDFStatus.APISubmitted : Master_PIDFStatus.APIInProgress;
            await _auditLogService.UpdatePIDFStatusCommon(long.Parse(_oAPIRnD.Pidfid), (int)_StatusID, _oAPIRnD.LoggedInUserId);
            await _notificationService.CreateNotification(long.Parse(_oAPIRnD.Pidfid), (int)_StatusID, string.Empty, string.Empty, _oAPIRnD.LoggedInUserId);
            return DBOperation.Success;
        }

        public async Task<DBOperation> AddUpdateAPICharter(PIDFAPICharterFormEntity _oAPICharter)
        {
            var loggedInUser = _helper.GetLoggedInUser();
            var _objPidfApiCharterTimelineInMonth = FillObjData<TimelineInMonths, PidfApiCharterTimelineInMonth>(_oAPICharter.TimelineInMonths);
            var _objPidfApiCharterManhourEstimates = FillObjData<ManhourEstimates, PidfApiCharterManhourEstimate>(_oAPICharter.ManhourEstimates);
            var _objPidfApiCharterAnalyticalDepartment = FillObjData<AnalyticalDepartment, PidfApiCharterAnalyticalDepartment>(_oAPICharter.AnalyticalDepartment);

            var _objPidfApiCharterPRDDepartment = FillObjData<PRDDepartment, PidfApiCharterPrddepartment>(_oAPICharter.PRDDepartment);
            var _objPidfApiCharterCapitalOtherExpenditure = FillObjData<CapitalOtherExpenditure, PidfApiCharterCapitalOtherExpenditure>(_oAPICharter.CapitalOtherExpenditure);
            var _objPidfApiCharterHeadwiseBudget = FillObjData<HeadwiseBudget, PidfApiCharterHeadwiseBudget>(_oAPICharter.HeadwiseBudget);

            if (_oAPICharter.PIDFAPICharterFormID > 0)
            {
                var lastApiCharter = _pidf_API_Charter_repository.GetAllQuery().First(x => x.PidfApiCharterId == _oAPICharter.PIDFAPICharterFormID);
                if (lastApiCharter != null)
                {
                    var _oldObjAPIcharter = await GetAPICharterFormData(Int32.Parse(_oAPICharter.Pidfid), 1); // IsCharter = 1;

                    RemoveChildDataAPICharter(_oAPICharter.PIDFAPICharterFormID); // Remove child table data
                    lastApiCharter.PidfApiCharterTimelineInMonths = _objPidfApiCharterTimelineInMonth;
                    lastApiCharter.PidfApiCharterManhourEstimates = _objPidfApiCharterManhourEstimates;
                    lastApiCharter.PidfApiCharterAnalyticalDepartments = _objPidfApiCharterAnalyticalDepartment;

                    lastApiCharter.PidfApiCharterPrddepartments = _objPidfApiCharterPRDDepartment;
                    lastApiCharter.PidfApiCharterCapitalOtherExpenditures = _objPidfApiCharterCapitalOtherExpenditure;
                    lastApiCharter.PidfApiCharterHeadwiseBudgets = _objPidfApiCharterHeadwiseBudget;

                    _oAPICharter.ManHourRates = (Convert.ToString(_oAPICharter.ManHourRates) == "" || _oAPICharter.ManHourRates == null) ? "0" : _oAPICharter.ManHourRates;
                    lastApiCharter.ManHourRates = _oAPICharter.ManHourRates;
                    lastApiCharter.ApigroupLeader = _oAPICharter.APIGroupLeader;
                    lastApiCharter.ProjectComplexityId = _oAPICharter.ProjectComplexityId;

                    lastApiCharter.Pidfid = long.Parse(_oAPICharter.Pidfid);
                    lastApiCharter.ModifyBy = _oAPICharter.LoggedInUserId;
                    lastApiCharter.ModifyDate = DateTime.Now;
                    _pidf_API_Charter_repository.UpdateAsync(lastApiCharter);
                    await _unitOfWork.SaveChangesAsync();
                    //Implement AuditLog
                    var isSuccess = await _auditLogService.CreateAuditLog(Utility.Audit.AuditActionType.Update,
                    ModuleEnum.APIManagement, _oldObjAPIcharter, _oAPICharter, (int)_oAPICharter.PIDFAPICharterFormID);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var _oDBApiCharter = new PidfApiCharter();

                _oDBApiCharter.PidfApiCharterTimelineInMonths = _objPidfApiCharterTimelineInMonth;
                _oDBApiCharter.PidfApiCharterManhourEstimates = _objPidfApiCharterManhourEstimates;
                _oDBApiCharter.PidfApiCharterAnalyticalDepartments = _objPidfApiCharterAnalyticalDepartment;
                _oDBApiCharter.PidfApiCharterPrddepartments = _objPidfApiCharterPRDDepartment;
                _oDBApiCharter.PidfApiCharterCapitalOtherExpenditures = _objPidfApiCharterCapitalOtherExpenditure;
                _oDBApiCharter.PidfApiCharterHeadwiseBudgets = _objPidfApiCharterHeadwiseBudget;

                _oAPICharter.ManHourRates = (Convert.ToString(_oAPICharter.ManHourRates) == "" || _oAPICharter.ManHourRates == null) ? "0" : _oAPICharter.ManHourRates;
                _oDBApiCharter.ManHourRates = _oAPICharter.ManHourRates;
                _oDBApiCharter.ApigroupLeader = _oAPICharter.APIGroupLeader;
                _oDBApiCharter.ProjectComplexityId = _oAPICharter.ProjectComplexityId;
                _oDBApiCharter.Pidfid = long.Parse(_oAPICharter.Pidfid);

                _oDBApiCharter.CreatedBy = _oAPICharter.LoggedInUserId;
                _oDBApiCharter.CreatedDate = DateTime.Now;
                _pidf_API_Charter_repository.AddAsync(_oDBApiCharter);
                //Implement PIDF staurs change
                await _unitOfWork.SaveChangesAsync();
            }

            var _StatusID = (_oAPICharter.SaveType == "Save") ? Master_PIDFStatus.APISubmitted : Master_PIDFStatus.APIInProgress;
            await _auditLogService.UpdatePIDFStatusCommon(long.Parse(_oAPICharter.Pidfid), (int)_StatusID, _oAPICharter.LoggedInUserId);
            await _notificationService.CreateNotification(long.Parse(_oAPICharter.Pidfid), (int)_StatusID, string.Empty, string.Empty, _oAPICharter.LoggedInUserId);
            return DBOperation.Success;
        }
        public async Task<DBOperation> AddUpdateAPIGroupLeader(APIInterestedUserEntity _oAPIAssignedUser, int _pidfid)
        {
            try
            {
                var loggedInUserID = _helper.GetLoggedInUser().UserId;
                var dbObj = await _pidf_API_Master_repository.GetAsync(x => x.Pidfid == _pidfid);
                if (dbObj != null)
                {
                    dbObj.Interested = _oAPIAssignedUser.IsAPIIntrested;
                    dbObj.Remark = _oAPIAssignedUser.ApiRemark;
                    _pidf_API_Master_repository.UpdateAsync(dbObj);
                }
                else
                {
                    var NewObject = new PidfApiMaster();
                    NewObject.Pidfid = _pidfid;
                    NewObject.UserId = _oAPIAssignedUser.AssignedAPIUser;
                    NewObject.Interested = _oAPIAssignedUser.IsAPIIntrested;
                    NewObject.Remark = _oAPIAssignedUser.ApiRemark;
                    NewObject.CreatedBy = loggedInUserID;
                    NewObject.CreatedDate = DateTime.Now;
                    _pidf_API_Master_repository.AddAsync(NewObject);
                }

                var outsourcedbObj = _pIDFAPIOutsource.GetAll().Where(x => x.Pidfid == _pidfid).ToList();//_pIDFAPIOutsource
                if (_oAPIAssignedUser.IsAPIIntrested == false && outsourcedbObj.Count>0)
                {
                    foreach (var notInterestedData in _oAPIAssignedUser.NotInterestedDatas)
                    {
                        var updatedObject = new PidfApiOutsourceDatum
                        {
                            ApioutsourceDataId = outsourcedbObj.Where(x=>x.ApioutsourceId==notInterestedData.ApiOutsourceId).Select(x=>x.ApioutsourceDataId).FirstOrDefault(),
                            ApioutsourceId = notInterestedData.ApiOutsourceId,
                            Pidfid = _pidfid,
                            Primary = notInterestedData.Details[0],
                            PotentialAlt1 = notInterestedData.Details[1],
                            PotentialAlt2 = notInterestedData.Details[2]
                        };
                        _pIDFAPIOutsource.UpdateAsync(updatedObject);
                         _unitOfWork.SaveChanges();
                    }
                }
                else
                {
                    foreach (var notInterestedData in _oAPIAssignedUser.NotInterestedDatas)
                    {
                        var newObject = new PidfApiOutsourceDatum();
                        newObject.ApioutsourceId = notInterestedData.ApiOutsourceId;
                        newObject.Pidfid = _pidfid;
                        newObject.Primary = notInterestedData.Details[0];
                        newObject.PotentialAlt1 = notInterestedData.Details[1];
                        newObject.PotentialAlt2 = notInterestedData.Details[2];
                        newObject.CreatedBy = loggedInUserID;
                        newObject.CreatedDate = DateTime.Now;
                        _pIDFAPIOutsource.AddAsync(newObject);
                    }
                }
                await _unitOfWork.SaveChangesAsync();
                return DBOperation.Success;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
        public async Task<DBOperation> InsertAPIInterestedUserData(List<PIDFAPIInhouseEntity> pIDFAPIInhouseEntity,int pidfId)
        {
            var loggedInUserID = _helper.GetLoggedInUser().UserId;
            var inhousesourcedbObj = _pIDFAPIInhouse.GetAll().Where(x => x.Pidfid == pidfId).ToList();
            if (inhousesourcedbObj.Count>0)
            {
                foreach (var InterestedData in pIDFAPIInhouseEntity)
                {
                    var updatedObject = new PidfApiInhouse
                    {
                        PidfapiinhouseId = inhousesourcedbObj.Where(x => x.ApiinhouseId == InterestedData.ApiInhouseId).Select(x => x.PidfapiinhouseId).FirstOrDefault(),
                        ApiinhouseId = InterestedData.ApiInhouseId,
                        Pidfid = pidfId,
                        Primary = InterestedData.Primary,
                    };
                    _pIDFAPIInhouse.UpdateAsync(updatedObject);
                    _unitOfWork.SaveChanges();
                }
            }
            else
            {
                foreach (var InterestedData in pIDFAPIInhouseEntity)
                {
                    var newObject = new PidfApiInhouse
                    {
                        ApiinhouseId = InterestedData.ApiInhouseId,
                        Pidfid = pidfId,
                        Primary = InterestedData.Primary,
                        CreatedBy = loggedInUserID,
                        CreatedDate = DateTime.Now
                    };

                    _pIDFAPIInhouse.AddAsync(newObject);
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return DBOperation.Success;
        }
        public async Task<List<MasterAPIOutsourceEntity>> GetAllMasterAPIOutsourcelabels()
        {
            var dbObj = await _masterAPIOutsource.GetAllAsync();
            return _mapperFactory.GetList<MasterApiOutsource, MasterAPIOutsourceEntity>(dbObj.ToList());
        }
        public async Task<List<PIDFAPIInhouseEntity>> GetAPICharterDataByPIDF(long pidfId)
        {
            var inhouselabellist = await _masterAPIInhouse.GetAllAsync();
            var pidfInhouseList = _pIDFAPIInhouse.GetAllAsync().Result.Where(x => x.Pidfid == pidfId);
            if (pidfInhouseList.Count() > 0)
            {
            var joinedData = from p in pidfInhouseList
                             join pi in inhouselabellist
                             on p.ApiinhouseId equals pi.ApiinhouseId

                             select new PIDFAPIInhouseEntity()
                             {
                                 Primary = p.Primary,
                                 PIDFAPIInhouseId = p.PidfapiinhouseId,
                                 PIDFId = p.Pidfid,
                                 CreatedDate = p.CreatedDate,
                                 CreatedBy = p.CreatedBy,
                                 ApiInhouseId = p.ApiinhouseId,
                                 APIInhouseName = pi.ApiinhouseName
                             };

            return joinedData.ToList(); // Return the entire list of joined data
        }
            else
            {
                var getData = from pi in inhouselabellist
                                 select new PIDFAPIInhouseEntity()
                                 {
                                     ApiInhouseId = pi.ApiinhouseId,
                                     APIInhouseName = pi.ApiinhouseName
                                 };

                return getData.ToList();
            }
        }

       
    }
}