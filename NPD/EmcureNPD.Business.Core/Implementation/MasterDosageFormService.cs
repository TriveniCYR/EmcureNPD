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
    public class MasterDosageFormService : IMasterDosageFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterDosageForm> _repository { get; set; }

        public MasterDosageFormService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterDosageForm>();
        }

        public async Task<List<MasterDosageFormEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterDosageForm, MasterDosageFormEntity>(await _repository.GetAllAsync());
        }

        public async Task<List<MasterDosageFormEntity>> GetAllActiveDosageFormFinance()
        {
            return _mapperFactory.GetList<MasterDosageForm, MasterDosageFormEntity>(_repository.GetAllQuery().Where(x => x.IsActive == true).ToList());
        }

        public async Task<MasterDosageFormEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterDosageForm, MasterDosageFormEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateDosageForm(MasterDosageFormEntity entityDosageForm)
        {
            MasterDosageForm objDosageForm;
            if (entityDosageForm.DosageFormId > 0)
            {
                objDosageForm = _repository.Get(entityDosageForm.DosageFormId);
                if (objDosageForm != null)
                {
                    objDosageForm = _mapperFactory.Get<MasterDosageFormEntity, MasterDosageForm>(entityDosageForm);
                    _repository.UpdateAsync(objDosageForm);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objDosageForm = _mapperFactory.Get<MasterDosageFormEntity, MasterDosageForm>(entityDosageForm);
                _repository.AddAsync(objDosageForm);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objDosageForm.DosageFormId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteDosageForm(int id)
        {
            var entityDosageForm = _repository.Get(x => x.DosageFormId == id);

            if (entityDosageForm == null)
                return DBOperation.NotFound;

            _repository.Remove(entityDosageForm);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}