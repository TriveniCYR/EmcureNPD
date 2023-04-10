using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation
{
    public class MasterModuleService : IMasterModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<MasterModule> _repository { get; set; }
        private IRepository<MasterSubModule> _repositorySub { get; set; }

        private IRepository<RoleModulePermission> _repositoryRolePermission { get; set; }

        public MasterModuleService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterModule>();
            _repositorySub = _unitOfWork.GetRepository<MasterSubModule>();
            _repositoryRolePermission = _unitOfWork.GetRepository<RoleModulePermission>();
        }

        public async Task<List<MasterModuleEntity>> GetAll()
        {

            var MasterModuleData = _mapperFactory.GetList<MasterModule, MasterModuleEntity>(await _repository.GetAllAsync()).OrderBy(x => x.SortOrder).ToList();

            var MasterSubModuleData = _mapperFactory.GetList<MasterSubModule, MasterSubModuleEntity>(await _repositorySub.GetAllAsync());

            MasterModuleData = MasterModuleData.Select(xx => { xx.RoleModulePermission.ModuleId = xx.ModuleId; return xx; }).ToList();
            MasterSubModuleData = MasterSubModuleData.Select(xx => { xx.RoleModulePermission.ModuleId = xx.ModuleId; xx.RoleModulePermission.SubModuleId = xx.SubModuleId; return xx; }).ToList();

            MasterModuleData = MasterModuleData.GroupJoin(MasterSubModuleData,
                            c => c.ModuleId,
                            o => o.ModuleId,
                            (c, o) => new MasterModuleEntity
                            {
                                ModuleName = c.ModuleName,
                                ModuleId = c.ModuleId,
                                CreatedDate = c.CreatedDate,
                                IsActive = c.IsActive,
                                RoleModulePermission = c.RoleModulePermission,
                                MasterSubModules = o.ToList()
                            }).ToList();

            return MasterModuleData;
        }

        public async Task<List<MasterModuleEntity>> GetByRoleId(int roleId)
        {
            var Permissions = _mapperFactory.GetList<RoleModulePermission, RoleModulePermissionEntity>(await _repositoryRolePermission.FindAllAsync(xx => xx.RoleId == roleId)).OrderBy(x => x.SortOrder).ToList();
            if (Permissions.Any())
            {
                var MasterModuleData = _mapperFactory.GetList<MasterModule, MasterModuleEntity>(await _repository.GetAllAsync());

                var MasterSubModuleData = _mapperFactory.GetList<MasterSubModule, MasterSubModuleEntity>(await _repositorySub.GetAllAsync());
                MasterSubModuleData = MasterSubModuleData.GroupJoin(Permissions,
                                        c => c.ModuleId, o => o.ModuleId,
                                        (c, o) => new MasterSubModuleEntity
                                        {
                                            CreatedDate = c.CreatedDate,
                                            IsActive = c.IsActive,
                                            ModuleId = c.ModuleId,
                                            SubModuleId = c.SubModuleId,
                                            SubModuleName = c.SubModuleName,
                                            RoleModulePermission = o.FirstOrDefault(xx => xx.SubModuleId == c.SubModuleId)
                                        }).ToList();
                foreach (var item in MasterSubModuleData)
                {
                    if (item.RoleModulePermission == null)
                    {
                        var _roleModulepermission = new RoleModulePermissionEntity();
                        _roleModulepermission.Add = false;
                        _roleModulepermission.Delete = false;
                        _roleModulepermission.View = false;
                        _roleModulepermission.Edit = false;
                        _roleModulepermission.Approve = false;
                        _roleModulepermission.RoleId = roleId;
                        _roleModulepermission.ModuleId = item.ModuleId;
                        _roleModulepermission.SubModuleId = item.SubModuleId;

                        item.RoleModulePermission = _roleModulepermission;
                    }
                }
                MasterModuleData = MasterModuleData.GroupJoin(MasterSubModuleData,
                               c => c.ModuleId,
                               o => o.ModuleId,
                               (c, o) => new MasterModuleEntity
                               {
                                   ModuleName = c.ModuleName,
                                   ModuleId = c.ModuleId,
                                   CreatedDate = c.CreatedDate,
                                   IsActive = c.IsActive,
                                   SortOrder= c.SortOrder,
                                   RoleModulePermission = Permissions.FirstOrDefault(xx => xx.ModuleId == c.ModuleId && xx.SubModuleId == 0),
                                   MasterSubModules = o.ToList()
                               }).ToList();

                foreach(var item in MasterModuleData)
                {
                   if(item.RoleModulePermission ==null)
                    {
                        var _roleModulepermission = new RoleModulePermissionEntity();
                        _roleModulepermission.Add = false;
                        _roleModulepermission.Delete = false;
                        _roleModulepermission.View = false;
                        _roleModulepermission.Edit = false;
                        _roleModulepermission.Approve = false;
                        _roleModulepermission.RoleId = roleId;
                        _roleModulepermission.ModuleId = item.ModuleId; 
                        item.RoleModulePermission = _roleModulepermission;
                    }
                }
                return MasterModuleData;
            }
            else
            {
                return await GetAll();
            }
        }
        public async Task<IEnumerable<dynamic>> GetByPermisionRoleUsingRoleId(int roleId)
        {
            var Permissions = _mapperFactory.GetList<RoleModulePermission, RoleModulePermissionEntity>(await _repositoryRolePermission.FindAllAsync(xx => xx.RoleId == roleId && (xx.Approve==true || xx.Add == true || xx.Delete == true || xx.Edit == true || xx.View == true)));
            if (Permissions.Any())
            {
                var MasterModuleData = _mapperFactory.GetList<MasterModule, MasterModuleEntity>(await _repository.GetAllAsync());

                var MasterSubModuleData = _mapperFactory.GetList<MasterSubModule, MasterSubModuleEntity>(await _repositorySub.GetAllAsync());

                var person = (from p in Permissions
                              join m in MasterModuleData on p.ModuleId equals m.ModuleId
                              join s in MasterSubModuleData on p.SubModuleId equals s.SubModuleId into SubMS
                              from SubM in SubMS.DefaultIfEmpty()
                              select new
                              {
                                  RoleId = p.RoleId,
                                  ModuleId = p.ModuleId,
                                  MainModuleName = m.ModuleName,
                                  SubModuleName = SubM?.SubModuleName ?? string.Empty,
                                  SubModuleId= p.SubModuleId,
                                  MainModuleId=p.ModuleId,
                                  ControlName= SubM?.ControlName ?? m.ControlName,
                                  Add = p.Add,
                                  View = p.View,
                                  Edit = p.Edit,
                                  Delete = p.Delete,
                                  Approve = p.Approve

                              }).ToList();


                return person;
            }
            else
            {
                return null;
            }
        }        
    }
}
