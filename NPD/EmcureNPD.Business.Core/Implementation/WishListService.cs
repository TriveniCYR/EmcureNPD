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
    }
}
