using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
using static EmcureNPD.Utility.Enums.GeneralEnum;
using EmcureNPD.Data.DataAccess.Core.Repositories;

namespace EmcureNPD.Business.Core.Implementation
{
	public class PidfPbfRnDService: IPidfPbfRnD
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapperFactory _mapperFactory;
		private readonly IHelper _helper;
		private IRepository<PidfPbfRnD> _pidfPbfRnDRepository { get; set; }
		private readonly IMasterAuditLogService _auditLogService;
		public PidfPbfRnDService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IHelper helper, IMasterAuditLogService auditLogService)
		{
			_unitOfWork = unitOfWork;
			_mapperFactory = mapperFactory;
			_helper = helper;
			_pidfPbfRnDRepository = _unitOfWork.GetRepository<PidfPbfRnD>();
			_auditLogService = auditLogService;
		}

		public async Task<DBOperation> AddUpdate(PidfPbfRnDEntity EntitypidfRnd)
		{
			PidfPbfRnD objpidfPbfRnD;
			var loggedInUserId = _helper.GetLoggedInUser().UserId;
			objpidfPbfRnD = _pidfPbfRnDRepository.GetAll().Where(x => x.PidfpbfrnDid == EntitypidfRnd.PidfpbfrnDid).First();
			if (objpidfPbfRnD != null)
			{
				//objpidfPbfRnD.ExicipientProtoype.CreatedDate = DateTime.UtcNow;
				_pidfPbfRnDRepository.UpdateAsync(objpidfPbfRnD);
				await _unitOfWork.SaveChangesAsync();
			}
			else
			{
				_pidfPbfRnDRepository.AddAsync(objpidfPbfRnD);
				await _unitOfWork.SaveChangesAsync();
			}
			var isSuccess = await _auditLogService.CreateAuditLog<PidfPbfRnDEntity>(objpidfPbfRnD.PidfpbfrnDid > 0 ? Utility.Audit.AuditActionType.Update : Utility.Audit.AuditActionType.Create,
						  Utility.Enums.ModuleEnum.PBF, EntitypidfRnd, EntitypidfRnd, Convert.ToInt32(objpidfPbfRnD.PidfpbfrnDid));
			await _unitOfWork.SaveChangesAsync();
			//var _StatusID = (EntitypidfRnd.SaveSubmitType == "Save") ? Master_PIDFStatus.PBFSubmitted : Master_PIDFStatus.PBFInProgress;
			//await _auditLogService.UpdatePIDFStatusCommon(objpidfPbfRnD.PidfpbfrnDid, (int)_StatusID, loggedInUserId);
			
			return DBOperation.Success;
		}

	public Task<List<PidfPbfRnDEntity>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<PidfPbfRnDEntity> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<dynamic> GetPidfPbfRnD(int PidfPbfRnDId = 0)
		{
			throw new NotImplementedException();
		}
	}
}
