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
    public class MasterBusinessUnitService : IMasterBusinessUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterBusinessUnit> _repository { get; set; }
        private IRepository<MasterBusinessUnitCountryMapping> _repositoryMasterBusinessUnitCountryMapping { get; set; }
        private IRepository<MasterCountry> _countryRepository { get; set; }


        public MasterBusinessUnitService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterBusinessUnit>();
            _countryRepository = _unitOfWork.GetRepository<MasterCountry>();
            _repositoryMasterBusinessUnitCountryMapping = _unitOfWork.GetRepository<MasterBusinessUnitCountryMapping>();
        }

        public async Task<List<MasterBusinessUnitEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterBusinessUnit, MasterBusinessUnitEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterBusinessUnitEntity> GetById(int id)
        {
            var  businessUnit = _mapperFactory.Get<MasterBusinessUnit, MasterBusinessUnitEntity>(await _repository.GetAsync(id));
            businessUnit.CountryIds = string.Join(",", _repositoryMasterBusinessUnitCountryMapping.GetAllQuery().Where(x => x.BusinessUnitId == businessUnit.BusinessUnitId).Select(x => x.CountryId.ToString()));
            businessUnit.MasterBusinessCountryMappingIds = string.Join(",", _repositoryMasterBusinessUnitCountryMapping.GetAllQuery().Where(x => x.BusinessUnitId == businessUnit.BusinessUnitId).Select(x => x.BusinessUnitCountryMappingId.ToString()));
            return businessUnit;
        }

        public async Task<DBOperation> AddUpdateBusinessUnit(MasterBusinessUnitEntity entityBusinessUnit)
        {
            MasterBusinessUnit objBusinessUnit;
            var oMasterBusinessUnitCountryMapping = new MasterBusinessUnitCountryMappingEntity(); ;
            var objMasterBusinessUnitCountryMapping = new MasterBusinessUnitCountryMapping();
            
            string[] countryList = entityBusinessUnit.CountryIds.Split(',');
            int[] countryIds = countryList.Select(int.Parse).ToArray();

            if (entityBusinessUnit.BusinessUnitId > 0)
            {
                objBusinessUnit = _repository.Get(entityBusinessUnit.BusinessUnitId);
                if (objBusinessUnit != null)
                {
                    objBusinessUnit = _mapperFactory.Get<MasterBusinessUnitEntity, MasterBusinessUnit>(entityBusinessUnit);
                    _repository.UpdateAsync(objBusinessUnit);

                    await _unitOfWork.SaveChangesAsync();

                    oMasterBusinessUnitCountryMapping.BusinessUnitId = entityBusinessUnit.BusinessUnitId;

                    if (entityBusinessUnit.MasterBusinessCountryMappingIds != null || entityBusinessUnit.MasterBusinessCountryMappingIds != "")
                    {
                        var entityBusinessUnitCountry = _repositoryMasterBusinessUnitCountryMapping.GetAllQuery().Where(x => entityBusinessUnit.MasterBusinessCountryMappingIds.Split(',', StringSplitOptions.None).Contains(x.BusinessUnitCountryMappingId.ToString()));
                        foreach (var item in entityBusinessUnitCountry)
                        {
                            _repositoryMasterBusinessUnitCountryMapping.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();

                        foreach (var country in countryIds)
                        {
                            oMasterBusinessUnitCountryMapping.CountryId = country;
                            objMasterBusinessUnitCountryMapping = _mapperFactory.Get<MasterBusinessUnitCountryMappingEntity, MasterBusinessUnitCountryMapping>(oMasterBusinessUnitCountryMapping);
                            _repositoryMasterBusinessUnitCountryMapping.AddAsync(objMasterBusinessUnitCountryMapping);
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
                objBusinessUnit = _mapperFactory.Get<MasterBusinessUnitEntity, MasterBusinessUnit>(entityBusinessUnit);
                _repository.AddAsync(objBusinessUnit);

                await _unitOfWork.SaveChangesAsync();

                if (objBusinessUnit.BusinessUnitId != 0)
                {
                    oMasterBusinessUnitCountryMapping.BusinessUnitId = objBusinessUnit.BusinessUnitId;
                    foreach (var country in countryIds)
                    {
                        oMasterBusinessUnitCountryMapping.CountryId = country;
                        objMasterBusinessUnitCountryMapping = _mapperFactory.Get<MasterBusinessUnitCountryMappingEntity, MasterBusinessUnitCountryMapping>(oMasterBusinessUnitCountryMapping);
                        _repositoryMasterBusinessUnitCountryMapping.AddAsync(objMasterBusinessUnitCountryMapping);
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
            var entityBusinessUnitCountryMapping = _repositoryMasterBusinessUnitCountryMapping.GetAllQuery().Where(x => x.BusinessUnitId == id);
            if (entityBusinessUnit == null)
                return DBOperation.NotFound;

            if (entityBusinessUnitCountryMapping == null)
                return DBOperation.NotFound;

            foreach (var item in entityBusinessUnitCountryMapping)
            {
                _repositoryMasterBusinessUnitCountryMapping.Remove(item);
            }
            _repository.Remove(entityBusinessUnit);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
        public async Task<MasterCountryEntity> GetCountryByBusinessUnitId(int id)
        {
            var businessUnitCountryMapping = _repositoryMasterBusinessUnitCountryMapping.Get(x => x.BusinessUnitId == id);
            var country = _countryRepository.Get(x => x.CountryId == businessUnitCountryMapping.CountryId);
            return _mapperFactory.Get<MasterCountry, MasterCountryEntity>(country);
        }
    }
}