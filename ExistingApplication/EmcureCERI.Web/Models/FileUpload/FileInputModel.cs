using Microsoft.AspNetCore.Http;

namespace EmcureCERI.Web.Models.FileUpload
{
    public class FileInputModel
    {
        public int PatientId { get; set; }

        public IFormFile FileToUpload { get; set; }
    }
}
