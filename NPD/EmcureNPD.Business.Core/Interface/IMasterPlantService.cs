using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterPlantService
    {
        Task<List<MasterPlantEntity>> GetAll();

        Task<MasterPlantEntity> GetById(int id);

        Task<DBOperation> AddUpdatePlant(MasterPlantEntity entityPlant);

        Task<DBOperation> DeletePlant(int id);
    }
}