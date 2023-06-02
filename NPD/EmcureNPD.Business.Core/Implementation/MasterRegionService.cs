using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.ServiceImplementations
{
    public class MasterRegionService : IMasterRegionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterRegion> _repository { get; set; }
        private IRepository<MasterRegionCountryMapping> _repositoryMasterRegionCountryMapping { get; set; }
        private IRepository<MasterCountry> _countryRepository { get; set; }

        public MasterRegionService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterRegion>();
            _countryRepository = _unitOfWork.GetRepository<MasterCountry>();
            _repositoryMasterRegionCountryMapping = _unitOfWork.GetRepository<MasterRegionCountryMapping>();
        }

        public async Task<List<MasterRegionEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterRegion, MasterRegionEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterRegionEntity> GetById(int id)
        {
            var Region = _mapperFactory.Get<MasterRegion, MasterRegionEntity>(await _repository.GetAsync(id));
            Region.CountryIds = string.Join(",", _repositoryMasterRegionCountryMapping.GetAllQuery().Where(x => x.RegionId == Region.RegionId).Select(x => x.CountryId.ToString()));
            Region.MasterBusinessCountryMappingIds = string.Join(",", _repositoryMasterRegionCountryMapping.GetAllQuery().Where(x => x.RegionId == Region.RegionId).Select(x => x.RegionCountryMappingId.ToString()));
            return Region;
        }

        public async Task<DBOperation> AddUpdateRegion(MasterRegionEntity entityRegion)
        {
            MasterRegion objRegion;
            var oMasterRegionCountryMapping = new MasterRegionCountryMappingEntity(); ;
            var objMasterRegionCountryMapping = new MasterRegionCountryMapping();

            string[] countryList = entityRegion.CountryIds.Split(',');
            int[] countryIds = countryList.Select(int.Parse).ToArray();

            if (entityRegion.RegionId > 0)
            {
                var objModalData = _repository.Exists(x => x.RegionName.ToLower() == entityRegion.RegionName.ToLower() && x.RegionId!= entityRegion.RegionId);
                if (objModalData)
                {
                    return DBOperation.AlreadyExist;
                }
                objRegion = _repository.Get(entityRegion.RegionId);
                if (objRegion != null)
                {
                    objRegion = _mapperFactory.Get<MasterRegionEntity, MasterRegion>(entityRegion);
                    _repository.UpdateAsync(objRegion);

                    await _unitOfWork.SaveChangesAsync();

                    oMasterRegionCountryMapping.RegionId = entityRegion.RegionId;

                    if (entityRegion.MasterBusinessCountryMappingIds != null || entityRegion.MasterBusinessCountryMappingIds != "")
                    {
                        var entityRegionCountry = _repositoryMasterRegionCountryMapping.GetAllQuery().Where(x => entityRegion.MasterBusinessCountryMappingIds.Split(',', StringSplitOptions.None).Contains(x.RegionCountryMappingId.ToString()));
                        foreach (var item in entityRegionCountry)
                        {
                            _repositoryMasterRegionCountryMapping.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();

                        foreach (var country in countryIds)
                        {
                            oMasterRegionCountryMapping.CountryId = country;
                            objMasterRegionCountryMapping = _mapperFactory.Get<MasterRegionCountryMappingEntity, MasterRegionCountryMapping>(oMasterRegionCountryMapping);
                            _repositoryMasterRegionCountryMapping.AddAsync(objMasterRegionCountryMapping);
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
                var objModalData = _repository.Exists(x => x.RegionName.ToLower() == entityRegion.RegionName.ToLower());
                if(objModalData)
                {
                    return DBOperation.AlreadyExist;
                }
                objRegion = _mapperFactory.Get<MasterRegionEntity, MasterRegion>(entityRegion);
                _repository.AddAsync(objRegion);

                await _unitOfWork.SaveChangesAsync();

                if (objRegion.RegionId != 0)
                {
                    oMasterRegionCountryMapping.RegionId = objRegion.RegionId;
                    foreach (var country in countryIds)
                    {
                        oMasterRegionCountryMapping.CountryId = country;
                        objMasterRegionCountryMapping = _mapperFactory.Get<MasterRegionCountryMappingEntity, MasterRegionCountryMapping>(oMasterRegionCountryMapping);
                        _repositoryMasterRegionCountryMapping.AddAsync(objMasterRegionCountryMapping);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
                else { return DBOperation.NotFound; }
            }

            if (objRegion.RegionId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteRegion(int id)
        {
            var entityRegion = _repository.Get(x => x.RegionId == id);
            var entityRegionCountryMapping = _repositoryMasterRegionCountryMapping.GetAllQuery().Where(x => x.RegionId == id);
            if (entityRegion == null)
                return DBOperation.NotFound;

            if (entityRegionCountryMapping == null)
                return DBOperation.NotFound;

            foreach (var item in entityRegionCountryMapping)
            {
                _repositoryMasterRegionCountryMapping.Remove(item);
            }
            _repository.Remove(entityRegion);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }

        public async Task<MasterCountryEntity> GetCountryByRegionId(int id)
        {
            var RegionCountryMapping = _repositoryMasterRegionCountryMapping.Get(x => x.RegionId == id);
            var country = _countryRepository.Get(x => x.CountryId == RegionCountryMapping.CountryId);
            return _mapperFactory.Get<MasterCountry, MasterCountryEntity>(country);
        }
    }
}