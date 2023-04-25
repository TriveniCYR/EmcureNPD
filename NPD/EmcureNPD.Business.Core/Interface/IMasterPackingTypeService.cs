using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterPackingTypeService
    {
        Task<List<MasterPackingTypeEntity>> GetAll();

        Task<MasterPackingTypeEntity> GetById(int id);

        Task<DBOperation> AddUpdatePackingType(MasterPackingTypeEntity entityPackingType);

        Task<DBOperation> DeletePackingType(int id);
    }
}