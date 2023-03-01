using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public class MasterRoleEntity
    {
        public MasterRoleEntity()
        {
            MasterModules = new List<MasterModuleEntity>();
        }

        public int RoleId { get; set; }
        public int LoggedUserId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RoleName", ResourceType = typeof(Master))]
        public string RoleName { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }


        public List<MasterRoleEntity> Roles { get; set; }


        public virtual List<MasterModuleEntity> MasterModules { get; set; }

    }

}
