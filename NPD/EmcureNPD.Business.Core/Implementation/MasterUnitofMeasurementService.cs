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
    public class MasterUnitofMeasurementService : IMasterUnitofMeasurementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterUnitofMeasurement> _repository { get; set; }

        public MasterUnitofMeasurementService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterUnitofMeasurement>();
        }

        public async Task<List<MasterUnitofMeasurementEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterUnitofMeasurement, MasterUnitofMeasurementEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterUnitofMeasurementEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterUnitofMeasurement, MasterUnitofMeasurementEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateUnitofMeasurement(MasterUnitofMeasurementEntity entityUnitofMeasurement)
        {
            MasterUnitofMeasurement objUnitofMeasurement;
            if (entityUnitofMeasurement.UnitofMeasurementId > 0)
            {
                objUnitofMeasurement = _repository.Get(entityUnitofMeasurement.UnitofMeasurementId);
                if (objUnitofMeasurement != null)
                {
                    objUnitofMeasurement = _mapperFactory.Get<MasterUnitofMeasurementEntity, MasterUnitofMeasurement>(entityUnitofMeasurement);
                    _repository.UpdateAsync(objUnitofMeasurement);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.UnitofMeasurementName.ToLower() == entityUnitofMeasurement.UnitofMeasurementName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objUnitofMeasurement = _mapperFactory.Get<MasterUnitofMeasurementEntity, MasterUnitofMeasurement>(entityUnitofMeasurement);
                _repository.AddAsync(objUnitofMeasurement);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objUnitofMeasurement.UnitofMeasurementId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteUnitofMeasurement(int id)
        {
            var entityUnitofMeasurement = _repository.Get(x => x.UnitofMeasurementId == id);

            if (entityUnitofMeasurement == null)
                return DBOperation.NotFound;

            _repository.Remove(entityUnitofMeasurement);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}