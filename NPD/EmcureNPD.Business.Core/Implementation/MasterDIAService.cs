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
    public class MasterDIAService : IMasterDIAService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterDium> _repository { get; set; }

        public MasterDIAService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterDium>();
        }

        public async Task<List<MasterDIAEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterDium, MasterDIAEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterDIAEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterDium, MasterDIAEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateDIA(MasterDIAEntity entityDIA)
        {
            MasterDium objDIA;
            if (entityDIA.DIAId > 0)
            {
                var objModelData = _repository.Exists(x => x.Dianame.ToLower() == entityDIA.DIAName.ToLower() && x.Diaid!= entityDIA.DIAId);
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objDIA = _repository.Get(entityDIA.DIAId);
                if (objDIA != null)
                {
                    objDIA = _mapperFactory.Get<MasterDIAEntity, MasterDium>(entityDIA);
                    _repository.UpdateAsync(objDIA);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.Dianame.ToLower() == entityDIA.DIAName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objDIA = _mapperFactory.Get<MasterDIAEntity, MasterDium>(entityDIA);
                _repository.AddAsync(objDIA);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objDIA.Diaid == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteDIA(int id)
        {
            var entityDIA = _repository.Get(x => x.Diaid == id);

            if (entityDIA == null)
                return DBOperation.NotFound;

            _repository.Remove(entityDIA);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}