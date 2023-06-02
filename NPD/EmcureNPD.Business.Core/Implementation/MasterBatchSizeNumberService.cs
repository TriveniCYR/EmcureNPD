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
    public class MasterBatchSizeNumberService : IMasterBatchSizeNumberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterBatchSizeNumber> _repository { get; set; }

        public MasterBatchSizeNumberService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterBatchSizeNumber>();
        }

        public async Task<List<MasterBatchSizeNumberEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterBatchSizeNumber, MasterBatchSizeNumberEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterBatchSizeNumberEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterBatchSizeNumber, MasterBatchSizeNumberEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateBatchSizeNumber(MasterBatchSizeNumberEntity entityBatchSizeNumber)
        {
            MasterBatchSizeNumber objBatchSizeNumber;
            if (entityBatchSizeNumber.BatchSizeNumberId > 0)
            {
                var objModelData = _repository.Exists(x => x.BatchSizeNumberName.ToLower() == entityBatchSizeNumber.BatchSizeNumberName.ToLower() && x.BatchSizeNumberId!= entityBatchSizeNumber.BatchSizeNumberId);
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objBatchSizeNumber = _repository.Get(entityBatchSizeNumber.BatchSizeNumberId);
                if (objBatchSizeNumber != null)
                {
                    objBatchSizeNumber = _mapperFactory.Get<MasterBatchSizeNumberEntity, MasterBatchSizeNumber>(entityBatchSizeNumber);
                    _repository.UpdateAsync(objBatchSizeNumber);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.BatchSizeNumberName.ToLower() == entityBatchSizeNumber.BatchSizeNumberName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objBatchSizeNumber = _mapperFactory.Get<MasterBatchSizeNumberEntity, MasterBatchSizeNumber>(entityBatchSizeNumber);
                _repository.AddAsync(objBatchSizeNumber);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objBatchSizeNumber.BatchSizeNumberId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteBatchSizeNumber(int id)
        {
            var entityBatchSizeNumber = _repository.Get(x => x.BatchSizeNumberId == id);

            if (entityBatchSizeNumber == null)
                return DBOperation.NotFound;

            _repository.Remove(entityBatchSizeNumber);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}