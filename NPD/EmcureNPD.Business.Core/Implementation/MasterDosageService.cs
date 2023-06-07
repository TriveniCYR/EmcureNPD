using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.ServiceImplementations
{
    public class MasterDosageService : IMasterDosageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterDosage> _repository { get; set; }

        public MasterDosageService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterDosage>();
        }

        public async Task<List<MasterDosageEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterDosage, MasterDosageEntity>(await _repository.GetAllAsync());
        }

        public async Task<List<MasterDosageEntity>> GetAllActiveDosageFinance()
        {
            return _mapperFactory.GetList<MasterDosage, MasterDosageEntity>(_repository.GetAllQuery().Where(x => x.IsActive == true).ToList());
        }

        public async Task<MasterDosageEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterDosage, MasterDosageEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateDosage(MasterDosageEntity entityDosage)
        {
            MasterDosage objDosage;
            if (entityDosage.DosageId > 0)
            {
                var objModelData = _repository.Exists(x => x.DosageName.ToLower() == entityDosage.DosageName.ToLower() && x.DosageId!= entityDosage.DosageId);
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objDosage = _repository.Get(entityDosage.DosageId);
                if (objDosage != null)
                {
                    objDosage = _mapperFactory.Get<MasterDosageEntity, MasterDosage>(entityDosage);
                    _repository.UpdateAsync(objDosage);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.DosageName.ToLower() == entityDosage.DosageName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objDosage = _mapperFactory.Get<MasterDosageEntity, MasterDosage>(entityDosage);
                _repository.AddAsync(objDosage);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objDosage.DosageId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteDosage(int id)
        {
            var entityDosage = _repository.Get(x => x.DosageId == id);

            if (entityDosage == null)
                return DBOperation.NotFound;

            _repository.Remove(entityDosage);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}