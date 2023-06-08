using EmcureNPD.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
	public interface IMasterPlantLine
	{
		Task<List<MasterPlantLineEntity>> GetAll();

		Task<MasterPlantLineEntity> GetById(long id);

		Task<DBOperation> AddUpdatePlantLine(MasterPlantLineEntity entityPlantLine);

		Task<DBOperation> DeletePlantLine(int id);
		Task<List<MasterPlantEntity>> GetAllActivePlants();
	}
}
