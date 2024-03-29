﻿using EmcureNPD.Business.Core.Interface;
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
    public class MasterRoleService : IMasterRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterRole> _repository { get; set; }
        private readonly IRoleModulePermission _roleModulePermission;
        private IRepository<MasterUser> _Userrepository { get; set; }

        public MasterRoleService(IUnitOfWork unitOfWork, IMasterAuditLogService auditLogService,
                                IMapperFactory mapperFactory, IRoleModulePermission roleModulePermission)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _roleModulePermission = roleModulePermission;
            _repository = _unitOfWork.GetRepository<MasterRole>();
            _Userrepository = _unitOfWork.GetRepository<MasterUser>();
        }

        public async Task<DBOperation> AddUpdateRole(MasterRoleEntity masterRoleEntity)
        {
            MasterRole objRole;
            var LoggedUserId = masterRoleEntity.LoggedUserId;
            if (masterRoleEntity.RoleId > 0) //Update existing user
            {
                if (!masterRoleEntity.IsActive)
                {
                    var IsUserExist = _Userrepository.GetAllQuery().Where(x => x.RoleId == masterRoleEntity.RoleId).ToList();
                    if (IsUserExist.Count > 0)
                        masterRoleEntity.IsActive = true;
                }
                objRole = _repository.Get(masterRoleEntity.RoleId);
                var OldObjRole = objRole;
                if (objRole != null)
                {
                    objRole = _mapperFactory.Get<MasterRoleEntity, MasterRole>(masterRoleEntity);
                    objRole.ModifyBy = LoggedUserId;
                    objRole.ModifyDate = DateTime.Now;
                    _repository.UpdateAsync(objRole);
                    await _unitOfWork.SaveChangesAsync();

                    //var isSuccess = await _auditLogService.CreateAuditLog<MasterRole>(Utility.Audit.AuditActionType.Update,
                    //    Utility.Enums.ModuleEnum.RoleManagement, OldObjRole, objRole,0);
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else //add new user
            {
                objRole = _mapperFactory.Get<MasterRoleEntity, MasterRole>(masterRoleEntity);
                objRole.CreatedBy = LoggedUserId;
                objRole.CreatedDate = DateTime.Now;
                _repository.AddAsync(objRole);
                await _unitOfWork.SaveChangesAsync();
            }

            if (objRole.RoleId == 0)
                return DBOperation.Error;

            #region Add Module Permsson

            if (masterRoleEntity.MasterModules.Count > 0)
            {
                var ModulePermission = masterRoleEntity.MasterModules.Select(xx => xx.RoleModulePermission);
                var SubModulePermission = masterRoleEntity.MasterModules.SelectMany(xx => xx.MasterSubModules?.Select(yy => yy.RoleModulePermission));
                var Permissions = ModulePermission.Concat(SubModulePermission);
                Permissions = Permissions.Select(xx => { xx.RoleId = objRole.RoleId; return xx; });

                await _roleModulePermission.AddUpdateRoleModulePermission(Permissions.ToList());
            }

            #endregion Add Module Permsson

            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteRole(int id)
        {
            var entityRole = _repository.Get(x => x.RoleId == id);
            var IsUserExist = _Userrepository.GetAllQuery().Where(x => x.RoleId == entityRole.RoleId).ToList();
            if (IsUserExist.Count <= 0)
            {
                if (entityRole == null)
                    return DBOperation.NotFound;

                _repository.Remove(entityRole);

                await _unitOfWork.SaveChangesAsync();
                return DBOperation.Success;
            }
            return DBOperation.NotFound;
        }

        public async Task<List<MasterRoleEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterRole, MasterRoleEntity>(await _repository.GetAllAsync());
        }

        public async Task<List<MasterRoleEntity>> GetActiveRole()
        {
            var ActiveRole = await _repository.GetAllAsync(x => x.IsActive == true && x.IsDeleted == false);
            return _mapperFactory.GetList<MasterRole, MasterRoleEntity>(ActiveRole.ToList());
        }

        public async Task<MasterRoleEntity> GetById(int id)
        {
            MasterRoleEntity _roleEntity = _mapperFactory.Get<MasterRole, MasterRoleEntity>(await _repository.GetAsync(id));
            
                var IsUserExist = _Userrepository.GetAllQuery().Where(x => x.RoleId == _roleEntity.RoleId).ToList();
                if (IsUserExist !=null && IsUserExist.Count > 0)
                _roleEntity.IsUserAssigned = true;
           
            return _roleEntity;
        }
    }
}