using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterFormulationService
    {
        Task<List<MasterFormulationEntity>> GetAll();

        Task<MasterFormulationEntity> GetById(int id);

        Task<DBOperation> AddUpdateFormulation(MasterFormulationEntity entityFormulation);

        Task<DBOperation> DeleteFormulation(int id);
    }
}