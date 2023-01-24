﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDPackagingScaleUp
    {
        public int PackagingPrototypeId { get; set; }
        public int? StrengthId { get; set; }
        public int? PackagingTypeId { get; set; }
        public string UnitofMeasurement { get; set; }
        public decimal? Cost { get; set; }
        public int? Quantity { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
