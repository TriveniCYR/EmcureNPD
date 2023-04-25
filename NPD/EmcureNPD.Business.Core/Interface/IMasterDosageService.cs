using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterDosageService
    {
        Task<List<MasterDosageEntity>> GetAll();

        Task<List<MasterDosageEntity>> GetAllActiveDosageFinance();

        Task<MasterDosageEntity> GetById(int id);

        Task<DBOperation> AddUpdateDosage(MasterDosageEntity entityDosage);

        Task<DBOperation> DeleteDosage(int id);
    }
}