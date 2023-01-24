using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterDepartmentService
    {
        Task<List<MasterDepartmentEntity>> GetAll();

        Task<MasterDepartmentEntity> GetById(int id);

        Task<DBOperation> AddUpdateDepartment(MasterDepartmentEntity entityDepartment);

        Task<DBOperation> DeleteDepartment(int id);
        Task<List<MasterDepartmentEntity>> GetAllActiveDepartment();
        Task<MasterBusinessUnitEntity> GetBusinessUnitByDepartmentId(int id);
    }
}
