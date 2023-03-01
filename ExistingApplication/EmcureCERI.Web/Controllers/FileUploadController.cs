using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.FileProviders;
using EmcureCERI.Web.Models.FileUpload;
using EmcureCERI.Business.Core.ServiceImplementations;
using EmcureCERI.Data.DataAccess.Entities;
using System;
using Newtonsoft.Json;

namespace EmcureCERI.Web.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IFileProvider fileProvider;
        private readonly IPatientService _patient;

        public FileUploadController(IPatientService patient, IFileProvider fileProvider)
        {
            this._patient = patient;
            this.fileProvider = fileProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/document",
                        file.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("RegisteredPatients", "Prescriber");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Content("files not selected");

            foreach (var file in files)
            {
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/document",
                        file.GetFilename());

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Files");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileViaModel(FileInputModel model)
        {
            if (model == null || model.FileToUpload == null || model.FileToUpload.Length == 0)
                return Json(new { result = "error", message = "File Not Selected" }, new JsonSerializerSettings());
            //return Content("file not selected");

            var filename = model.FileToUpload.GetFilename();

            string extension = System.IO.Path.GetExtension(filename);

            if (extension == ".pdf" || extension == ".PDF")
            {
                string result = filename.Substring(0, filename.Length - extension.Length);

                var finalName = result + DateTime.Now.ToString("yyyyMMddTHHmmss") + extension;

                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/document",
                            finalName);

                if (model.PatientId != 0)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.FileToUpload.CopyToAsync(stream);
                    }

                    PatientDetails uer = _patient.GetPatient(model.PatientId);
                    if (uer != null)
                    {
                        uer.PdfUploadDate = DateTime.Now;
                        uer.PdfName = finalName;
                        uer.IsConsentFcheckByHcp = true;
                        uer.PdfUploadBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        _patient.UpdatePatientDetails(uer);
                    }

                    return RedirectToAction("InformedConsentForm", "Prescriber", new { id = model.PatientId });

                }
                else
                {
                    return Json(new { result = "error", message = "Patient Not Selected" }, new JsonSerializerSettings());
                }

            }
            else
            {
                return Json(new { result = "error", message = "File Extension Is InValid - Only Upload PDF File" }, new JsonSerializerSettings());
            }

            //var supportedTypes = new[] { "txt", "doc", "docx", "pdf", "xls", "xlsx" };
            //var fileExt = System.IO.Path.GetExtension(filename).Substring(1);
            //if (!supportedTypes.Contains(fileExt))
            //{
            //    ErrorMessage = "File Extension Is InValid - Only Upload WORD/PDF/EXCEL/TXT File";
            //    return ErrorMessage;
            //}
            //Array supportedTypes = new[] { "txt", "doc", "docx", "pdf", "xls", "xlsx" };

            //string[] stringArray = { "pdf" };
            //if (stringArray) {

            //}


            //    if (!supportedTypes.C(extension))
            //{
            //    ErrorMessage = "File Extension Is InValid - Only Upload WORD/PDF/EXCEL/TXT File";
            //    return ErrorMessage;
            //}

            //if (extension == ".Pdf")




        }

        public IActionResult Files()
        {
            var model = new FilesViewModel();
            foreach (var item in this.fileProvider.GetDirectoryContents(""))
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, Path = item.PhysicalPath });
            }
            return View(model);
        }



        public async Task<IActionResult> DownloadById(int id)
        {
            if (id != 0)
            {
                var uer = _patient.GetPatient(id);
                if (uer.PdfName == null || uer.PdfName == "")
                {

                }
                else
                {
                    string filename = uer.PdfName;

                    if (filename == null)
                        return Content("filename not present");

                    var path = Path.Combine(
                                   Directory.GetCurrentDirectory(),
                                   "wwwroot/document", filename);

                    var memory = new MemoryStream();
                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    return File(memory, GetContentType(path), Path.GetFileName(path));
                }

            }
            return null;
        }

        public async Task<IActionResult> Download(string filename)
        {
            filename = "ravindra.jpg";
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/document", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}