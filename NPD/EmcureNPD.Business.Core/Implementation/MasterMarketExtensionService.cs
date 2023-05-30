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
    public class MasterMarketExtensionService : IMasterMarketExtensionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterMarketExtenstion> _repository { get; set; }

        public MasterMarketExtensionService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterMarketExtenstion>();
        }

        public async Task<List<MarketExtensionEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterMarketExtenstion, MarketExtensionEntity>(await _repository.GetAllAsync());
        }

        public async Task<MarketExtensionEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterMarketExtenstion, MarketExtensionEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateMarketExtension(MarketExtensionEntity entityMarketExtension)
        {
            MasterMarketExtenstion objMarketExtension;
            if (entityMarketExtension.MarketExtenstionId > 0)
            {
                objMarketExtension = _repository.Get(entityMarketExtension.MarketExtenstionId);
                if (objMarketExtension != null)
                {
                    objMarketExtension = _mapperFactory.Get<MarketExtensionEntity, MasterMarketExtenstion>(entityMarketExtension);
                    _repository.UpdateAsync(objMarketExtension);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.MarketExtenstionName.ToLower() == entityMarketExtension.MarketExtenstionName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objMarketExtension = _mapperFactory.Get<MarketExtensionEntity, MasterMarketExtenstion>(entityMarketExtension);
                _repository.AddAsync(objMarketExtension);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objMarketExtension.MarketExtenstionId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteMarketExtension(int id)
        {
            var entityMarketExtension = _repository.Get(x => x.MarketExtenstionId == id);

            if (entityMarketExtension == null)
                return DBOperation.NotFound;

            _repository.Remove(entityMarketExtension);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}