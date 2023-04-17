using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class MedicalService : IMedicalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IMasterAuditLogService _auditLogService;
        private readonly IConfiguration _configuration;
        private readonly INotificationService _notificationService;

        private IRepository<PidfMedical> _pidfMedicalrepository { get; set; }
        private IRepository<PidfMedicalFile> _pidfMedicalFilerepository { get; set; }
        private readonly IHelper _helper;

        public MedicalService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IMasterAuditLogService auditLogService, IConfiguration configuration, INotificationService notificationService, IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _auditLogService = auditLogService;
            _notificationService = notificationService;
            _helper = helper;
            _pidfMedicalrepository = unitOfWork.GetRepository<PidfMedical>();
            _pidfMedicalFilerepository = unitOfWork.GetRepository<PidfMedicalFile>();
            _configuration = configuration;
        }

        public async Task<DBOperation> Medical(PIDFMedicalViewModel medicalModel, IFormFileCollection files, string path)
        {
            PidfMedical objPIDFMedical;
            PidfMedicalFile objPIDFMedicalFile;
            PIDFMedicalViewModel oldPIDFFEntity;
            if (medicalModel.PidfmedicalId > 0)
            {
                objPIDFMedical = await _pidfMedicalrepository.GetAsync(medicalModel.PidfmedicalId);
                if (objPIDFMedical != null)
                {
                    int i = 0;
                    bool validFile = false;
                    oldPIDFFEntity = _mapperFactory.Get<PidfMedical, PIDFMedicalViewModel>(objPIDFMedical);
                    objPIDFMedical = _mapperFactory.Get<PIDFMedicalViewModel, PidfMedical>(medicalModel);
                    if (files.Count() != 0)
                    {
                        foreach (var item in files)
                        {
                            string us = FileValidation(files[i]);
                            if (us == null)
                                validFile = true;
                            else
                                validFile = false;
                            i++;
                        }
                        if (validFile == true)
                        {
                            _pidfMedicalrepository.UpdateAsync(objPIDFMedical);
                            await _unitOfWork.SaveChangesAsync();
                        }
                        else
                        {
                            return DBOperation.InvalidFile;
                        }
                    }
                    else if (files.Count() == 0 && medicalModel.FileName.Length != 0)
                    {
                        _pidfMedicalrepository.UpdateAsync(objPIDFMedical);
                        await _unitOfWork.SaveChangesAsync();
                    }
                    else
                    {
                        return DBOperation.Error;
                    }
                    var MedicalFile = _pidfMedicalFilerepository.GetAllQuery().Where(x => x.PidfmedicalId == medicalModel.PidfmedicalId).ToList();
                    foreach (var item in MedicalFile)
                    {
                        var fullPath = path + "\\" + item.FileName;
                        var previousFileName = "Medical\\" + item.FileName;
                        if (!medicalModel.FileName.Contains(previousFileName))
                        {
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            PidfMedicalFile medicalFile = new PidfMedicalFile
                            {
                                PidfmedicalFileId = Convert.ToInt64(item.PidfmedicalFileId),
                            };
                            _pidfMedicalFilerepository.Remove(medicalFile);
                        }
                    }
                    if (medicalModel.FileName.Count() > 0)
                    {
                        int iteration = 0;
                        foreach (var filename in medicalModel.FileName)
                        {
                            var uniqueFileName = Path.GetFileNameWithoutExtension(medicalModel.FileName[iteration])
                                   + Guid.NewGuid().ToString().Substring(0, 4)
                                   + Path.GetExtension(medicalModel.FileName[iteration]);
                            PidfMedicalFile medicalFiles = new PidfMedicalFile
                            {
                                FileName = uniqueFileName,
                                CreatedDate = DateTime.Now,
                                PidfmedicalId = Convert.ToInt64(objPIDFMedical.PidfmedicalId),
                                CreatedBy = (int)objPIDFMedical.CreatedBy,
                            };
                            if (files.Count() != 0 && iteration < files.Count())
                            {
                                await FileUpload(files[iteration], path, uniqueFileName);
                                _pidfMedicalFilerepository.AddAsync(medicalFiles);
                                await _unitOfWork.SaveChangesAsync();
                            }
                            iteration++;
                        }
                    }
                    await _auditLogService.UpdatePIDFStatusCommon(medicalModel.Pidfid, (int)Master_PIDFStatus.MedicalSubmitted, medicalModel.CreatedBy);
                    await _notificationService.CreateNotification((int)medicalModel.Pidfid, (int)Master_PIDFStatus.MedicalSubmitted, string.Empty, string.Empty, medicalModel.CreatedBy);

                    var isSuccess = await _auditLogService.CreateAuditLog<PIDFMedicalViewModel>(medicalModel.PidfmedicalId > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
                           Utility.Enums.ModuleEnum.Medical, oldPIDFFEntity, medicalModel, Convert.ToInt32(objPIDFMedical.PidfmedicalId));
                    return DBOperation.Success;
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else if (medicalModel.PidfmedicalId == 0)
            {
                int i = 0;
                bool validFile = false;
                var medical = _mapperFactory.Get<PIDFMedicalViewModel, PidfMedical>(medicalModel);
                medical.MedicalOpinion = medicalModel.MedicalOpinion;
                medical.Remark = medicalModel.Remark;
                medical.CreatedDate = DateTime.Now;
                if (files.Count() != 0)
                {
                    foreach (var item in files)
                    {
                        string us = FileValidation(files[i]);
                        if (us == null)
                            validFile = true;
                        else
                            validFile = false;
                        i++;
                    }
                    if (validFile == true)
                    {
                        _pidfMedicalrepository.AddAsync(medical);
                        await _unitOfWork.SaveChangesAsync();
                    }
                    else
                    {
                        return DBOperation.InvalidFile;
                    }
                }
                else if (files.Count() == 0)
                {
                    return DBOperation.Error;
                }
                else
                    return DBOperation.Error;

                if (medicalModel.FileName != null)
                {
                    int iteration = 0;
                    foreach (var filename in medicalModel.FileName)
                    {
                        var uniqueFileName = Path.GetFileNameWithoutExtension(filename)
                                   + Guid.NewGuid().ToString().Substring(0, 4)
                                   + Path.GetExtension(filename);
                        PidfMedicalFile medicalFiles = new PidfMedicalFile
                        {
                            FileName = uniqueFileName,
                            CreatedDate = DateTime.Now,
                            PidfmedicalId = Convert.ToInt64(medical.PidfmedicalId),
                            CreatedBy = (int)medical.CreatedBy,
                        };
                        if (files.Count() != 0)
                        {
                            if (validFile == true)
                            {
                                await FileUpload(files[iteration], path, uniqueFileName);
                                _pidfMedicalFilerepository.AddAsync(medicalFiles);
                                await _unitOfWork.SaveChangesAsync();
                            }
                            else
                                return DBOperation.InvalidFile;
                        }
                        iteration++;
                    }
                }
                await _auditLogService.UpdatePIDFStatusCommon(medicalModel.Pidfid, (int)Master_PIDFStatus.MedicalSubmitted, medicalModel.CreatedBy);

                await _notificationService.CreateNotification((int)medicalModel.Pidfid, (int)Master_PIDFStatus.MedicalSubmitted, string.Empty, string.Empty, medicalModel.CreatedBy);

                return DBOperation.Success;
            }
            else
            {
                return DBOperation.Error;
            }
        }

        public async Task FileUpload(IFormFile files, string path, string uniqueFileName)
        {
            if (files != null)
            {
                string uploadFolder = path;
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                var filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }
            }
        }

        public string FileValidation(IFormFile file)
        {
            PIDFMedicalViewModel fileUpload = new PIDFMedicalViewModel();
            fileUpload.FileSize = Convert.ToInt32(_configuration.GetSection("FileUploadSettings").GetSection("MaxFileSizeMb").Value);
            try
            {
                var supportedTypes = _configuration.GetSection("FileUploadSettings").GetSection("AllowedFileExtension").Value;
                var fileTypes = supportedTypes.Split(',');
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!fileTypes.Contains(fileExt))
                {
                    fileUpload.ErrorMessage = _configuration.GetSection("FileUploadSettings").GetSection("FileNotAllowedErrorMessage").Value;
                }
                else if (file.Length > (fileUpload.FileSize * 1024 * 1024))
                {
                    fileUpload.ErrorMessage = _configuration.GetSection("FileUploadSettings").GetSection("FileSizeExceedErrorMessage").Value;
                }
                else
                {
                    fileUpload.ErrorMessage = null;
                }
                return fileUpload.ErrorMessage;
            }
            catch (Exception ex)
            {
                fileUpload.ErrorMessage = "Upload Container Should Not Be Empty or Contact Admin";
                return fileUpload.ErrorMessage;
            }
        }

        public async Task<PIDFMedicalViewModel> GetPIDFMedicalData(long pidfId)
        {
            Expression<Func<PidfMedical, bool>> expr = u => u.Pidfid == pidfId;
            dynamic objData = (dynamic)await _pidfMedicalrepository.FindAllAsync(expr);
            PIDFMedicalViewModel data = new PIDFMedicalViewModel();
            if (objData != null && objData.Count > 0)
            {
                data = _mapperFactory.Get<PidfMedical, PIDFMedicalViewModel>(objData[0]);
                var medicalFileData = _pidfMedicalFilerepository.GetAllQuery().Where(x => x.PidfmedicalId == data.PidfmedicalId).ToList();
                int i = 0;
                data.FileName = new string[medicalFileData.Count];
                foreach (var item in medicalFileData)
                {
                    data.PidfmedicalId = item.PidfmedicalId;
                    data.PidfmedicalFileId = item.PidfmedicalFileId;
                    data.FileName[i] = item.FileName;
                    i++;
                }
            }
            return data;
        }
    }
}