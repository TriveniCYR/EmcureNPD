using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRndBatchSize
    {
        public long BatchSizeId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public string PrototypeFormulation { get; set; }
        public string ScaleUpbatch { get; set; }
        public string ExhibitBatch1 { get; set; }
        public string ExhibitBatch2 { get; set; }
        public string ExhibitBatch3 { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
