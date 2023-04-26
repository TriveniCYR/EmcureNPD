using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IPidfFinanceService
    {
        Task<List<PidfFinance>> GetAll();

        Task<PidfFinance> GetById(int id);

        Task<DBOperation> AddUpdatePidfFinance(FinanceModel EntitypidfFinance);

        Task<dynamic> GetPidfFinance(int Pidfid = 0);

        Task<dynamic> GetFinanceBatchSizeCoating(int PidffinaceId = 0);
        Task<dynamic> GetFinaceProjectionYear(int monthTobeDeduct = 0);
        Task<dynamic> GetPackSizeByStrengthId(int PidfId = 0, int Buid = 0, int StrengthId = 0);
        Task<dynamic> GetSUIMSVolumeYearWiseByPackSize(int PidfId = 0, int Buid = 0, int StrengthId = 0, int PackSize = 0);
     }
}