using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterActivityTypeService
    {
        Task<List<MasterActivityTypeEntity>> GetAll();

        Task<MasterActivityTypeEntity> GetById(int id);

        Task<DBOperation> AddUpdateActivityType(MasterActivityTypeEntity entityActivityType);

        Task<DBOperation> DeleteActivityType(int id);
    }
}