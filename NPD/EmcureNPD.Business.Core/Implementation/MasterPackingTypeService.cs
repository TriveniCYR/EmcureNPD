using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class MasterPackingTypeService : IMasterPackingTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterPackingType> _repository { get; set; }

        public MasterPackingTypeService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterPackingType>();
        }

        public async Task<List<MasterPackingTypeEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterPackingType, MasterPackingTypeEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterPackingTypeEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterPackingType, MasterPackingTypeEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdatePackingType(MasterPackingTypeEntity entityPackingType)
        {
            MasterPackingType objPackingType;
            if (entityPackingType.PackingTypeId > 0)
            {
                objPackingType = _repository.Get(entityPackingType.PackingTypeId);
                if (objPackingType != null)
                {
                    objPackingType = _mapperFactory.Get<MasterPackingTypeEntity, MasterPackingType>(entityPackingType);
                    _repository.UpdateAsync(objPackingType);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objPackingType = _mapperFactory.Get<MasterPackingTypeEntity, MasterPackingType>(entityPackingType);
                _repository.AddAsync(objPackingType);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objPackingType.PackingTypeId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeletePackingType(int id)
        {
            var entityPackingType = _repository.Get(x => x.PackingTypeId == id);

            if (entityPackingType == null)
                return DBOperation.NotFound;

            _repository.Remove(entityPackingType);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}