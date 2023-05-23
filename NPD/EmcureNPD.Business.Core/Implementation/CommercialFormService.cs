using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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

        private readonly IHelper _helper;

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
        private IRepository<MasterPackSize> _masterPackSizeEepository { get; set; }

        public CommercialFormService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory,
            IMasterBusinessUnitService businessUnitService, IMasterCountryService countryService,
            INotificationService notificationService,
            IMasterAuditLogService auditLogService, IPidfProductStrengthService productStrengthService, IExceptionService exceptionService, IHelper helper)
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
            _masterPackSizeEepository = _unitOfWork.GetRepository<MasterPackSize>();
            _notificationService = notificationService;
            _businessUnitrepository = _unitOfWork.GetRepository<MasterBusinessUnit>();
            _pidfProductStrengthrepository = _unitOfWork.GetRepository<PidfproductStrength>();
            _countryrepository = _unitOfWork.GetRepository<MasterCountry>();
            _ExceptionService = exceptionService;
            _helper = helper;
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
        public async Task<DBOperation> AddUpdateCommercialPIDF(PIDFCommercialViewModel entitycommPIDF)
        {
            var loggedInUserID = _helper.GetLoggedInUser().UserId;

            var AllObjofPidfId = _commercialrepository.GetAllQuery().Where(x => x.Pidfid == entitycommPIDF.Pidfid).ToList();
            if(AllObjofPidfId != null)
            {
                foreach(var _obj in AllObjofPidfId)
                {
					var IsExist =  entitycommPIDF.PIDFArrMainCommercial.Any(x=>x.BusinessUnitId == _obj.BusinessUnitId 
                            && x.PidfproductStrengthId == _obj.PidfproductStrengthId && x.PackSizeId == _obj.PackSizeId);

                    if (!IsExist) // if not IsExist then Delete
					{
						//Remove all Already mapped Years data
						var CommercialYears = await _commercialYearrepository.GetAllAsync(x => x.PidfcommercialId == _obj.PidfcommercialId);
						foreach (var it in CommercialYears.OrderBy(x => x.YearIndex))
						{							
							_commercialYearrepository.Remove(it);
							await _unitOfWork.SaveChangesAsync();
						}

						_commercialrepository.Remove(_obj);
						await _unitOfWork.SaveChangesAsync();
					}
				}
            }
			foreach (var item in entitycommPIDF.PIDFArrMainCommercial)
			{
				var listYear = new List<PidfCommercialYear>();
				int i = 1;

				foreach (var year in item.PidfCommercialYears.OrderBy(x => x.YearIndex))
				{
					var Yeardata = _mapperFactory.Get<PidfCommercialYearEntity, PidfCommercialYear>(year);
					//Yeardata.PackagingTypeId = year.CommercialPackagingTypeId;
					Yeardata.PidfcommercialYearId = 0;
					Yeardata.PidfcommercialId = 0;
					// Yeardata.YearIndex = year.YearIndex;
					listYear.Add(Yeardata);
					i++;
				}

				Expression<Func<PidfCommercial, bool>> expr = u => u.BusinessUnitId == item.BusinessUnitId && u.Pidfid == entitycommPIDF.Pidfid && u.PidfproductStrengthId == item.PidfproductStrengthId && u.PackSizeId == item.PackSizeId;
				var objFetchData = await _commercialrepository.GetAsync(expr);

				if (objFetchData == null)
				{
					var NewCommPIDF = new PidfCommercial();
					NewCommPIDF.Pidfid = item.Pidfid;
					NewCommPIDF.BusinessUnitId = item.BusinessUnitId;
					NewCommPIDF.PidfproductStrengthId = item.PidfproductStrengthId;
					NewCommPIDF.PackSizeId = item.PackSizeId;
					NewCommPIDF.MarketSizeInUnit = item.MarketSizeInUnit;
					NewCommPIDF.ShelfLife = item.ShelfLife;
					NewCommPIDF.CreatedDate = DateTime.Now;
					NewCommPIDF.CreatedBy = loggedInUserID;
					NewCommPIDF.IsDeleted = false;
					NewCommPIDF.PidfCommercialYears = listYear;
					_commercialrepository.AddAsync(NewCommPIDF);
					await _unitOfWork.SaveChangesAsync();
				}
				else
				{
					var OldObjpidfCommercial = _mapperFactory.Get<PidfCommercial, PIDFCommercialEntity>(objFetchData);

					//OldObjpidfCommercial.PidfCommercialYears = item.PidfCommercialYears;
					//Remove all Already mapped Years data
					var CommercialYears = await _commercialYearrepository.GetAllAsync(x => x.PidfcommercialId == objFetchData.PidfcommercialId);

					foreach (var it in CommercialYears.OrderBy(x => x.YearIndex))
					{
						OldObjpidfCommercial.PidfCommercialYears.Add(_mapperFactory.Get<PidfCommercialYear, PidfCommercialYearEntity>(it));
						_commercialYearrepository.Remove(it);
					}

					objFetchData.PidfCommercialYears = listYear;
					objFetchData.MarketSizeInUnit = item.MarketSizeInUnit;
					objFetchData.ShelfLife = item.ShelfLife;
					objFetchData.ModifyBy = loggedInUserID;
					objFetchData.ModifyDate = DateTime.Now;

					_commercialrepository.UpdateAsync(objFetchData);
					await _unitOfWork.SaveChangesAsync();

					var isSuccess = await _auditLogService.CreateAuditLog(Utility.Audit.AuditActionType.Update,
					ModuleEnum.CommercialManagement, OldObjpidfCommercial, item, loggedInUserID);
				}
			}

			var _StatusID = (entitycommPIDF.SaveType == "Sv") ? Master_PIDFStatus.CommercialSubmitted : Master_PIDFStatus.CommercialInProgress;
            await _auditLogService.UpdatePIDFStatusCommon(entitycommPIDF.Pidfid, (int)_StatusID, loggedInUserID);
            await _notificationService.CreateNotification(entitycommPIDF.Pidfid, (int)_StatusID, string.Empty, string.Empty, loggedInUserID);

            return DBOperation.Success;
        }

        public async Task<dynamic> GetCommercialFormData(long pidfId, int buid, int? strengthid)
        {
            var loggedInUserId = _helper.GetLoggedInUser().UserId;

            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFId", pidfId),
                new SqlParameter("@UserId", loggedInUserId),
            };

            DataSet dsCommercial = await _repository.GetDataSetBySP("stp_npd_GetCommercialFormData", System.Data.CommandType.StoredProcedure, osqlParameter);

            dynamic DropdownObjects = new ExpandoObject();
            DropdownObjects.Commercial = dsCommercial.Tables[0];
            DropdownObjects.CommercialYear = dsCommercial.Tables[1];
            DropdownObjects.PIDFStrength = dsCommercial.Tables[2];
            DropdownObjects.PIDF = dsCommercial.Tables[3];
            DropdownObjects.BusinessUnit = dsCommercial.Tables[4];
            DropdownObjects.Currency = dsCommercial.Tables[5];
            DropdownObjects.PackagingType = dsCommercial.Tables[6];
            DropdownObjects.PackSize = dsCommercial.Tables[7];
            DropdownObjects.FinalSelection = dsCommercial.Tables[8];
            return DropdownObjects;
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
        public async Task<List<MasterPackSizeViewModelEntity>> GetAllPackSize()
        {
            var list = await _masterPackSizeEepository.GetAllAsync(x => x.IsActive == true);
            return _mapperFactory.GetList<MasterPackSize, MasterPackSizeViewModelEntity>(list.ToList());
        }
    }
}