using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterBERequirementService
    {
        Task<List<MasterBERequirementEntity>> GetAll();

        Task<MasterBERequirementEntity> GetById(int id);

        Task<DBOperation> AddUpdateBERequirement(MasterBERequirementEntity entityBERequirement);

        Task<DBOperation> DeleteBERequirement(int id);
    }
}
