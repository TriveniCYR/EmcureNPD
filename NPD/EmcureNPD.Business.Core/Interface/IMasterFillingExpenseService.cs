using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterFillingExpenseService
    {
        Task<List<MasterFillingExpenseEntity>> GetAll();

        Task<MasterFillingExpenseEntity> GetById(int id);

        Task<DBOperation> AddUpdateFillingExpense(MasterFillingExpenseEntity entityFillingExpense);

        Task<DBOperation> DeleteFillingExpense(int id);
    }
}