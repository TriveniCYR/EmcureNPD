using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterRoleService
    {
        Task<List<MasterRoleEntity>> GetAll();

        Task<MasterRoleEntity> GetById(int id);

        Task<DBOperation> AddUpdateRole(MasterRoleEntity masterRoleEntity);

        Task<DBOperation> DeleteRole(int id);
        Task<List<MasterRoleEntity>> GetActiveRole();
    }
}
