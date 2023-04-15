using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterProductStrengthService
    {
        Task<List<MasterProductStrengthEntity>> GetAll();

        Task<MasterProductStrengthEntity> GetById(int id);

        Task<DBOperation> AddUpdateProductStrength(MasterProductStrengthEntity entityProductStrength);

        Task<DBOperation> DeleteProductStrength(int id);
    }
}