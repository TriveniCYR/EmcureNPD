using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class WishListService : IWishList
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<TblWishList> _repository { get; set; }
        private IRepository<MasterWishListType> _repositorywishType { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IMasterAuditLogService _auditLogService;
        private readonly IExceptionService _ExceptionService;
        private readonly IHelper _helper;
        public WishListService(IUnitOfWork unitOfWork,
            IConfiguration configuration, DbContext dbContext, IMasterAuditLogService auditLogService, IExceptionService exceptionService, IHelper helper, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _auditLogService = auditLogService;
            _repository = _unitOfWork.GetRepository<TblWishList>();
            _repositorywishType = unitOfWork.GetRepository<MasterWishListType>();
            _ExceptionService = exceptionService;
            _helper = helper;
            _mapperFactory = mapperFactory;
        }
        public async Task<DBOperation> AddUpdateWishList(WishListEntity model)
        {
            TblWishList objWishlist=new TblWishList();
            var loggedInUserId = _helper.GetLoggedInUser().UserId;
            if (model.WishListId>0)
            {
                objWishlist = await _repository.GetAsync(model.WishListId);
                if(objWishlist != null) {
                    objWishlist.CreatedBy = loggedInUserId;
                    objWishlist.UpdatedOn = DateTime.Now;
                    objWishlist.MoleculeName = model.MoleculeName;
                    objWishlist.WishListTypeId = model.WishListTypeId;
                    objWishlist.GeographyId = model.GeographyId;
                    objWishlist.CountryId=model.CountryId;
                    objWishlist.Strength = model.Strength;
                    objWishlist.IsInhouseOrInLicensed = model.IsInhouseOrInLicensed;
                    objWishlist.DateOfFiling = model.DateOfFiling;
                    objWishlist.DateOfApproval = model.DateOfApproval;
                    objWishlist.NameofVendor = model.NameofVendor;
                    objWishlist.VendorEvaluationRemark = model.VendorEvaluationRemark;
                    objWishlist.ReferenceDrugProduct = model.ReferenceDrugProduct;
                    objWishlist.Remarks = model.Remarks;
                    _repository.UpdateAsync(objWishlist);
                }
              
            }
            else
            {
                objWishlist.CreatedBy = loggedInUserId;
                objWishlist.CreatedOn = DateTime.Now;
                objWishlist.MoleculeName = model.MoleculeName;
                objWishlist.WishListTypeId = model.WishListTypeId;
                objWishlist.GeographyId = model.GeographyId;
                objWishlist.CountryId = model.CountryId;
                objWishlist.Strength = model.Strength;
                objWishlist.IsInhouseOrInLicensed = model.IsInhouseOrInLicensed;
                objWishlist.DateOfFiling = model.DateOfFiling;
                objWishlist.DateOfApproval = model.DateOfApproval;
                objWishlist.NameofVendor = model.NameofVendor;
                objWishlist.VendorEvaluationRemark = model.VendorEvaluationRemark;
                objWishlist.ReferenceDrugProduct = model.ReferenceDrugProduct;
                objWishlist.Remarks = model.Remarks;
                _repository.AddAsync(objWishlist);
                
            }
            await _unitOfWork.SaveChangesAsync();
            return DBOperation.Success;
        }

        public async Task<List<WishListEntity>> GetAll()
        {
          return  _mapperFactory.GetList<TblWishList, WishListEntity>(await _repository.GetAllAsync());
        }
		public async Task<DataTableResponseModel> GetAllWishList(DataTableAjaxPostModel model)
		{
			DataTableResponseModel oDataTableResponseModel = null;
			if (model.columns == null)
			{
				return oDataTableResponseModel;
			}

			string ColumnName = (model.order.Count > 0 ? model.columns[model.order[0].column].data : string.Empty);
			string SortDir = (model.order.Count > 0 ? model.order[0].dir : string.Empty);

			//int userId = _helper.GetLoggedInUser().UserId;

			SqlParameter[] osqlParameter = {
				//new SqlParameter("@UserId",userId),
				new SqlParameter("@CurrentPageNumber", model.start),
					new SqlParameter("@PageSize", model.length),
					new SqlParameter("@SortColumn", ColumnName),
					new SqlParameter("@SortDirection", SortDir),
					new SqlParameter("@SearchText", model.search.value)
			};

			var wishLsit = await _repository.GetBySP("GetWishList", System.Data.CommandType.StoredProcedure, osqlParameter);

			var TotalRecord = (wishLsit != null && wishLsit.Rows.Count > 0 ? Convert.ToInt32(wishLsit.Rows[0]["TotalRecord"]) : 0);
			var TotalCount = (wishLsit != null && wishLsit.Rows.Count > 0 ? Convert.ToInt32(wishLsit.Rows[0]["TotalCount"]) : 0);

			oDataTableResponseModel = new DataTableResponseModel(model.draw, TotalRecord, TotalCount, wishLsit);
			return oDataTableResponseModel;
		}
		public async Task<TblWishList> GetById(int id)
        {
           return await _repository.GetAsync((long)id);
        }

        public async Task<dynamic> GetWishListByTypeId(int id = 0)
        {
            try
            {
                SqlParameter[] osqlParameter = {
                new SqlParameter("@WishListTypeId", id)
            };

                DataSet data = await _repository.GetDataSetBySP("GetWishListByTypeId", System.Data.CommandType.StoredProcedure, osqlParameter);
                return data;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return null;
            }
        }
        public async Task<List<MasterWishListType>> GetWishListType()
        {
            try
            {
              return await _repositorywishType.GetAllAsync();
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return null;
            }
        }
    }
}
