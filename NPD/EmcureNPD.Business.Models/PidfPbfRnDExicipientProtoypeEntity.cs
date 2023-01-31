using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Business.Models
{
    public partial class PidfPbfRnDExicipientProtoypeEntity
    {
        public int ExicipientProtoypeId { get; set; }
        public int? StrengthId { get; set; }
        public string ExicipientPrototype { get; set; }
        public decimal? Cost { get; set; }
        public string DosagePerUnit { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
