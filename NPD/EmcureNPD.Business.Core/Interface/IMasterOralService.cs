using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterOralService
    {
        Task<List<MasterOralEntity>> GetAll();

        Task<MasterOralEntity> GetById(int id);

        Task<DBOperation> AddUpdateOral(MasterOralEntity entityOral);

        Task<DBOperation> DeleteOral(int id);
    }
}