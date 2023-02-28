using EmcureNPD.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterTestLicenseService
    {
        Task<List<MasterTestLicenseEntity>> GetAll();

        Task<MasterTestLicenseEntity> GetById(int id);

        Task<DBOperation> AddUpdateTestLicense(MasterTestLicenseEntity entityTestLicense);

        Task<DBOperation> DeleteTestLicense(int id);
    }
}
