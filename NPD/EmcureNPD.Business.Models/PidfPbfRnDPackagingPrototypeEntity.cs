using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Business.Models
{
    public partial class PidfPbfRnDPackagingPrototypeEntity
    {
        public int PackagingPrototypeId { get; set; }
        public int? StrengthId { get; set; }
        public int? PackagingTypeId { get; set; }
        public string UnitofMeasurement { get; set; }
        public decimal? Cost { get; set; }
        public int? Quantity { get; set; }
        public bool? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
