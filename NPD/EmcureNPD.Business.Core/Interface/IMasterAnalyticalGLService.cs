using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterAnalyticalGLService
    {
        Task<List<MasterAnalyticalGLEntity>> GetAll();

        Task<MasterAnalyticalGLEntity> GetById(int id);

        Task<DBOperation> AddUpdateAnalyticalGL(MasterAnalyticalGLEntity entityAnalyticalGL);

        Task<DBOperation> DeleteAnalyticalGL(int id);
    }
}