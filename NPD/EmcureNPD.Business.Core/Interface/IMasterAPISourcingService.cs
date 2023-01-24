using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterAPISourcingService
    {
        Task<List<MasterAPISourcingEntity>> GetAll();

        Task<MasterAPISourcingEntity> GetById(int id);

        Task<DBOperation> AddUpdateAPISourcing(MasterAPISourcingEntity entityAPISourcing);

        Task<DBOperation> DeleteAPISourcing(int id);
    }
}