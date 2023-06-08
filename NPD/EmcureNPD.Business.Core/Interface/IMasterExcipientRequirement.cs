using EmcureNPD.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterExcipientRequirement
    {
        Task<List<MasterExcipientRequirementEntity>> GetAll();

        Task<MasterExcipientRequirementEntity> GetById(long id);

        Task<DBOperation> AddUpdateExcipientRequirement(MasterExcipientRequirementEntity entityExcipientRequirement);

        Task<DBOperation> DeleteExcipientRequirement(int id);
    }
}
