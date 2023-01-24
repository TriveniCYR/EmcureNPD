using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterFormRNDDivisionService
    {
        Task<List<MasterFormRNDDivisionEntity>> GetAll();

        Task<MasterFormRNDDivisionEntity> GetById(int id);

        Task<DBOperation> AddUpdateFormRNDDivision(MasterFormRNDDivisionEntity entityFormRNDDivision);

        Task<DBOperation> DeleteFormRNDDivision(int id);
    }
}