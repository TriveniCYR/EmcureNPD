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
    public class MasterTransformFormService : IMasterTransformFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterTransform> _repository { get; set; }

        public MasterTransformFormService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterTransform>();
        }

        public async Task<List<TransformFormEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterTransform, TransformFormEntity>(await _repository.GetAllAsync());
        }

        public async Task<TransformFormEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterTransform, TransformFormEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateTransformForm(TransformFormEntity entityTransformForm)
        {
            MasterTransform objTransformForm;
            if (entityTransformForm.TransformId > 0)
            {
                objTransformForm = _repository.Get(entityTransformForm.TransformId);
                if (objTransformForm != null)
                {
                    objTransformForm = _mapperFactory.Get<TransformFormEntity, MasterTransform>(entityTransformForm);
                    _repository.UpdateAsync(objTransformForm);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objTransformForm = _mapperFactory.Get<TransformFormEntity, MasterTransform>(entityTransformForm);
                _repository.AddAsync(objTransformForm);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objTransformForm.TransformId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteTransformForm(int id)
        {
            var entityTransformForm = _repository.Get(x => x.TransformId == id);

            if (entityTransformForm == null)
                return DBOperation.NotFound;

            _repository.Remove(entityTransformForm);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}