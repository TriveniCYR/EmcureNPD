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

namespace EmcureNPD.Business.Core.Implementation
{
    public class RoleModulePermissionService : IRoleModulePermission
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<RoleModulePermission> _repository { get; set; }

        public RoleModulePermissionService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<RoleModulePermission>();
        }

        public async Task<DBOperation> AddUpdateRoleModulePermission(List<RoleModulePermissionEntity> roleModulePermissionEntitys)
        {

            List<RoleModulePermission> objRoleModulePermission;
            if (roleModulePermissionEntitys.Count > 0)
            {
                var objRoleModulePermissionEntity = roleModulePermissionEntitys.FirstOrDefault();
                await DeleteRoleModulePermission(objRoleModulePermissionEntity.RoleId);
            }

            objRoleModulePermission = _mapperFactory.GetList<RoleModulePermissionEntity, RoleModulePermission>(roleModulePermissionEntitys);

            foreach (var rolePermission in objRoleModulePermission)
            {
                _repository.AddAsync(rolePermission);
                await _unitOfWork.SaveChangesAsync();
            }

            if (objRoleModulePermission.Count == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteRoleModulePermission(int id)
        {
            var entityRole = await _repository.GetAllAsync(x => x.RoleId == id);

            if (!entityRole.Any())
                return DBOperation.NotFound;

            foreach (var roleModule in entityRole)
            {
                _repository.Remove(roleModule);
                await _unitOfWork.SaveChangesAsync();
            }

            return DBOperation.Success;
        }

        public Task<List<RoleModulePermissionEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<MasterRoleEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
