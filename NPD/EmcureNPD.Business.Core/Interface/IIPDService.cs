using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IIPDService
    {
        Task<IPDEntity> FillDropdown();

        //Task<DBOperation> Medical(PIDFMedicalViewModel medicalModel,IFormFileCollection files, string path);
        Task FileUpload(IFormFile files, string path, string uniqueFileName);

        public string FileValidation(IFormFile files);

        Task<PIDFMedicalViewModel> GetPIDFMedicalData(long pidfId);

        Task<DBOperation> AddUpdateIPD(IPDEntity entityIPD);

        Task<IPDEntity> GetIPDFormData(long pidfId, int buid);

        Task<DataTableResponseModel> GetAllIPDPIDFList(DataTableAjaxPostModel model);

        Task<IEnumerable<dynamic>> GetAllRegion(int userId);

        Task<IEnumerable<dynamic>> GetCountryRefByRegionIds(string regionIds);

        Task<DBOperation> ApproveRejectIpdPidf(EntryApproveRej oApprRej);
        Task<dynamic> GetCountryByBussinessUnitIds(string BUId);
        Task<dynamic> GetPatentStrategy();
    }
}