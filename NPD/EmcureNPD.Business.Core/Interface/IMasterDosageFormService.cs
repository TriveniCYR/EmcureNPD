using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterDosageFormService
    {
        Task<List<MasterDosageFormEntity>> GetAll();

        Task<MasterDosageFormEntity> GetById(int id);

        Task<DBOperation> AddUpdateDosageForm(MasterDosageFormEntity entityDosageForm);

        Task<DBOperation> DeleteDosageForm(int id);
    }
}