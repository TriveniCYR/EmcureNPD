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
    public class MasterManufacturingService : IMasterManufacturingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterManufacturing> _repository { get; set; }

        public MasterManufacturingService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterManufacturing>();
        }

        public async Task<List<MasterManufacturingEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterManufacturing, MasterManufacturingEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterManufacturingEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterManufacturing, MasterManufacturingEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateManufacturing(MasterManufacturingEntity entityManufacturing)
        {
            MasterManufacturing objManufacturing;
            if (entityManufacturing.ManufacturingId > 0)
            {
                objManufacturing = _repository.Get(entityManufacturing.ManufacturingId);
                if (objManufacturing != null)
                {
                    objManufacturing = _mapperFactory.Get<MasterManufacturingEntity, MasterManufacturing>(entityManufacturing);
                    _repository.UpdateAsync(objManufacturing);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.ManufacturingName.ToLower() == entityManufacturing.ManufacturingName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objManufacturing = _mapperFactory.Get<MasterManufacturingEntity, MasterManufacturing>(entityManufacturing);
                _repository.AddAsync(objManufacturing);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objManufacturing.ManufacturingId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteManufacturing(int id)
        {
            var entityManufacturing = _repository.Get(x => x.ManufacturingId == id);

            if (entityManufacturing == null)
                return DBOperation.NotFound;

            _repository.Remove(entityManufacturing);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}