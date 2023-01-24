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
    public class MasterFillingExpenseService : IMasterFillingExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterExpenseRegion> _repository { get; set; }

        public MasterFillingExpenseService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterExpenseRegion>();
        }

        public async Task<List<MasterFillingExpenseEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterExpenseRegion, MasterFillingExpenseEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterFillingExpenseEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterExpenseRegion, MasterFillingExpenseEntity>(await _repository.GetAsync(id));
        }

        public async Task<DBOperation> AddUpdateFillingExpense(MasterFillingExpenseEntity entityFillingExpense)
        {
            MasterExpenseRegion objFillingExpense;
            if (entityFillingExpense.ExpenseRegionId > 0)
            {
                objFillingExpense = _repository.Get(entityFillingExpense.ExpenseRegionId);
                if (objFillingExpense != null)
                {
                    objFillingExpense = _mapperFactory.Get<MasterFillingExpenseEntity, MasterExpenseRegion>(entityFillingExpense);
                    _repository.UpdateAsync(objFillingExpense);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objFillingExpense = _mapperFactory.Get<MasterFillingExpenseEntity, MasterExpenseRegion>(entityFillingExpense);
                _repository.AddAsync(objFillingExpense);
            }

            await _unitOfWork.SaveChangesAsync();

            if (objFillingExpense.ExpenseRegionId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteFillingExpense(int id)
        {
            var entityFillingExpense = _repository.Get(x => x.ExpenseRegionId == id);

            if (entityFillingExpense == null)
                return DBOperation.NotFound;

            _repository.Remove(entityFillingExpense);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
    }
}