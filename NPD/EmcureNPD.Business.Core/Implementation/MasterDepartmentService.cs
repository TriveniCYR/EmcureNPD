using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.ServiceImplementations
{
    public class MasterDepartmentService : IMasterDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterDepartment> _repository { get; set; }
        private IRepository<MasterBusinessUnit> _businessUnitRepository { get; set; }
        private IRepository<MasterDepartmentBusinessUnitMapping> _repositoryDepartmentBusinessUnitMapping { get; set; }

        public MasterDepartmentService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterDepartment>();
            _businessUnitRepository = _unitOfWork.GetRepository<MasterBusinessUnit>();
            _repositoryDepartmentBusinessUnitMapping = _unitOfWork.GetRepository<MasterDepartmentBusinessUnitMapping>();
        }

        public async Task<List<MasterDepartmentEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterDepartment, MasterDepartmentEntity>(await _repository.GetAllAsync());
        }

        public async Task<List<MasterDepartmentEntity>> GetAllActiveDepartment()
        {
            var objDepartmentList = new List<MasterDepartmentEntity>();
            var departmentList = _repository.GetAllQuery().Where(x => x.IsActive == true);
            foreach (var department in departmentList)
            {
                var department1 = _mapperFactory.Get<MasterDepartment, MasterDepartmentEntity>(department);
                objDepartmentList.Add(department1);
            }
            return objDepartmentList;
        }

        public async Task<MasterDepartmentEntity> GetById(int id)
        {
            var department = _mapperFactory.Get<MasterDepartment, MasterDepartmentEntity>(await _repository.GetAsync(id));
            department.BusinessUnitIds = string.Join(",", _repositoryDepartmentBusinessUnitMapping.GetAllQuery().Where(x => x.DepartmentId == department.DepartmentId).Select(x => x.BusinessUnitId.ToString()));
            department.BusinessUnitMappingIds = string.Join(",", _repositoryDepartmentBusinessUnitMapping.GetAllQuery().Where(x => x.DepartmentId == department.DepartmentId).Select(x => x.DepartmentBusinessUnitMappingId.ToString()));
            return department;
        }

        public async Task<DBOperation> AddUpdateDepartment(MasterDepartmentEntity entityDepartment)
        {
            MasterDepartment objDepartment;
            var oMasterDepartmentBusinessUnitMappingEntity = new MasterDepartmentBusinessUnitMappingEntity(); ;
            var objMasterDepartmentBusinessUnitMapping = new MasterDepartmentBusinessUnitMapping();

            string[] businessUnitIdList = entityDepartment.BusinessUnitIds.Split(',');
            int[] businessUnitIds = businessUnitIdList.Select(int.Parse).ToArray();

            if (entityDepartment.DepartmentId > 0)
            {
                objDepartment = _repository.Get(entityDepartment.DepartmentId);
                if (objDepartment != null)
                {
                    objDepartment = _mapperFactory.Get<MasterDepartmentEntity, MasterDepartment>(entityDepartment);
                    _repository.UpdateAsync(objDepartment);

                    await _unitOfWork.SaveChangesAsync();

                    oMasterDepartmentBusinessUnitMappingEntity.DepartmentId = entityDepartment.DepartmentId;

                    if (entityDepartment.BusinessUnitMappingIds != null || entityDepartment.BusinessUnitMappingIds != "")
                    {
                        var entityDepartmentBusinessUnit = _repositoryDepartmentBusinessUnitMapping.GetAllQuery().Where(x => entityDepartment.BusinessUnitMappingIds.Split(',', StringSplitOptions.None).Contains(x.DepartmentBusinessUnitMappingId.ToString()));
                        foreach (var item in entityDepartmentBusinessUnit)
                        {
                            _repositoryDepartmentBusinessUnitMapping.Remove(item);
                        }
                        await _unitOfWork.SaveChangesAsync();

                        foreach (var businessUnit in businessUnitIds)
                        {
                            oMasterDepartmentBusinessUnitMappingEntity.BusinessUnitId = businessUnit;
                            objMasterDepartmentBusinessUnitMapping = _mapperFactory.Get<MasterDepartmentBusinessUnitMappingEntity, MasterDepartmentBusinessUnitMapping>(oMasterDepartmentBusinessUnitMappingEntity);
                            _repositoryDepartmentBusinessUnitMapping.AddAsync(objMasterDepartmentBusinessUnitMapping);
                        }
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                var objModelData = _repository.Exists(x => x.DepartmentName.ToLower() == entityDepartment.DepartmentName.ToLower());
                if (objModelData)
                { return DBOperation.AlreadyExist; }
                objDepartment = _mapperFactory.Get<MasterDepartmentEntity, MasterDepartment>(entityDepartment);
                _repository.AddAsync(objDepartment);

                await _unitOfWork.SaveChangesAsync();
                if (objDepartment.DepartmentId != 0)
                {
                    oMasterDepartmentBusinessUnitMappingEntity.DepartmentId = objDepartment.DepartmentId;
                    foreach (var businessUnit in businessUnitIds)
                    {
                        oMasterDepartmentBusinessUnitMappingEntity.BusinessUnitId = businessUnit;
                        objMasterDepartmentBusinessUnitMapping = _mapperFactory.Get<MasterDepartmentBusinessUnitMappingEntity, MasterDepartmentBusinessUnitMapping>(oMasterDepartmentBusinessUnitMappingEntity);
                        _repositoryDepartmentBusinessUnitMapping.AddAsync(objMasterDepartmentBusinessUnitMapping);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
                else { return DBOperation.NotFound; }
            }

            if (objDepartment.DepartmentId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteDepartment(int id)
        {
            var entityDepartment = _repository.Get(x => x.DepartmentId == id);
            var entityDepartmentBusinessUnit = _repositoryDepartmentBusinessUnitMapping.GetAllQuery().Where(x => x.DepartmentId == id);

            if (entityDepartment == null)
                return DBOperation.NotFound;

            if (entityDepartmentBusinessUnit == null)
                return DBOperation.NotFound;

            foreach (var item in entityDepartmentBusinessUnit)
            {
                _repositoryDepartmentBusinessUnitMapping.Remove(item);
            }
            _repository.Remove(entityDepartment);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }

        public async Task<MasterBusinessUnitEntity> GetBusinessUnitByDepartmentId(int id)
        {
            var departmentBusinessUnitMapping = _repositoryDepartmentBusinessUnitMapping.Get(x => x.DepartmentId == id);
            var businessUnit = _businessUnitRepository.Get(x => x.BusinessUnitId == departmentBusinessUnitMapping.BusinessUnitId);
            return _mapperFactory.Get<MasterBusinessUnit, MasterBusinessUnitEntity>(businessUnit);
        }
    }
}