using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterBatchSizeNumberService
    {
        Task<List<MasterBatchSizeNumberEntity>> GetAll();

        Task<MasterBatchSizeNumberEntity> GetById(int id);

        Task<DBOperation> AddUpdateBatchSizeNumber(MasterBatchSizeNumberEntity entityBatchSizeNumber);

        Task<DBOperation> DeleteBatchSizeNumber(int id);
    }
}