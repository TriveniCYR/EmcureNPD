using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDExicipientScaleUpEntity
    {
        public int ExicipientScaleUpId { get; set; }
        public int? StrengthId { get; set; }
        public string ExicipientPrototype { get; set; }
        public decimal? Cost { get; set; }
        public string DosagePerUnit { get; set; }
        public bool? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
