using EmcureNPD.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterTestTypeService
    {
            Task<List<MasterTestTypeEntity>> GetAll();

            Task<MasterTestTypeEntity> GetById(int id);

            Task<DBOperation> AddUpdateTestType(MasterTestTypeEntity entityTestType);

            Task<DBOperation> DeleteTestType(int id);
    }
}
