using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterBusinessUnitService
    {
        Task<List<MasterBusinessUnitEntity>> GetAll();

        Task<MasterBusinessUnitEntity> GetById(int id);

        Task<DBOperation> AddUpdateBusinessUnit(MasterBusinessUnitEntity entityBusinessUnit);

        Task<DBOperation> DeleteBusinessUnit(int id);
        Task<MasterCountryEntity> GetCountryByBusinessUnitId(int id);
    }
}
