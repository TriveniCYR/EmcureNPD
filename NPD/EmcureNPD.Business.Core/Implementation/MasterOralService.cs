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
    public class MasterOralService : IMasterOralService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterOral> _repository { get; set; }

        public MasterOralService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterOral>();
        }

        public async Task<List<MasterOralEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterOral, MasterOralEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterOralEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterOral, MasterOralEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateOral(MasterOralEntity entityOral)
        {
            MasterOral objOral;
            if (entityOral.OralId > 0)
            {
                objOral = _repository.Get(entityOral.OralId);
                if (objOral != null)
                {
                    objOral = _mapperFactory.Get<MasterOralEntity, MasterOral>(entityOral);
                    _repository.UpdateAsync(objOral);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objOral = _mapperFactory.Get<MasterOralEntity, MasterOral>(entityOral);
                _repository.AddAsync(objOral);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objOral.OralId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteOral(int id)
        {
            var entityOral = _repository.Get(x => x.OralId == id);

            if (entityOral == null)
                return DBOperation.NotFound;

            _repository.Remove(entityOral);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}