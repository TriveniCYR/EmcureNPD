using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterExtensionApplicationService
    {
        Task<List<MasterExtensionApplicationEntity>> GetAll();

        Task<MasterExtensionApplicationEntity> GetById(int id);

        Task<DBOperation> AddUpdateExtensionApplication(MasterExtensionApplicationEntity entityExtensionApplication);

        Task<DBOperation> DeleteExtensionApplication(int id);
    }
}