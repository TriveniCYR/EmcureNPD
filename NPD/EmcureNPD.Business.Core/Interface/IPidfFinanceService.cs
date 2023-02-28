using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
	public interface IPidfFinanceService
	{

		Task<List<PidfFinance>> GetAll();

		Task<PidfFinance> GetById(int id);

		Task<DBOperation> AddUpdatePidfFinance(FinanceModel EntitypidfFinance);
		Task<dynamic> GetPidfFinance(int Pidfid = 0);
		Task<dynamic> GetFinanceBatchSizeCoating(int PidffinaceId = 0);
	}
}
	
