using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterExipientService
    {
        Task<List<MasterExipientEntity>> GetAll();

        Task<MasterExipientEntity> GetById(int id);

        Task<DBOperation> AddUpdateExipient(MasterExipientEntity entityExipient);

        Task<DBOperation> DeleteExipient(int id);
    }
}