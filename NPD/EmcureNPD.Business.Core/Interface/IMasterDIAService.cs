using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterDIAService
    {
        Task<List<MasterDIAEntity>> GetAll();

        Task<MasterDIAEntity> GetById(int id);

        Task<DBOperation> AddUpdateDIA(MasterDIAEntity entityDIA);

        Task<DBOperation> DeleteDIA(int id);
    }
}
