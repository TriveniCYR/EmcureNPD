using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterPackSize
    {
        public MasterPackSize()
        {
            PidfCommercials = new HashSet<PidfCommercial>();
        }

        public int PackSizeId { get; set; }
        public string PackSize { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<PidfCommercial> PidfCommercials { get; set; }
    }
}
