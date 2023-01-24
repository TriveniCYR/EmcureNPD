using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.ServiceImplementations
{
    public class MasterCountryService : IMasterCountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterCountry> _repository { get; set; }

        public MasterCountryService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterCountry>();
        }

        public async Task<List<MasterCountryEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterCountry, MasterCountryEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterCountryEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterCountry, MasterCountryEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateCountry(MasterCountryEntity entityCountry)
        {
            MasterCountry objCountry;
            if (entityCountry.CountryId > 0)
            {
                objCountry = _repository.Get(entityCountry.CountryId);
                if (objCountry != null)
                {
                    objCountry = _mapperFactory.Get<MasterCountryEntity, MasterCountry>(entityCountry);
                    _repository.UpdateAsync(objCountry);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objCountry = _mapperFactory.Get<MasterCountryEntity, MasterCountry>(entityCountry);
                _repository.AddAsync(objCountry);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objCountry.CountryId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteCountry(int id)
        {
            var entityCountry = _repository.Get(x => x.CountryId == id);

            if (entityCountry == null)
                return DBOperation.NotFound;

            _repository.Remove(entityCountry);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}