using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
	public interface ISessionManager
	{
		Task<SessionManagerEntity> ValidateActiveToken(int UserId);
		Task<DBOperation> AddUpdateSession(SessionManagerEntity entitySessionManager);
	}
}
