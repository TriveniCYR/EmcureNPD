using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Data;
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

        Task<MasterRegionEntity> GetRegionByBusinessUnitId(int id);

        Task<dynamic> GetCountryByBusinessUnitId(int BusinessUnitId);

        DataTable GetActiveBusinessUnit();
    }
}