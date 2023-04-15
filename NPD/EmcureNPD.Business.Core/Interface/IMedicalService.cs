using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMedicalService
    {
        Task<DBOperation> Medical(PIDFMedicalViewModel medicalModel, IFormFileCollection files, string path);

        Task FileUpload(IFormFile files, string path, string uniqueFileName);

        public string FileValidation(IFormFile files);

        Task<PIDFMedicalViewModel> GetPIDFMedicalData(long pidfId);
    }
}