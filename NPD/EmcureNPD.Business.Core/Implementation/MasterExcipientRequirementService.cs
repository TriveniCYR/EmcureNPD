using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class MasterExcipientRequirementService : IMasterExcipientRequirement
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterExcipientRequirement> _repository { get; set; }
        private readonly IHelper _helper;
        public MasterExcipientRequirementService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterExcipientRequirement>();
            _helper = helper;
        }
        public async Task<DBOperation> AddUpdateExcipientRequirement(MasterExcipientRequirementEntity entityExcipientRequirement)
        {
            MasterExcipientRequirement objExcipientRequirement;
            
            if (entityExcipientRequirement.ExcipientRequirementId > 0)
            {
                var objModelData = _repository.Exists(x => x.ExcipientRequirementName.ToLower() == entityExcipientRequirement.ExcipientRequirementName.ToLower() && x.ExcipientRequirementId != entityExcipientRequirement.ExcipientRequirementId);
                if (objModelData)
                { return DBOperation.AlreadyExist; }
               
					entityExcipientRequirement.ModifyBy = _helper.GetLoggedInUser().UserId;
                    entityExcipientRequirement.ModifyDate = DateTime.Now;
					objExcipientRequirement = _mapperFactory.Get<MasterExcipientRequirementEntity, MasterExcipientRequirement>(entityExcipientRequirement);
                    _repository.UpdateAsync(objExcipientRequirement);

                    await _unitOfWork.SaveChangesAsync();
               
            }
            else
            {
                
                entityExcipientRequirement.CreatedBy = _helper.GetLoggedInUser().UserId;
				entityExcipientRequirement.CreatedDate = DateTime.Now;
				var objModelData = _repository.Exists(x => x.ExcipientRequirementName.ToLower() == entityExcipientRequirement.ExcipientRequirementName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objExcipientRequirement = _mapperFactory.Get<MasterExcipientRequirementEntity, MasterExcipientRequirement>(entityExcipientRequirement);
                _repository.AddAsync(objExcipientRequirement);

                await _unitOfWork.SaveChangesAsync();
            }
            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteExcipientRequirement(int id)
        {
            var entityExcipientRequirement = _repository.Get(x => x.ExcipientRequirementId == id);
            _repository.Remove(entityExcipientRequirement);
            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }

        public async Task<List<MasterExcipientRequirementEntity>> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return _mapperFactory.GetList<MasterExcipientRequirement, MasterExcipientRequirementEntity>(list.ToList());
        }

        public async Task<MasterExcipientRequirementEntity> GetById(long id)
        {
           return _mapperFactory.Get<MasterExcipientRequirement, MasterExcipientRequirementEntity>(await _repository.GetAsync(id));
        }
    }
}
