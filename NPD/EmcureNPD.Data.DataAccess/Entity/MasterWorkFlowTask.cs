using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterWorkFlowTask
    {
        public int TaskId { get; set; }
        public int? ParentId { get; set; }
        public string TaskName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public int? WorkflowId { get; set; }
        public bool? Country { get; set; }

        public virtual MasterWorkflow Workflow { get; set; }
    }
}
