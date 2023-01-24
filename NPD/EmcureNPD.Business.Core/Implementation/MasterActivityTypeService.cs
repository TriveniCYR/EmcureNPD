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
    public class MasterActivityTypeService : IMasterActivityTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterActivityType> _repository { get; set; }

        public MasterActivityTypeService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterActivityType>();
        }

        public async Task<List<MasterActivityTypeEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterActivityType, MasterActivityTypeEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterActivityTypeEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterActivityType, MasterActivityTypeEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateActivityType(MasterActivityTypeEntity entityActivityType)
        {
            MasterActivityType objActivityType;
            if (entityActivityType.ActivityTypeId > 0)
            {
                objActivityType = _repository.Get(entityActivityType.ActivityTypeId);
                if (objActivityType != null)
                {
                    objActivityType = _mapperFactory.Get<MasterActivityTypeEntity, MasterActivityType>(entityActivityType);
                    _repository.UpdateAsync(objActivityType);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objActivityType = _mapperFactory.Get<MasterActivityTypeEntity, MasterActivityType>(entityActivityType);
                _repository.AddAsync(objActivityType);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objActivityType.ActivityTypeId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteActivityType(int id)
        {
            var entityActivityType = _repository.Get(x => x.ActivityTypeId == id);

            if (entityActivityType == null)
                return DBOperation.NotFound;

            _repository.Remove(entityActivityType);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}