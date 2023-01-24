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
    public class MasterExtensionApplicationService : IMasterExtensionApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterExtensionApplication> _repository { get; set; }

        public MasterExtensionApplicationService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterExtensionApplication>();
        }

        public async Task<List<MasterExtensionApplicationEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterExtensionApplication, MasterExtensionApplicationEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterExtensionApplicationEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterExtensionApplication, MasterExtensionApplicationEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateExtensionApplication(MasterExtensionApplicationEntity entityExtensionApplication)
        {
            MasterExtensionApplication objExtensionApplication;
            if (entityExtensionApplication.ExtensionApplicationId > 0)
            {
                objExtensionApplication = _repository.Get(entityExtensionApplication.ExtensionApplicationId);
                if (objExtensionApplication != null)
                {
                    objExtensionApplication = _mapperFactory.Get<MasterExtensionApplicationEntity, MasterExtensionApplication>(entityExtensionApplication);
                    _repository.UpdateAsync(objExtensionApplication);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objExtensionApplication = _mapperFactory.Get<MasterExtensionApplicationEntity, MasterExtensionApplication>(entityExtensionApplication);
                _repository.AddAsync(objExtensionApplication);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objExtensionApplication.ExtensionApplicationId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteExtensionApplication(int id)
        {
            var entityExtensionApplication = _repository.Get(x => x.ExtensionApplicationId == id);

            if (entityExtensionApplication == null)
                return DBOperation.NotFound;

            _repository.Remove(entityExtensionApplication);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}