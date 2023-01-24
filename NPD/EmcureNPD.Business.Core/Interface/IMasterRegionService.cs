using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterRegionService
    {
        Task<List<MasterRegionEntity>> GetAll();

        Task<MasterRegionEntity> GetById(int id);

        Task<DBOperation> AddUpdateRegion(MasterRegionEntity entityRegion);

        Task<DBOperation> DeleteRegion(int id);
        Task<MasterCountryEntity> GetCountryByRegionId(int id);
    }
}
