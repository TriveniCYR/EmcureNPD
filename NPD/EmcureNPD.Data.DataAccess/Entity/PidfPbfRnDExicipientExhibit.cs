using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDExicipientExhibit
    {
        public PidfPbfRnDExicipientExhibit()
        {
            PidfPbfRnDs = new HashSet<PidfPbfRnD>();
        }

        public int ExicipientExhibitId { get; set; }
        public long StrengthId { get; set; }
        public string ExicipientExhibit { get; set; }
        public decimal? Cost { get; set; }
        public string DosagePerUnit { get; set; }
        public bool? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual PidfproductStrength Strength { get; set; }
        public virtual ICollection<PidfPbfRnD> PidfPbfRnDs { get; set; }
    }
}
