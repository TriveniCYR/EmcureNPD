using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class MasterTestLicenseService : IMasterTestLicenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterTestLicense> _repository { get; set; }

        public MasterTestLicenseService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterTestLicense>();
        }

        public async Task<List<MasterTestLicenseEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterTestLicense, MasterTestLicenseEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterTestLicenseEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterTestLicense, MasterTestLicenseEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateTestLicense(MasterTestLicenseEntity entityTestLicense)
        {
            MasterTestLicense objTestLicense;
            if (entityTestLicense.TestLicenseId > 0)
            {
                objTestLicense = _repository.Get(entityTestLicense.TestLicenseId);
                if (objTestLicense != null)
                {
                    objTestLicense = _mapperFactory.Get<MasterTestLicenseEntity, MasterTestLicense>(entityTestLicense);
                    _repository.UpdateAsync(objTestLicense);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objTestLicense = _mapperFactory.Get<MasterTestLicenseEntity, MasterTestLicense>(entityTestLicense);
                _repository.AddAsync(objTestLicense);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objTestLicense.TestLicenseId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteTestLicense(int id)
        {
            var entityTestLicense = _repository.Get(x => x.TestLicenseId == id);

            if (entityTestLicense == null)
                return DBOperation.NotFound;

            _repository.Remove(entityTestLicense);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}