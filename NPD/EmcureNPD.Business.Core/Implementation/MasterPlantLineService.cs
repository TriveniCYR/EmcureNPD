using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;
namespace EmcureNPD.Business.Core.Implementation
{
	public class MasterPlantLineService : IMasterPlantLine
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapperFactory _mapperFactory;
		private IRepository<MasterPlantLine> _repository { get; set; }
		private IRepository<MasterPlant> _repositoryMaterPlant { get; set; }
		private readonly IHelper _helper;
		public MasterPlantLineService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IHelper helper)
		{
			_unitOfWork = unitOfWork;
			_mapperFactory = mapperFactory;
			_repository = _unitOfWork.GetRepository<MasterPlantLine>();
			_repositoryMaterPlant = _unitOfWork.GetRepository<MasterPlant>();
			_helper = helper;
		}
		public async Task<DBOperation> AddUpdatePlantLine(MasterPlantLineEntity entityPlantLine)
		{
			MasterPlantLine objMasterPlantLine;

			if (entityPlantLine.LineId > 0)
			{
				var objModelData = _repository.Exists(x => x.LineName.ToLower() == entityPlantLine.LineName.ToLower() && x.LineId != entityPlantLine.LineId);
				if (objModelData)
				{ return DBOperation.AlreadyExist; }

				entityPlantLine.ModifyBy = _helper.GetLoggedInUser().UserId;
				entityPlantLine.ModifyDate = DateTime.Now;
				objMasterPlantLine = _mapperFactory.Get<MasterPlantLineEntity, MasterPlantLine>(entityPlantLine);
				_repository.UpdateAsync(objMasterPlantLine);

				await _unitOfWork.SaveChangesAsync();

			}
			else
			{

				entityPlantLine.CreatedBy = _helper.GetLoggedInUser().UserId;
				entityPlantLine.CreatedDate = DateTime.Now;
				var objModelData = _repository.Exists(x => x.LineName.ToLower() == entityPlantLine.LineName.ToLower());
				if (objModelData)
				{ return DBOperation.AlreadyExist; }
				objMasterPlantLine = _mapperFactory.Get<MasterPlantLineEntity, MasterPlantLine>(entityPlantLine);
				_repository.AddAsync(objMasterPlantLine);

				await _unitOfWork.SaveChangesAsync();
			}
			return DBOperation.Success;
		}

		public async Task<DBOperation> DeletePlantLine(int id)
		{
			var entityExcipientRequirement = _repository.Get(x => x.LineId == id);
			_repository.Remove(entityExcipientRequirement);
			await _unitOfWork.SaveChangesAsync();

			return DBOperation.Success;
		}

		public async Task<List<MasterPlantLineEntity>> GetAll()
		{
			var list = await _repository.GetAllAsync();
			return _mapperFactory.GetList<MasterPlantLine, MasterPlantLineEntity>(list.ToList());
		}

		public async Task<List<MasterPlantEntity>> GetAllActivePlants()
		{
			 var list= _mapperFactory.GetList<MasterPlant, MasterPlantEntity>(await _repositoryMaterPlant.GetAllAsync()).Where(x=>x.IsActive==true).ToList();
			return list;
		}

		public async Task<MasterPlantLineEntity> GetById(long id)
		{
			return _mapperFactory.Get<MasterPlantLine, MasterPlantLineEntity>(await _repository.GetAsync(id));
		}
	}
}
