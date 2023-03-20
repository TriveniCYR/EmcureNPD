using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfGeneral
    {
        public PidfPbfGeneral()
        {
            PidfPbfAnalyticalCosts = new HashSet<PidfPbfAnalyticalCost>();
            PidfPbfAnalyticals = new HashSet<PidfPbfAnalytical>();
            PidfPbfClinicals = new HashSet<PidfPbfClinical>();
            PidfPbfGeneralStrengths = new HashSet<PidfPbfGeneralStrength>();
            PidfPbfRnDExicipientPrototypes = new HashSet<PidfPbfRnDExicipientPrototype>();
        }

        public long PbfgeneralId { get; set; }
        public long Pidfpbfid { get; set; }
        public int BusinessUnitId { get; set; }
        public string Capex { get; set; }
        public double? TotalExpense { get; set; }
        public string ProjectComplexity { get; set; }
        public int ProductTypeId { get; set; }
        public string TestLicenseAvailability { get; set; }
        public DateTime? BudgetTimelineSubmissionDate { get; set; }
        public DateTime? ProjectDevelopmentInitialDate { get; set; }
        public int? FormulationGlid { get; set; }
        public int? AnalyticalGlid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual MasterUser AnalyticalGl { get; set; }
        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual MasterUser FormulationGl { get; set; }
        public virtual PidfPbf Pidfpbf { get; set; }
        public virtual MasterProductType ProductType { get; set; }
        public virtual ICollection<PidfPbfAnalyticalCost> PidfPbfAnalyticalCosts { get; set; }
        public virtual ICollection<PidfPbfAnalytical> PidfPbfAnalyticals { get; set; }
        public virtual ICollection<PidfPbfClinical> PidfPbfClinicals { get; set; }
        public virtual ICollection<PidfPbfGeneralStrength> PidfPbfGeneralStrengths { get; set; }
        public virtual ICollection<PidfPbfRnDExicipientPrototype> PidfPbfRnDExicipientPrototypes { get; set; }
    }
}
