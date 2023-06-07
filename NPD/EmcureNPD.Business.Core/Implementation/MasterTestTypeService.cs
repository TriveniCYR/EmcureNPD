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
    public class MasterTestTypeService : IMasterTestTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterTestType> _repository { get; set; }

        public MasterTestTypeService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterTestType>();
        }

        public async Task<List<MasterTestTypeEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterTestType, MasterTestTypeEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterTestTypeEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterTestType, MasterTestTypeEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateTestType(MasterTestTypeEntity entityTestType)
        {
            MasterTestType objTestType;
            if (entityTestType.TestTypeId > 0)
            {
                var objModelData = _repository.Exists(x => x.TestTypeName.ToLower() == entityTestType.TestTypeName.ToLower() && x.TestTypeId!= entityTestType.TestTypeId);
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objTestType = _repository.Get(entityTestType.TestTypeId);
                if (objTestType != null)
                {
                    objTestType = _mapperFactory.Get<MasterTestTypeEntity, MasterTestType>(entityTestType);
                    _repository.UpdateAsync(objTestType);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.TestTypeName.ToLower() == entityTestType.TestTypeName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objTestType = _mapperFactory.Get<MasterTestTypeEntity, MasterTestType>(entityTestType);
                _repository.AddAsync(objTestType);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objTestType.TestTypeId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteTestType(int id)
        {
            var entityTestType = _repository.Get(x => x.TestTypeId == id);

            if (entityTestType == null)
                return DBOperation.NotFound;

            _repository.Remove(entityTestType);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}