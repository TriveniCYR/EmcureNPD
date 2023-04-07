using System;
using System.Collections.Generic;

namespace EmcureNPD.Business.Models
{
    public class MasterModuleEntity
    {

        public MasterModuleEntity()
        {
            RoleModulePermission = new RoleModulePermissionEntity();
            MasterSubModules = new List<MasterSubModuleEntity>();
        }

        public int ModuleId { get; set; }
        public int SortOrder { get; set; }
        public string ModuleName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
        public string ControlName { get; set; }

        public virtual RoleModulePermissionEntity RoleModulePermission { get; set; }



        public virtual List<MasterSubModuleEntity> MasterSubModules { get; set; }
    }
    public class MenuModels
    {

        public string MainMenuName { get; set; }
        public int MainMenuId { get; set; }
        public string SubMenuName { get; set; }
        public int SubMenuId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
