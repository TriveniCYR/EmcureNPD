using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterTransformFormService
    {
        Task<List<TransformFormEntity>> GetAll();

        Task<TransformFormEntity> GetById(int id);

        Task<DBOperation> AddUpdateTransformForm(TransformFormEntity entityTransformForm);

        Task<DBOperation> DeleteTransformForm(int id);
    }
}