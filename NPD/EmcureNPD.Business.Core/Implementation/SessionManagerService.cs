using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
	public class SessionManagerService : ISessionManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapperFactory _mapperFactory;
		private IRepository<TblSessionManager> _repository { get; set; }
		public SessionManagerService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
		{
			_unitOfWork = unitOfWork;
			_mapperFactory = mapperFactory;
			_repository = _unitOfWork.GetRepository<TblSessionManager>();
		}
		public async Task<GeneralEnum.DBOperation> AddUpdateSession(SessionManagerEntity entitySessionManager)
		{
			TblSessionManager objSessionManager;
			
				objSessionManager = _repository.GetAllQuery().Where(x => x.Email == entitySessionManager.Email).FirstOrDefault();
				if (objSessionManager != null)
				{
					//objSessionManager = _mapperFactory.Get<SessionManagerEntity, TblSessionManager>(entitySessionManager);
				    objSessionManager.UserId=entitySessionManager.UserId;
				    objSessionManager.TokenIssuedAt = entitySessionManager.TokenIssuedAt;
				    objSessionManager.UserToken= entitySessionManager.UserToken;
				    objSessionManager.VallidTo=entitySessionManager.VallidTo;
					_repository.UpdateAsync(objSessionManager);
				await _unitOfWork.SaveChangesAsync();
				return DBOperation.Success;
			    }
			    else
			    {
			    
			    	objSessionManager = _mapperFactory.Get<SessionManagerEntity, TblSessionManager>(entitySessionManager);
			    	_repository.AddAsync(objSessionManager);
				await _unitOfWork.SaveChangesAsync();
				return DBOperation.Success;
			    }

			

			

			
		}

		
		public async Task<SessionManagerEntity> ValidateActiveToken(int UserId)
		{
			var objData = await _repository.GetAsync(x => x.UserId == UserId);
			return _mapperFactory.Get<TblSessionManager, SessionManagerEntity>(objData);
		}
	}
}
