using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterCountryService
    {
        Task<List<MasterCountryEntity>> GetAll();

        Task<MasterCountryEntity> GetById(int id);

        Task<DBOperation> AddUpdateCountry(MasterCountryEntity entityCountry);

        Task<DBOperation> DeleteCountry(int id);
    }
}
