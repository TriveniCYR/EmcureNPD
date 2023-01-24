using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterSubModule
    {
        public int SubModuleId { get; set; }
        public int? ModuleId { get; set; }
        public string SubModuleName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ControlName { get; set; }

        public virtual MasterModule Module { get; set; }
    }
}
