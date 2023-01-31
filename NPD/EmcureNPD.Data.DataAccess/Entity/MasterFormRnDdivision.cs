using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterFormRnDdivision
    {
        public MasterFormRnDdivision()
        {
            PidfPbfs = new HashSet<PidfPbf>();
        }

        public int FormRnDdivisionId { get; set; }
        public string FormRnDdivisionName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<PidfPbf> PidfPbfs { get; set; }
    }
}
