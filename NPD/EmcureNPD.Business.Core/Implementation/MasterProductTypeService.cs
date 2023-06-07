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
    public class MasterProductTypeService : IMasterProductTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterProductType> _repository { get; set; }

        public MasterProductTypeService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterProductType>();
        }

        public async Task<List<MasterProductTypeEntity>> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return _mapperFactory.GetList<MasterProductType, MasterProductTypeEntity>(list.ToList());
        }

        public async Task<MasterProductTypeEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterProductType, MasterProductTypeEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateProductType(MasterProductTypeEntity entityProductType)
        {
            MasterProductType objProductType;
            if (entityProductType.ProductTypeId > 0)
            {
                var objModelData = _repository.Exists(x => x.ProductTypeName.ToLower() == entityProductType.ProductTypeName.ToLower() && x.ProductTypeId!= entityProductType.ProductTypeId);
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objProductType = _repository.Get(entityProductType.ProductTypeId);
                if (objProductType != null)
                {
                    objProductType = _mapperFactory.Get<MasterProductTypeEntity, MasterProductType>(entityProductType);
                    _repository.UpdateAsync(objProductType);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.ProductTypeName.ToLower() == entityProductType.ProductTypeName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objProductType = _mapperFactory.Get<MasterProductTypeEntity, MasterProductType>(entityProductType);
                _repository.AddAsync(objProductType);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objProductType.ProductTypeId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteProductType(int id)
        {
            var entityProductType = _repository.Get(x => x.ProductTypeId == id);

            if (entityProductType == null)
                return DBOperation.NotFound;

            _repository.Remove(entityProductType);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}