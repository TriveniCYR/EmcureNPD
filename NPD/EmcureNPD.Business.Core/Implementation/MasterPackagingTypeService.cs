using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.ServiceImplementations
{
    public class MasterPackagingTypeService : IMasterPackagingTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterPackagingType> _repository { get; set; }

        public MasterPackagingTypeService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterPackagingType>();
        }

        public async Task<List<MasterPackagingTypeEntity>> GetAll()
        {
            var PackagingTypeList = await _repository.GetAllAsync();
            return _mapperFactory.GetList<MasterPackagingType, MasterPackagingTypeEntity>(PackagingTypeList.ToList());
        }

        public async Task<MasterPackagingTypeEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterPackagingType, MasterPackagingTypeEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdatePackagingType(MasterPackagingTypeEntity entityPackagingType)
        {
            MasterPackagingType objPackagingType;
            if (entityPackagingType.PackagingTypeId > 0)
            {
                objPackagingType = _repository.Get(entityPackagingType.PackagingTypeId);
                if (objPackagingType != null)
                {
                    objPackagingType = _mapperFactory.Get<MasterPackagingTypeEntity, MasterPackagingType>(entityPackagingType);
                    _repository.UpdateAsync(objPackagingType);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objPackagingType = _mapperFactory.Get<MasterPackagingTypeEntity, MasterPackagingType>(entityPackagingType);
                _repository.AddAsync(objPackagingType);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objPackagingType.PackagingTypeId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeletePackagingType(int id)
        {
            var entityPackagingType = _repository.Get(x => x.PackagingTypeId == id);

            if (entityPackagingType == null)
                return DBOperation.NotFound;

            _repository.Remove(entityPackagingType);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}