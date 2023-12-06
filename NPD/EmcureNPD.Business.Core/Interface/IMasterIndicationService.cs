using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterIndicationService
	{
        Task<List<MasterIndicationEntity>> GetAll();

        Task<MasterIndicationEntity> GetById(int id);

        Task<DBOperation> AddUpdateIndication(MasterIndicationEntity entityIndication);

        Task<DBOperation> DeleteIndication(int id);
    }
}