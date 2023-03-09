using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Business.Models
{
    public partial class PidfPbfAnalyticalScaleUpEntity
    {
        public long ScaleUpId { get; set; }
        public long PBFAnalyticalId { get; set; }
        public long StrengthId { get; set; }
        public long TestTypeId { get; set; }
        public int? Numberoftests { get; set; }
        public string PrototypeDevelopment { get; set; }
        public decimal? Cost { get; set; }
        public decimal? PrototypeCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
