using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using static EmcureNPD.Utility.Enums.GeneralEnum;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

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
			//_businessUnitService = businessUnitService;
			//_countryService = countryService;
            _auditLogService = auditLogService;
            _notificationService = notificationService;
			_helper = helper;

   //         _repository = _unitOfWork.GetRepository<PidfIpd>();
			//_ipdParentRepository = unitOfWork.GetRepository<PidfIpdPatentDetail>();
			//_regionRepository = _unitOfWork.GetRepository<MasterRegion>();
			//_userRegionRepository = _unitOfWork.GetRepository<MasterUserRegionMapping>();
			//_userRegionCountryRepository = _unitOfWork.GetRepository<MasterRegionCountryMapping>();
			//_ipdRegionRepository = unitOfWork.GetRepository<PidfIpdRegion>();
			//_ipdCountryRepository = unitOfWork.GetRepository<PidfIpdCountry>();
			//_pidfrepository = unitOfWork.GetRepository<Pidf>();
			_pidfMedicalrepository = unitOfWork.GetRepository<PidfMedical>();
			_pidfMedicalFilerepository = unitOfWork.GetRepository<PidfMedicalFile>();

			//_projectTaskRepository = unitOfWork.GetRepository<ProjectTask>();
			//_masterUserRepository = unitOfWork.GetRepository<MasterUser>();
			//_masterProjectStatusRepository = unitOfWork.GetRepository<MasterProjectStatus>();
			//_masterProjectPriorityRepository = unitOfWork.GetRepository<MasterProjectPriority>();

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
					oldPIDFFEntity = _mapperFactory.Get<PidfMedical, PIDFMedicalViewModel>(objPIDFMedical);
					objPIDFMedical = _mapperFactory.Get<PIDFMedicalViewModel, PidfMedical>(medicalModel);
					if (files.Count() != 0)
					{
						string us = FileValidation(files[i]);
						if (us == null)
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
					var MedicalFile = _pidfMedicalFilerepository.GetAll().Where(x => x.PidfmedicalId == medicalModel.PidfmedicalId).ToList();
					foreach (var item in MedicalFile)
					{
						if (medicalModel.FileName.Length != 0 && i < medicalModel.FileName.Count())
						{
							var uniqueFileName = Path.GetFileNameWithoutExtension(medicalModel.FileName[i])
							   + Guid.NewGuid().ToString().Substring(0, 4)
							   + Path.GetExtension(medicalModel.FileName[i]);
							PidfMedicalFile medicalFiles = new PidfMedicalFile
							{
								FileName = uniqueFileName,
								CreatedDate = DateTime.Now,
								PidfmedicalId = Convert.ToInt64(objPIDFMedical.PidfmedicalId),
								PidfmedicalFileId = Convert.ToInt64(item.PidfmedicalFileId),
								CreatedBy = (int)objPIDFMedical.CreatedBy,
							};
							var fullPath = path + "\\" + item.FileName;
							var itmFileName = "Medical\\" + item.FileName;
							if (!medicalModel.FileName.Contains(itmFileName))
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
								if (files.Count() != 0)
								{
									string us = FileValidation(files[i]);
									if (us == null)
									{
										await FileUpload(files[i], path, uniqueFileName);
										_pidfMedicalFilerepository.UpdateAsync(medicalFiles);
									}
									else
									{
										return DBOperation.InvalidFile;

									}
								}

							}
							i++;
						}
						else if (medicalModel.FileName.Length != 0)
						{
							PidfMedicalFile medicalFiles = new PidfMedicalFile
							{
								PidfmedicalFileId = Convert.ToInt64(item.PidfmedicalFileId),
							};
							//var file = item.FileName.Substring(7);
							var fullPath = path + "\\" + item.FileName;

							if (System.IO.File.Exists(fullPath))
							{
								System.IO.File.Delete(fullPath);
							}
							_pidfMedicalFilerepository.Remove(medicalFiles);
							i++;
						}
					}

					if (medicalModel.FileName.Length != 0 && i < medicalModel.FileName.Count())
					{
						foreach (var filename in i != 0 ? medicalModel.FileName.Skip(i) : medicalModel.FileName)
						{
							var uniqueFileName = Path.GetFileNameWithoutExtension(medicalModel.FileName[i])
							   + Guid.NewGuid().ToString().Substring(0, 4)
							   + Path.GetExtension(medicalModel.FileName[i]);
							PidfMedicalFile medicalFiles = new PidfMedicalFile
							{
								FileName = uniqueFileName,
								CreatedDate = DateTime.Now,
								PidfmedicalId = Convert.ToInt64(objPIDFMedical.PidfmedicalId),
								CreatedBy = (int)objPIDFMedical.CreatedBy,
							};
							if (files.Count() != 0)
							{
								string us = FileValidation(files[i]);
								if (us == null)
								{
									await FileUpload(files[i], path, uniqueFileName);
									_pidfMedicalFilerepository.AddAsync(medicalFiles);
								}
								else
								{
									return DBOperation.InvalidFile;
								}
							}
							i++;
						}
					}
					//status update in PIDF
					await _auditLogService.UpdatePIDFStatusCommon(medicalModel.Pidfid, (int)Master_PIDFStatus.MedicalSubmitted, medicalModel.CreatedBy);
					//test to update notification
                    await _notificationService.UpdateNotification(13, "testTitleUpdate", "testDescriptionUpdate", medicalModel.CreatedBy);

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
				var medical = _mapperFactory.Get<PIDFMedicalViewModel, PidfMedical>(medicalModel);
				medical.MedicalOpinion = medicalModel.MedicalOpinion;
				medical.Remark = medicalModel.Remark;
				medical.CreatedDate = DateTime.Now;
				if (files.Count() != 0)
				{
					string us = FileValidation(files[i]);
					if (us == null)
					{
						_pidfMedicalrepository.AddAsync(medical);
						await _unitOfWork.SaveChangesAsync();
					}
					else
					{
						return DBOperation.InvalidFile;
					}
				}
				else if (files.Count() == 0 && medicalModel.FileName != null)
				{
					_pidfMedicalrepository.AddAsync(medical);
					await _unitOfWork.SaveChangesAsync();
				}
				else
				{
					return DBOperation.Error;
				}

				//var medicalFile = _mapperFactory.Get<PIDFMedicalViewModel, PidfMedicalFile>(medicalModel);
				if (medicalModel.FileName != null)
				{
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
							string us = FileValidation(files[i]);
							if (us == null)
							{
								await FileUpload(files[i], path, uniqueFileName);
								_pidfMedicalFilerepository.AddAsync(medicalFiles);
							}
							else
							{
								return DBOperation.InvalidFile;
							}
						}
						i++;
					}
				}
				//Status Update in PIDF
                await _auditLogService.UpdatePIDFStatusCommon(medicalModel.Pidfid, (int)Master_PIDFStatus.MedicalSubmitted, medicalModel.CreatedBy);

                //test To create notification
                await _notificationService.CreateNotification((int)medicalModel.Pidfid, (int)Master_PIDFStatus.MedicalSubmitted, "testTitleCreate", "testDescriptionCreate", medicalModel.CreatedBy);

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
				//     var uniqueFileName = Path.GetFileNameWithoutExtension(file.FileName)
				//+ Guid.NewGuid().ToString().Substring(0, 4)
				//+ Path.GetExtension(file.FileName);
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
				var medicalFileData = _pidfMedicalFilerepository.GetAll().Where(x => x.PidfmedicalId == data.PidfmedicalId).ToList();
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

