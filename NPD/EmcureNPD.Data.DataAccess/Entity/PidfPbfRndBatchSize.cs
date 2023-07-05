﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRndBatchSize
    {
        public long BatchSizeId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public double? PrototypeFormulation { get; set; }
        public double? ScaleUpbatch { get; set; }
        public double? ExhibitBatch1 { get; set; }
        public double? ExhibitBatch2 { get; set; }
        public double? ExhibitBatch3 { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public double? Salt { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
