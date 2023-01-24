using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterWorkflowService
    {
        Task<List<MasterWorkflowEntity>> GetAll();

        Task<MasterWorkflowEntity> GetById(int id);

        Task<DBOperation> AddUpdateWorkflow(MasterWorkflowEntity entityWorkflow);

        Task<DBOperation> DeleteWorkflow(int id);
    }
}
