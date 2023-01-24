using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IRoleModulePermission
    {
        Task<List<RoleModulePermissionEntity>> GetAll();

        Task<MasterRoleEntity> GetById(int id);

        Task<DBOperation> AddUpdateRoleModulePermission(List<RoleModulePermissionEntity> roleModulePermissionEntitys);

        Task<DBOperation> DeleteRoleModulePermission(int id);
    }
}
