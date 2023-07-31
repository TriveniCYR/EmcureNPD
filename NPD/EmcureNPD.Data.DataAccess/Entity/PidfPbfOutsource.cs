using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfOutsource
    {
        public int PidfpbfoutsourceId { get; set; }
        public long Pidfid { get; set; }
        public int ProjectWorkflowId { get; set; }
        public int PbfworkflowId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual Pidf Pidf { get; set; }
    }
}
