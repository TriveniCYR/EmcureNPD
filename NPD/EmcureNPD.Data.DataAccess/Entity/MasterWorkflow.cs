using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterWorkflow
    {
        public MasterWorkflow()
        {
            MasterWorkFlowTask1s = new HashSet<MasterWorkFlowTask1>();
            PidfPbfs = new HashSet<PidfPbf>();
        }

        public int WorkflowId { get; set; }
        public string WorkflowName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<MasterWorkFlowTask1> MasterWorkFlowTask1s { get; set; }
        public virtual ICollection<PidfPbf> PidfPbfs { get; set; }
    }
}
