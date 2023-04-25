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
    public class MasterPackSizeService : IMasterPackSizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterPackSize> _repository { get; set; }

        public MasterPackSizeService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterPackSize>();
        }

        public async Task<List<MasterPackSizeEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterPackSize, MasterPackSizeEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterPackSizeEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterPackSize, MasterPackSizeEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdatePackSize(MasterPackSizeEntity entityPackSize)
        {
            MasterPackSize objPackSize;
            if (entityPackSize.PackSizeId > 0)
            {
                objPackSize = _repository.Get(entityPackSize.PackSizeId);
                if (objPackSize != null)
                {
                    objPackSize = _mapperFactory.Get<MasterPackSizeEntity, MasterPackSize>(entityPackSize);
                    _repository.UpdateAsync(objPackSize);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objPackSize = _mapperFactory.Get<MasterPackSizeEntity, MasterPackSize>(entityPackSize);
                _repository.AddAsync(objPackSize);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objPackSize.PackSizeId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeletePackSize(int id)
        {
            var entityPackSize = _repository.Get(x => x.PackSizeId == id);

            if (entityPackSize == null)
                return DBOperation.NotFound;

            _repository.Remove(entityPackSize);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}