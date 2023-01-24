using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterPackagingTypeService
    {
        Task<List<MasterPackagingTypeEntity>> GetAll();

        Task<MasterPackagingTypeEntity> GetById(int id);

        Task<DBOperation> AddUpdatePackagingType(MasterPackagingTypeEntity entityPackagingType);

        Task<DBOperation> DeletePackagingType(int id);
    }
}
