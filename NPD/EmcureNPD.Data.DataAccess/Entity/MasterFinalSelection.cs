using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterFinalSelection
    {
        public MasterFinalSelection()
        {
            PidfCommercialYears = new HashSet<PidfCommercialYear>();
        }

        public int FinalSelectionId { get; set; }
        public string FinalSelectionName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<PidfCommercialYear> PidfCommercialYears { get; set; }
    }
}
