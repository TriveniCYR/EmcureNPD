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
	public interface IPidfPbfRnD
	{
		Task<List<PidfPbfRnDEntity>> GetAll();

		Task<PidfPbfRnDEntity> GetById(int id);

		Task<DBOperation>AddUpdate(PidfPbfRnDEntity EntitypidfRnd);
		Task<dynamic> GetPidfPbfRnD(int PidfPbfRnDId = 0);
	}
}
