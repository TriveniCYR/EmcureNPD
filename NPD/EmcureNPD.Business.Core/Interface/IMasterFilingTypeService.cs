using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterFilingTypeService
    {
        Task<List<MasterFilingTypeEntity>> GetAll();

        Task<MasterFilingTypeEntity> GetById(int id);

        Task<DBOperation> AddUpdateFilingType(MasterFilingTypeEntity entityFilingType);

        Task<DBOperation> DeleteFilingType(int id);
    }
}