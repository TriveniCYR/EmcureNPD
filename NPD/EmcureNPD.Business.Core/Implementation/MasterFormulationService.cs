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
    public class MasterFormulationService : IMasterFormulationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterFormulation> _repository { get; set; }
        private readonly IMasterAuditLogService _auditLogService;

        public MasterFormulationService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IMasterAuditLogService auditLogService)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _auditLogService = auditLogService;
            _repository = _unitOfWork.GetRepository<MasterFormulation>();
        }

        public async Task<List<MasterFormulationEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterFormulation, MasterFormulationEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterFormulationEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterFormulation, MasterFormulationEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateFormulation(MasterFormulationEntity entityFormulation)
        {
            MasterFormulation objFormulation;
            MasterFormulationEntity oldFormulationEntity;

            if (entityFormulation.FormulationId > 0)
            {
                var objModelData = _repository.Exists(x => x.FormulationName.ToLower() == entityFormulation.FormulationName.ToLower() && x.FormulationId!= entityFormulation.FormulationId);
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objFormulation = _repository.Get(entityFormulation.FormulationId);
                if (objFormulation != null)
                {
                    objFormulation = _mapperFactory.Get<MasterFormulationEntity, MasterFormulation>(entityFormulation);

                    _repository.UpdateAsync(objFormulation);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.FormulationName.ToLower() == entityFormulation.FormulationName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objFormulation = _mapperFactory.Get<MasterFormulationEntity, MasterFormulation>(entityFormulation);
                _repository.AddAsync(objFormulation);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objFormulation.FormulationId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteFormulation(int id)
        {
            var entityFormulation = _repository.Get(x => x.FormulationId == id);

            if (entityFormulation == null)
                return DBOperation.NotFound;

            _repository.Remove(entityFormulation);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}