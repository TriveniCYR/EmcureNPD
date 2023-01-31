using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterBerequirement
    {
        public MasterBerequirement()
        {
            PidfPbfs = new HashSet<PidfPbf>();
        }

        public int BerequirementId { get; set; }
        public string BerequirementName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<PidfPbf> PidfPbfs { get; set; }
    }
}
