using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterPackSizeService
    {
        Task<List<MasterPackSizeEntity>> GetAll();

        Task<MasterPackSizeEntity> GetById(int id);

        Task<DBOperation> AddUpdatePackSize(MasterPackSizeEntity entityPackSize);

        Task<DBOperation> DeletePackSize(int id);
    }
}