using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterCurrencyService
    {
        Task<List<MasterCurrencyEntity>> GetAll();

        Task<MasterCurrencyEntity> GetById(int id);

        Task<DBOperation> AddUpdateCurrency(MasterCurrencyEntity entityCurrency);

        Task<DBOperation> DeleteCurrency(int id);

        Task<MasterCountryEntity> GetCountryByCurrencyId(int id);

        Task<List<MasterCurrencyEntity>> GetCurrencyByLoggedInUser();
    }
}