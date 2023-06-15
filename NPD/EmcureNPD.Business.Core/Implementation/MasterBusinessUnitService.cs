using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.ServiceImplementations
{
    public class MasterBusinessUnitService : IMasterBusinessUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterBusinessUnit> _repository { get; set; }
        private IRepository<MasterBusinessUnitRegionMapping> _repositoryMasterBusinessUnitRegionMapping { get; set; }
        private IRepository<MasterRegion> _regionRepository { get; set; }
        private readonly IHelper _helper;

        public MasterBusinessUnitService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _helper = helper;
            _repository = _unitOfWork.GetRepository<MasterBusinessUnit>();
            _regionRepository = _unitOfWork.GetRepository<MasterRegion>();
            _repositoryMasterBusinessUnitRegionMapping = _unitOfWork.GetRepository<MasterBusinessUnitRegionMapping>();
        }

        public async Task<List<MasterBusinessUnitEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterBusinessUnit, MasterBusinessUnitEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterBusinessUnitEntity> GetById(int id)
        {
            var businessUnit = _mapperFactory.Get<MasterBusinessUnit, MasterBusinessUnitEntity>(await _repository.GetAsync(id));
            businessUnit.RegionIds = string.Join(",", _repositoryMasterBusinessUnitRegionMapping.GetAllQuery().Where(x => x.BusinessUnitId == businessUnit.BusinessUnitId).Select(x => x.RegionId.ToString()));
            businessUnit.MasterBusinessRegionMappingIds = string.Join(",", _repositoryMasterBusinessUnitRegionMapping.GetAllQuery().Where(x => x.BusinessUnitId == businessUnit.BusinessUnitId).Select(x => x.BusinessUnitCountryMappingId.ToString()));
            return businessUnit;
        }

        public async Task<DBOperation> AddUpdateBusinessUnit(MasterBusinessUnitEntity entityBusinessUnit)
        {
            MasterBusinessUnit objBusinessUnit;
            var oMasterBusinessUnitRegionMapping = new MasterBusinessUnitRegionMappingEntity(); ;
            var objMasterBusinessUnitRegionMapping = new MasterBusinessUnitRegionMapping();

            string[] regionList = entityBusinessUnit.RegionIds.Split(',');
            int[] regionIds = regionList.Select(int.Parse).ToArray();

            if (entityBusinessUnit.IsDomestic)
            {
                var ListOfDomesticBU = await _repository.GetAllAsync(x => x.IsDomestic == true);
                if (ListOfDomesticBU != null && ListOfDomesticBU.Count() > 0)
                {
                    foreach(var item in ListOfDomesticBU)
                    {
                        item.IsDomestic = false;
						_repository.UpdateAsync(item);
					}
					await _unitOfWork.SaveChangesAsync();
				}
			}
            
            if (entityBusinessUnit.BusinessUnitId > 0)
            {
                var objBusinessUnitModelDate = _repository.Exists(x => x.BusinessUnitName.ToLower() == entityBusinessUnit.BusinessUnitName.ToLower() && x.BusinessUnitId!= entityBusinessUnit.BusinessUnitId);
                if (objBusinessUnitModelDate)
                { return DBOperation.AlreadyExist; }
                objBusinessUnit = _repository.Get(entityBusinessUnit.BusinessUnitId);
                if (objBusinessUnit != null)
                {
                    objBusinessUnit = _mapperFactory.Get<MasterBusinessUnitEntity, MasterBusinessUnit>(entityBusinessUnit);
                    _repository.UpdateAsync(objBusinessUnit);

                    await _unitOfWork.SaveChangesAsync();

                    oMasterBusinessUnitRegionMapping.BusinessUnitId = entityBusinessUnit.BusinessUnitId;

                    if (entityBusinessUnit.MasterBusinessRegionMappingIds != null || entityBusinessUnit.MasterBusinessRegionMappingIds != "")
                    {
                        var entityBusinessUnitCountry = _repositoryMasterBusinessUnitRegionMapping.GetAllQuery().Where(x => entityBusinessUnit.MasterBusinessRegionMappingIds.Split(',', StringSplitOptions.None).Contains(x.BusinessUnitCountryMappingId.ToString()));
                        foreach (var item in entityBusinessUnitCountry)
                        {
                            _repositoryMasterBusinessUnitRegionMapping.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();

                        foreach (var region in regionIds)
                        {
                            oMasterBusinessUnitRegionMapping.RegionId = region;
                            objMasterBusinessUnitRegionMapping = _mapperFactory.Get<MasterBusinessUnitRegionMappingEntity, MasterBusinessUnitRegionMapping>(oMasterBusinessUnitRegionMapping);
                            _repositoryMasterBusinessUnitRegionMapping.AddAsync(objMasterBusinessUnitRegionMapping);
                        }
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objBusinessUnitModelDate = _repository.Exists(x => x.BusinessUnitName.ToLower() == entityBusinessUnit.BusinessUnitName.ToLower());
                if (objBusinessUnitModelDate)
                { return DBOperation.AlreadyExist; }
                objBusinessUnit = _mapperFactory.Get<MasterBusinessUnitEntity, MasterBusinessUnit>(entityBusinessUnit);
                _repository.AddAsync(objBusinessUnit);

                await _unitOfWork.SaveChangesAsync();

                if (objBusinessUnit.BusinessUnitId != 0)
                {
                    oMasterBusinessUnitRegionMapping.BusinessUnitId = objBusinessUnit.BusinessUnitId;
                    foreach (var region in regionIds)
                    {
                        oMasterBusinessUnitRegionMapping.RegionId = region;
                        objMasterBusinessUnitRegionMapping = _mapperFactory.Get<MasterBusinessUnitRegionMappingEntity, MasterBusinessUnitRegionMapping>(oMasterBusinessUnitRegionMapping);
                        _repositoryMasterBusinessUnitRegionMapping.AddAsync(objMasterBusinessUnitRegionMapping);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
                else { return DBOperation.NotFound; }
            }

            if (objBusinessUnit.BusinessUnitId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteBusinessUnit(int id)
        {
            var entityBusinessUnit = _repository.Get(x => x.BusinessUnitId == id);
            var entityBusinessUnitRegionMapping = _repositoryMasterBusinessUnitRegionMapping.GetAllQuery().Where(x => x.BusinessUnitId == id);
            if (entityBusinessUnit == null)
                return DBOperation.NotFound;

            if (entityBusinessUnitRegionMapping == null)
                return DBOperation.NotFound;

            foreach (var item in entityBusinessUnitRegionMapping)
            {
                _repositoryMasterBusinessUnitRegionMapping.Remove(item);
            }
            _repository.Remove(entityBusinessUnit);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }

        public async Task<MasterRegionEntity> GetRegionByBusinessUnitId(int id)
        {
            var businessUnitRegionMapping = _repositoryMasterBusinessUnitRegionMapping.Get(x => x.BusinessUnitId == id);
            var region = _regionRepository.Get(x => x.RegionId == businessUnitRegionMapping.RegionId);
            return _mapperFactory.Get<MasterRegion, MasterRegionEntity>(region);
        }       

        public async Task<dynamic> GetCountryByBusinessUnitId(int BusinessUnitId)
        {
            var loggedInUserId = _helper.GetLoggedInUser().UserId;

            SqlParameter[] osqlParameter = {
                new SqlParameter("@BusinessUnitId", BusinessUnitId),
                new SqlParameter("@UserId", loggedInUserId)
            };
            var _businessUnit = await _repository.GetBySP("SP_GetCountryByBusinessUnit", CommandType.StoredProcedure, osqlParameter);
            return _businessUnit;
        }

        public DataTable GetActiveBusinessUnit()
        {
            return _repository.GetDataBySP("SP_GetActiveBusinessUnit");
        }
        public List<MasterBusinessUnitEntity> GetActiveEncryptedBusinessUnit()
        {
            var BU_List = _repository.GetAllQuery().Where(x => x.IsActive == true).ToList();
            var _objBusinessUnitEntity = _mapperFactory.GetList<MasterBusinessUnit, MasterBusinessUnitEntity>(BU_List);
            var _objBusinessUnitEntity_list = new List<MasterBusinessUnitEntity>();

			foreach (var item in _objBusinessUnitEntity)
            {
                _objBusinessUnitEntity_list.Add(
                    new MasterBusinessUnitEntity()
                    {
                        BusinessUnitId = item.BusinessUnitId,
                        BusinessUnitName = item.BusinessUnitName,
                        EncBusinessUnitId = UtilityHelper.Encrypt(Convert.ToString(item.BusinessUnitId))
                    });

			}
            return _objBusinessUnitEntity_list;
		}
    }
}