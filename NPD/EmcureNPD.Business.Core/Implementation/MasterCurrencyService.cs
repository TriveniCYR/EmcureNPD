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
    public class MasterCurrencyService : IMasterCurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterCurrency> _repository { get; set; }
        private IRepository<MasterCurrencyCountryMapping> _repositoryMasterCurrencyCountryMapping { get; set; }
        private IRepository<MasterCountry> _countryRepository { get; set; }


        public MasterCurrencyService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterCurrency>();
            _countryRepository = _unitOfWork.GetRepository<MasterCountry>();
            _repositoryMasterCurrencyCountryMapping = _unitOfWork.GetRepository<MasterCurrencyCountryMapping>();
        }

        public async Task<List<MasterCurrencyEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterCurrency, MasterCurrencyEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterCurrencyEntity> GetById(int id)
        {
            var  Currency = _mapperFactory.Get<MasterCurrency, MasterCurrencyEntity>(await _repository.GetAsync(id));
           // Currency.CountryIds = string.Join(",", _repositoryMasterCurrencyCountryMapping.GetAllQuery().Where(x => x.CurrencyId == Currency.CurrencyId).Select(x => x.CountryId.ToString()));
            Currency.MasterBusinessCountryMappingIds = string.Join(",", _repositoryMasterCurrencyCountryMapping.GetAllQuery().Where(x => x.CurrencyId == Currency.CurrencyId).Select(x => x.CurrencyCountryMappingId.ToString()));
            return Currency;
        }

        public async Task<DBOperation> AddUpdateCurrency(MasterCurrencyEntity entityCurrency)
        {
            MasterCurrency objCurrency;
            var oMasterCurrencyCountryMapping = new MasterCurrencyCountryMappingEntity(); ;
            var objMasterCurrencyCountryMapping = new MasterCurrencyCountryMapping();
            
        //    string[] countryList = entityCurrency.CountryIds.Split(',');
        //    int[] countryIds = countryList.Select(int.Parse).ToArray();

            if (entityCurrency.CurrencyId > 0)
            {
                objCurrency = _repository.Get(entityCurrency.CurrencyId);
                if (objCurrency != null)
                {
                    objCurrency = _mapperFactory.Get<MasterCurrencyEntity, MasterCurrency>(entityCurrency);
                    _repository.UpdateAsync(objCurrency);

                    await _unitOfWork.SaveChangesAsync();

                    oMasterCurrencyCountryMapping.CurrencyId = entityCurrency.CurrencyId;

                    if (entityCurrency.MasterBusinessCountryMappingIds != null || entityCurrency.MasterBusinessCountryMappingIds != "")
                    {
                        var entityCurrencyCountry = _repositoryMasterCurrencyCountryMapping.GetAllQuery().Where(x => entityCurrency.MasterBusinessCountryMappingIds.Split(',', StringSplitOptions.None).Contains(x.CurrencyCountryMappingId.ToString()));
                        foreach (var item in entityCurrencyCountry)
                        {
                            _repositoryMasterCurrencyCountryMapping.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();

                        /*foreach (var country in countryIds)
                        {
                            oMasterCurrencyCountryMapping.CountryId = country;
                            objMasterCurrencyCountryMapping = _mapperFactory.Get<MasterCurrencyCountryMappingEntity, MasterCurrencyCountryMapping>(oMasterCurrencyCountryMapping);
                            _repositoryMasterCurrencyCountryMapping.AddAsync(objMasterCurrencyCountryMapping);
                        }*/
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
                objCurrency = _mapperFactory.Get<MasterCurrencyEntity, MasterCurrency>(entityCurrency);
                _repository.AddAsync(objCurrency);

                await _unitOfWork.SaveChangesAsync();

                if (objCurrency.CurrencyId != 0)
                {
                    oMasterCurrencyCountryMapping.CurrencyId = objCurrency.CurrencyId;
                    /*foreach (var country in countryIds)
                    {
                        oMasterCurrencyCountryMapping.CountryId = country;
                        objMasterCurrencyCountryMapping = _mapperFactory.Get<MasterCurrencyCountryMappingEntity, MasterCurrencyCountryMapping>(oMasterCurrencyCountryMapping);
                        _repositoryMasterCurrencyCountryMapping.AddAsync(objMasterCurrencyCountryMapping);
                    }*/
                    await _unitOfWork.SaveChangesAsync();
                }
                else { return DBOperation.NotFound; }

            }

            if (objCurrency.CurrencyId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteCurrency(int id)
        {
            var entityCurrency = _repository.Get(x => x.CurrencyId == id);
            var entityCurrencyCountryMapping = _repositoryMasterCurrencyCountryMapping.GetAllQuery().Where(x => x.CurrencyId == id);
            if (entityCurrency == null)
                return DBOperation.NotFound;

            if (entityCurrencyCountryMapping == null)
                return DBOperation.NotFound;

            foreach (var item in entityCurrencyCountryMapping)
            {
                _repositoryMasterCurrencyCountryMapping.Remove(item);
            }
            _repository.Remove(entityCurrency);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
        public async Task<MasterCountryEntity> GetCountryByCurrencyId(int id)
        {
            var CurrencyCountryMapping = _repositoryMasterCurrencyCountryMapping.Get(x => x.CurrencyId == id);
            var country = _countryRepository.Get(x => x.CountryId == CurrencyCountryMapping.CountryId);
            return _mapperFactory.Get<MasterCountry, MasterCountryEntity>(country);
        }
    }
}