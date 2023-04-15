using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IPidfApiDetailsService
    {
        Task<List<PidfApiDetailEntity>> GetAll();

        Task<PidfApiDetailEntity> GetById(int id);
    }
}