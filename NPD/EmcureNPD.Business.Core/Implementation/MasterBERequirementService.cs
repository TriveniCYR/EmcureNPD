using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.ServiceImplementations
{
    public class MasterBERequirementService : IMasterBERequirementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IExceptionService _ExceptionService;
        private IRepository<MasterBerequirement> _repository { get; set; }

        public MasterBERequirementService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IExceptionService exceptionService)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterBerequirement>();
            _ExceptionService = exceptionService;
        }

        public async Task<List<MasterBERequirementEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterBerequirement, MasterBERequirementEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterBERequirementEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterBerequirement, MasterBERequirementEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateBERequirement(MasterBERequirementEntity entityBERequirement)
        {
            try
            {

            MasterBerequirement objBERequirement;
            if (entityBERequirement.BERequirementId > 0)
            {
                objBERequirement = _repository.Get(entityBERequirement.BERequirementId);
                if (objBERequirement != null)
                {
                    objBERequirement = _mapperFactory.Get<MasterBERequirementEntity, MasterBerequirement>(entityBERequirement);
                    _repository.UpdateAsync(objBERequirement);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objBERequirement = _mapperFactory.Get<MasterBERequirementEntity, MasterBerequirement>(entityBERequirement);
                _repository.AddAsync(objBERequirement);
            }
           
                await _unitOfWork.SaveChangesAsync();
            if (objBERequirement.BerequirementId == 0)
                return DBOperation.Error;

            return DBOperation.Success;

            }
            catch (System.Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return DBOperation.Error;
            }
        }

        public async Task<DBOperation> DeleteBERequirement(int id)
        {
            var entityBERequirement = _repository.Get(x => x.BerequirementId == id);

            if (entityBERequirement == null)
                return DBOperation.NotFound;

            _repository.Remove(entityBERequirement);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}