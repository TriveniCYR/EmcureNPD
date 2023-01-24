using System;

namespace EmcureNPD.Business.Models
{
    public class MasterSubModuleEntity
    {
        public MasterSubModuleEntity()
        {
            RoleModulePermission = new RoleModulePermissionEntity();
        }

        public int SubModuleId { get; set; }

        public int ModuleId { get; set; }

        public string SubModuleName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ControlName { get; set; }

        public virtual RoleModulePermissionEntity RoleModulePermission { get; set; }
    }
}
