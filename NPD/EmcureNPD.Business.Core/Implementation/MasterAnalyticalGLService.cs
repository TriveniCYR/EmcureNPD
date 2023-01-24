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
    public class MasterAnalyticalGLService : IMasterAnalyticalGLService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterAnalytical> _repository { get; set; }

        public MasterAnalyticalGLService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterAnalytical>();
        }

        public async Task<List<MasterAnalyticalGLEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterAnalytical, MasterAnalyticalGLEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterAnalyticalGLEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterAnalytical, MasterAnalyticalGLEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateAnalyticalGL(MasterAnalyticalGLEntity entityAnalyticalGL)
        {
            MasterAnalytical objAnalyticalGL;
            if (entityAnalyticalGL.AnalyticalId > 0)
            {
                objAnalyticalGL = _repository.Get(entityAnalyticalGL.AnalyticalId);
                if (objAnalyticalGL != null)
                {
                    objAnalyticalGL = _mapperFactory.Get<MasterAnalyticalGLEntity, MasterAnalytical>(entityAnalyticalGL);
                    _repository.UpdateAsync(objAnalyticalGL);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objAnalyticalGL = _mapperFactory.Get<MasterAnalyticalGLEntity, MasterAnalytical>(entityAnalyticalGL);
                _repository.AddAsync(objAnalyticalGL);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objAnalyticalGL.AnalyticalId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteAnalyticalGL(int id)
        {
            var entityAnalyticalGL = _repository.Get(x => x.AnalyticalId == id);

            if (entityAnalyticalGL == null)
                return DBOperation.NotFound;

            _repository.Remove(entityAnalyticalGL);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}