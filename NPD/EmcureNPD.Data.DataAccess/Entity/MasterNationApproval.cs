using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterNationApproval
    {
        public MasterNationApproval()
        {
            MasterNationApprovalCountryMappings = new HashSet<MasterNationApprovalCountryMapping>();
        }

        public int NationApprovalId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? MaxEop { get; set; }
        public int? MinEop { get; set; }

        public virtual ICollection<MasterNationApprovalCountryMapping> MasterNationApprovalCountryMappings { get; set; }
    }
}
