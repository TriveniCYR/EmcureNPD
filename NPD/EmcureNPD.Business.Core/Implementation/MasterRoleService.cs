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


        public MasterRoleService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IRoleModulePermission roleModulePermission)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _roleModulePermission = roleModulePermission;
            _repository = _unitOfWork.GetRepository<MasterRole>();
           
        }

        public async Task<DBOperation> AddUpdateRole(MasterRoleEntity masterRoleEntity)
        {
            MasterRole objRole;
            var LoggedUserId = masterRoleEntity.LoggedUserId;
            List<RoleModulePermissionEntity> objRolePermissions;
            if (masterRoleEntity.RoleId > 0) //Update existing user
            {
                objRole = _repository.Get(masterRoleEntity.RoleId);
                if (objRole != null)
                {
                    objRole = _mapperFactory.Get<MasterRoleEntity, MasterRole>(masterRoleEntity);
                    objRole.ModifyBy = LoggedUserId;
                    objRole.ModifyDate = DateTime.Now;
                    _repository.UpdateAsync(objRole);
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
            }

            await _unitOfWork.SaveChangesAsync();

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

            #endregion



            return DBOperation.Success;
        }

        public async Task<DBOperation> DeleteRole(int id)
        {
            var entityRole = _repository.Get(x => x.RoleId == id);

            if (entityRole == null)
                return DBOperation.NotFound;

            _repository.Remove(entityRole);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }

        public async Task<List<MasterRoleEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterRole, MasterRoleEntity>(await _repository.GetAllAsync());
        }

        public async Task<MasterRoleEntity> GetById(int id)
        {
            return _mapperFactory.Get<MasterRole, MasterRoleEntity>(await _repository.GetAsync(id));
        }



    }
}
