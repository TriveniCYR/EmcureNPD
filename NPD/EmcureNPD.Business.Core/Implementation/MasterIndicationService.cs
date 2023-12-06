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
    public class MasterIndicationService : IMasterIndicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterIndication> _repository { get; set; }

        public MasterIndicationService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterIndication>();
        }

        public async Task<List<MasterIndicationEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterIndication, MasterIndicationEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterIndicationEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterIndication, MasterIndicationEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateIndication(MasterIndicationEntity entityIndication)
        {
			MasterIndication objIndication;
            if (entityIndication.IndicationId > 0)
            {
                var objModelData = _repository.Exists(x => x.IndicationName.ToLower() == entityIndication.IndicationName.ToLower() && x.IndicationId!= entityIndication.IndicationId);
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objIndication = _repository.Get(entityIndication.IndicationId);
                if (objIndication != null)
                {
                    objIndication = _mapperFactory.Get<MasterIndicationEntity, MasterIndication>(entityIndication);
                    _repository.UpdateAsync(objIndication);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.IndicationName.ToLower() == entityIndication.IndicationName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objIndication = _mapperFactory.Get<MasterIndicationEntity, MasterIndication>(entityIndication);
                _repository.AddAsync(objIndication);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objIndication.IndicationId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteIndication(int id)
        {
            var entityIndication = _repository.Get(x => x.IndicationId == id);

            if (entityIndication == null)
                return DBOperation.NotFound;

            _repository.Remove(entityIndication);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}