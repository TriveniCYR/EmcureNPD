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
    public class MasterFilingTypeService : IMasterFilingTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterFilingType> _repository { get; set; }

        public MasterFilingTypeService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterFilingType>();
        }

        public async Task<List<MasterFilingTypeEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterFilingType, MasterFilingTypeEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterFilingTypeEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterFilingType, MasterFilingTypeEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateFilingType(MasterFilingTypeEntity entityFilingType)
        {
            MasterFilingType objFilingType;
            if (entityFilingType.FilingTypeId > 0)
            {
                var objModelData = _repository.Exists(x => x.FilingTypeName.ToLower() == entityFilingType.FilingTypeName.ToLower() && x.FilingTypeId!= entityFilingType.FilingTypeId);
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objFilingType = _repository.Get(entityFilingType.FilingTypeId);
                if (objFilingType != null)
                {
                    objFilingType = _mapperFactory.Get<MasterFilingTypeEntity, MasterFilingType>(entityFilingType);
                    _repository.UpdateAsync(objFilingType);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.FilingTypeName.ToLower() == entityFilingType.FilingTypeName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objFilingType = _mapperFactory.Get<MasterFilingTypeEntity, MasterFilingType>(entityFilingType);
                _repository.AddAsync(objFilingType);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objFilingType.FilingTypeId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteFilingType(int id)
        {
            var entityFilingType = _repository.Get(x => x.FilingTypeId == id);

            if (entityFilingType == null)
                return DBOperation.NotFound;

            _repository.Remove(entityFilingType);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}