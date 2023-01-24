using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfMedical
    {
        public long PidfmedicalId { get; set; }
        public long Pidfid { get; set; }
        public int MedicalOpinion { get; set; }
        public string Remark { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public virtual Pidf Pidf { get; set; }
    }
}
