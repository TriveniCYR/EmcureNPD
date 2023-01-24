using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterMarketExtensionService
    {
        Task<List<MarketExtensionEntity>> GetAll();

        Task<MarketExtensionEntity> GetById(int id);

        Task<DBOperation> AddUpdateMarketExtension(MarketExtensionEntity entityMarketExtension);

        Task<DBOperation> DeleteMarketExtension(int id);
    }
}