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
    public class MasterAPISourcingService : IMasterAPISourcingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterApisourcing> _repository { get; set; }

        public MasterAPISourcingService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterApisourcing>();
        }

        public async Task<List<MasterAPISourcingEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterApisourcing, MasterAPISourcingEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterAPISourcingEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterApisourcing, MasterAPISourcingEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateAPISourcing(MasterAPISourcingEntity entityAPISourcing)
        {
            MasterApisourcing objAPISourcing;
            if (entityAPISourcing.APISourcingId > 0)
            {
                objAPISourcing = _repository.Get(entityAPISourcing.APISourcingId);
                if (objAPISourcing != null)
                {
                    objAPISourcing = _mapperFactory.Get<MasterAPISourcingEntity, MasterApisourcing>(entityAPISourcing);
                    _repository.UpdateAsync(objAPISourcing);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objAPISourcing = _mapperFactory.Get<MasterAPISourcingEntity, MasterApisourcing>(entityAPISourcing);
                _repository.AddAsync(objAPISourcing);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objAPISourcing.ApisourcingId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteAPISourcing(int id)
        {
            var entityAPISourcing = _repository.Get(x => x.ApisourcingId == id);

            if (entityAPISourcing == null)
                return DBOperation.NotFound;

            _repository.Remove(entityAPISourcing);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}