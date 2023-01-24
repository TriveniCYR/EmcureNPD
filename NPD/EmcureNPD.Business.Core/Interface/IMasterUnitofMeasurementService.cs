using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterUnitofMeasurementService
    {
        Task<List<MasterUnitofMeasurementEntity>> GetAll();

        Task<MasterUnitofMeasurementEntity> GetById(int id);

        Task<DBOperation> AddUpdateUnitofMeasurement(MasterUnitofMeasurementEntity entityUnitofMeasurement);

        Task<DBOperation> DeleteUnitofMeasurement(int id);
    }
}
