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
    public class MasterWorkflowService : IMasterWorkflowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterWorkflow> _repository { get; set; }

        public MasterWorkflowService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterWorkflow>();
        }

        public async Task<List<MasterWorkflowEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterWorkflow, MasterWorkflowEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterWorkflowEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterWorkflow, MasterWorkflowEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateWorkflow(MasterWorkflowEntity entityWorkflow)
        {
            MasterWorkflow objWorkflow;
            if (entityWorkflow.WorkflowId > 0)
            {
                objWorkflow = _repository.Get(entityWorkflow.WorkflowId);
                if (objWorkflow != null)
                {
                    objWorkflow = _mapperFactory.Get<MasterWorkflowEntity, MasterWorkflow>(entityWorkflow);
                    _repository.UpdateAsync(objWorkflow);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.WorkflowName.ToLower() == entityWorkflow.WorkflowName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objWorkflow = _mapperFactory.Get<MasterWorkflowEntity, MasterWorkflow>(entityWorkflow);
                _repository.AddAsync(objWorkflow);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objWorkflow.WorkflowId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteWorkflow(int id)
        {
            var entityWorkflow = _repository.Get(x => x.WorkflowId == id);

            if (entityWorkflow == null)
                return DBOperation.NotFound;

            _repository.Remove(entityWorkflow);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}