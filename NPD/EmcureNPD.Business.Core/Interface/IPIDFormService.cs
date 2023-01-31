using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IPIDFormService
    {
        Task<PIDFormEntity> FillDropdown();
        Task<DBOperation> Medical(PIDFMedicalViewModel medicalModel);
        Task FileUpload(IFormFileCollection files, string path);
        public string FileValidation(IFormFile files);
        Task<DBOperation> AddUpdateIPD(PIDFormEntity entityIPD);
        Task<PIDFormEntity> GetIPDFormData(long pidfId, int buid);
        Task<DataTableResponseModel> GetAllIPDPIDFList(DataTableAjaxPostModel model);
        Task<IEnumerable<dynamic>> GetAllRegion(int userId);
        Task<IEnumerable<dynamic>> GetCountryRefByRegionIds(string regionIds);
        Task<DBOperation> ApproveRejectIpdPidf(EntryApproveRej oApprRej);

    }
}
