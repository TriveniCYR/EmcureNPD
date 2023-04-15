using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterModuleService
    {
        Task<List<MasterModuleEntity>> GetAll();

        Task<List<MasterModuleEntity>> GetByRoleId(int roleId);

        Task<IEnumerable<dynamic>> GetByPermisionRoleUsingRoleId(int roleId);
    }
}