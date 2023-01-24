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
    public class MasterProductStrengthService : IMasterProductStrengthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterProductStrength> _repository { get; set; }

        public MasterProductStrengthService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterProductStrength>();
        }

        public async Task<List<MasterProductStrengthEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterProductStrength, MasterProductStrengthEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterProductStrengthEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterProductStrength, MasterProductStrengthEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateProductStrength(MasterProductStrengthEntity entityProductStrength)
        {
            MasterProductStrength objProductStrength;
            if (entityProductStrength.ProductStrengthId > 0)
            {
                objProductStrength = _repository.Get(entityProductStrength.ProductStrengthId);
                if (objProductStrength != null)
                {
                    objProductStrength = _mapperFactory.Get<MasterProductStrengthEntity, MasterProductStrength>(entityProductStrength);
                    _repository.UpdateAsync(objProductStrength);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objProductStrength = _mapperFactory.Get<MasterProductStrengthEntity, MasterProductStrength>(entityProductStrength);
                _repository.AddAsync(objProductStrength);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objProductStrength.ProductStrengthId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteProductStrength(int id)
        {
            var entityProductStrength = _repository.Get(x => x.ProductStrengthId == id);

            if (entityProductStrength == null)
                return DBOperation.NotFound;

            _repository.Remove(entityProductStrength);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}