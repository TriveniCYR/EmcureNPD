using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterManufacturingService
    {
        Task<List<MasterManufacturingEntity>> GetAll();

        Task<MasterManufacturingEntity> GetById(int id);

        Task<DBOperation> AddUpdateManufacturing(MasterManufacturingEntity entityManufacturing);

        Task<DBOperation> DeleteManufacturing(int id);
    }
}