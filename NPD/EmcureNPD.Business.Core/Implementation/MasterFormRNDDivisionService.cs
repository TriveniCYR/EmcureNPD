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
    public class MasterFormRNDDivisionService : IMasterFormRNDDivisionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterFormRnDdivision> _repository { get; set; }

        public MasterFormRNDDivisionService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterFormRnDdivision>();
        }

        public async Task<List<MasterFormRNDDivisionEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterFormRnDdivision, MasterFormRNDDivisionEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterFormRNDDivisionEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterFormRnDdivision, MasterFormRNDDivisionEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateFormRNDDivision(MasterFormRNDDivisionEntity entityFormRNDDivision)
        {
            MasterFormRnDdivision objFormRNDDivision;
            if (entityFormRNDDivision.FormRNDDivisionId > 0)
            {
                objFormRNDDivision = _repository.Get(entityFormRNDDivision.FormRNDDivisionId);
                if (objFormRNDDivision != null)
                {
                    objFormRNDDivision = _mapperFactory.Get<MasterFormRNDDivisionEntity, MasterFormRnDdivision>(entityFormRNDDivision);
                    _repository.UpdateAsync(objFormRNDDivision);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objFormRNDDivision = _mapperFactory.Get<MasterFormRNDDivisionEntity, MasterFormRnDdivision>(entityFormRNDDivision);
                _repository.AddAsync(objFormRNDDivision);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objFormRNDDivision.FormRnDdivisionId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteFormRNDDivision(int id)
        {
            var entityFormRNDDivision = _repository.Get(x => x.FormRnDdivisionId == id);

            if (entityFormRNDDivision == null)
                return DBOperation.NotFound;

            _repository.Remove(entityFormRNDDivision);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}