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
    public class MasterPlantService : IMasterPlantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterPlant> _repository { get; set; }

        public MasterPlantService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterPlant>();
        }

        public async Task<List<MasterPlantEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterPlant, MasterPlantEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterPlantEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterPlant, MasterPlantEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdatePlant(MasterPlantEntity entityPlant)
        {
            MasterPlant objPlant;
            if (entityPlant.PlantId > 0)
            {
                var objModelData = _repository.Exists(x => x.PlantNameName.ToLower() == entityPlant.PlantNameName.ToLower() && x.PlantId!= entityPlant.PlantId);
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objPlant = _repository.Get(entityPlant.PlantId);
                if (objPlant != null)
                {
                    objPlant = _mapperFactory.Get<MasterPlantEntity, MasterPlant>(entityPlant);
                    _repository.UpdateAsync(objPlant);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.PlantNameName.ToLower() == entityPlant.PlantNameName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objPlant = _mapperFactory.Get<MasterPlantEntity, MasterPlant>(entityPlant);
                _repository.AddAsync(objPlant);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objPlant.PlantId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeletePlant(int id)
        {
            var entityPlant = _repository.Get(x => x.PlantId == id);

            if (entityPlant == null)
                return DBOperation.NotFound;

            _repository.Remove(entityPlant);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}