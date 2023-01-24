using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfAnalyticalExhibitEntity
    {
        public int PrototypeId { get; set; }
        public int? TestTypeId { get; set; }
        public int? Numberoftests { get; set; }
        public string PrototypeDevelopment { get; set; }
        public decimal? Cost { get; set; }
        public decimal? PrototypeCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
