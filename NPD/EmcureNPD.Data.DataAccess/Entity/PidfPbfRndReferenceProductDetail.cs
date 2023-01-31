using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRndReferenceProductDetail
    {
        public int ReferenceProductDetailId { get; set; }
        public decimal? UnitCostOfReferenceProduct { get; set; }
        public decimal? FormulationDevelopment { get; set; }
        public decimal? PilotBe { get; set; }
        public decimal? PharmasuiticalEquivalence { get; set; }
        public decimal? PivotalBio { get; set; }
        public decimal? TotalCost { get; set; }
    }
}
