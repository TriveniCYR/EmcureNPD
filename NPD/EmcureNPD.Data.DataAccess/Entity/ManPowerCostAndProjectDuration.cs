using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class ManPowerCostAndProjectDuration
    {
        public int ManPowerCostAndProjectDurationId { get; set; }
        public string ManHourRate { get; set; }
        public decimal? ProjectInitiation { get; set; }
        public decimal? LiteratureReviewAndSourcing { get; set; }
        public decimal? FormulationDevelopment { get; set; }
        public decimal? AnalyticalDevelopment { get; set; }
        public decimal? PilotBioStudy { get; set; }
        public decimal? ScaleUp { get; set; }
        public decimal? AmvandTt { get; set; }
        public decimal? ExhibitBatch { get; set; }
        public decimal? PivotalBioStudy { get; set; }
        public decimal? Stability { get; set; }
        public decimal? Filling { get; set; }
        public decimal? PrototypeCost { get; set; }
        public decimal? ScaleUpCost { get; set; }
        public decimal? ExhibitCost { get; set; }
        public decimal? TotalCost { get; set; }
    }
}
