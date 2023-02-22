﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfAnalytical
    {
        public PidfPbfAnalytical()
        {
            PidfPbfAnalyticalCosts = new HashSet<PidfPbfAnalyticalCost>();
            PidfPbfAnalyticalExhibits = new HashSet<PidfPbfAnalyticalExhibit>();
        }

        public long PbfanalyticalId { get; set; }
        public long Pbfid { get; set; }
        public long Pidfid { get; set; }
        public int BusinessUnitId { get; set; }
        public double? TotalExpense { get; set; }
        public string ProjectComplexity { get; set; }
        public string ProductType { get; set; }
        public string TestLicenseAvailability { get; set; }
        public DateTime? BudgetTimelineSubmissionDate { get; set; }
        public int? FormulationId { get; set; }
        public int? AnalyticalId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual PidfPbf Pbf { get; set; }
        public virtual Pidf Pidf { get; set; }
        public virtual ICollection<PidfPbfAnalyticalCost> PidfPbfAnalyticalCosts { get; set; }
        public virtual ICollection<PidfPbfAnalyticalExhibit> PidfPbfAnalyticalExhibits { get; set; }
    }
}
