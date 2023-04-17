using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterProductTypeService
    {
        Task<List<MasterProductTypeEntity>> GetAll();

        Task<MasterProductTypeEntity> GetById(int id);

        Task<DBOperation> AddUpdateProductType(MasterProductTypeEntity entityProductType);

        Task<DBOperation> DeleteProductType(int id);
    }
}