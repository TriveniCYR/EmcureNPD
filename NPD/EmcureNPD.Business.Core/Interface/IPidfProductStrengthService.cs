using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IPidfProductStrengthService
    {
        Task<List<PidfProductStregthEntity>> GetAll();

        Task<PidfProductStregthEntity> GetById(int id);
        Task<List<PidfProductStregthEntity>> GetStrengthByPIDFId(long pidfid);
    }
}