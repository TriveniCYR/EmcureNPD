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
    public class MasterExipientService : IMasterExipientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterExipient> _repository { get; set; }

        public MasterExipientService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterExipient>();
        }

        public async Task<List<MasterExipientEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterExipient, MasterExipientEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterExipientEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterExipient, MasterExipientEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateExipient(MasterExipientEntity entityExipient)
        {
            MasterExipient objExipient;
            if (entityExipient.ExipientId > 0)
            {
                objExipient = _repository.Get(entityExipient.ExipientId);
                if (objExipient != null)
                {
                    objExipient = _mapperFactory.Get<MasterExipientEntity, MasterExipient>(entityExipient);
                    _repository.UpdateAsync(objExipient);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objExipient = _mapperFactory.Get<MasterExipientEntity, MasterExipient>(entityExipient);
                _repository.AddAsync(objExipient);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objExipient.ExipientId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteExipient(int id)
        {
            var entityExipient = _repository.Get(x => x.ExipientId == id);

            if (entityExipient == null)
                return DBOperation.NotFound;

            _repository.Remove(entityExipient);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}