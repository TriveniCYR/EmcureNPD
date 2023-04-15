using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IPidfPbfRnD
    {
        Task<List<PidfPbfRnDEntity>> GetAll();

        Task<PidfPbfRnDEntity> GetById(int id);

        Task<DBOperation> AddUpdate(PidfPbfRnDEntity EntitypidfRnd);

        Task<dynamic> GetPidfPbfRnD(int PidfPbfRnDId = 0);
    }
}