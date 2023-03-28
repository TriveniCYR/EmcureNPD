﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDReferenceProductDetail
    {
        public long ReferenceProductDetailId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public string ReferenceProductDetailDevelopment { get; set; }
        public string UnitCostOfReferenceProduct { get; set; }
        public string FormulationDevelopment { get; set; }
        public string PilotBe { get; set; }
        public string PharmasuiticalEquivalence { get; set; }
        public string PivotalBio { get; set; }
        public double? TotalCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
