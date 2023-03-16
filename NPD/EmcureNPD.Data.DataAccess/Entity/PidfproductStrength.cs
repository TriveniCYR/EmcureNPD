using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfproductStrength
    {
        public PidfproductStrength()
        {
            PidfCommercials = new HashSet<PidfCommercial>();
            PidfPbfAnalyticalCosts = new HashSet<PidfPbfAnalyticalCost>();
            PidfPbfAnalyticalExhibits = new HashSet<PidfPbfAnalyticalExhibit>();
            PidfPbfAnalyticalPrototypes = new HashSet<PidfPbfAnalyticalPrototype>();
            PidfPbfAnalyticalScaleUps = new HashSet<PidfPbfAnalyticalScaleUp>();
            PidfPbfAnalyticals = new HashSet<PidfPbfAnalytical>();
            PidfPbfClinicalCosts = new HashSet<PidfPbfClinicalCost>();
            PidfPbfClinicalPilotBioFastings = new HashSet<PidfPbfClinicalPilotBioFasting>();
            PidfPbfClinicalPilotBioFeds = new HashSet<PidfPbfClinicalPilotBioFed>();
            PidfPbfClinicalPivotalBioFastings = new HashSet<PidfPbfClinicalPivotalBioFasting>();
            PidfPbfClinicalPivotalBioFeds = new HashSet<PidfPbfClinicalPivotalBioFed>();
            PidfPbfClinicals = new HashSet<PidfPbfClinical>();
            PidfPbfGenerals = new HashSet<PidfPbfGeneral>();
        }

        public long PidfproductStrengthId { get; set; }
        public long Pidfid { get; set; }
        public string Strength { get; set; }
        public int UnitofMeasurementId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }

        public virtual Pidf Pidf { get; set; }
        public virtual MasterUnitofMeasurement UnitofMeasurement { get; set; }
        public virtual ICollection<PidfCommercial> PidfCommercials { get; set; }
        public virtual ICollection<PidfPbfAnalyticalCost> PidfPbfAnalyticalCosts { get; set; }
        public virtual ICollection<PidfPbfAnalyticalExhibit> PidfPbfAnalyticalExhibits { get; set; }
        public virtual ICollection<PidfPbfAnalyticalPrototype> PidfPbfAnalyticalPrototypes { get; set; }
        public virtual ICollection<PidfPbfAnalyticalScaleUp> PidfPbfAnalyticalScaleUps { get; set; }
        public virtual ICollection<PidfPbfAnalytical> PidfPbfAnalyticals { get; set; }
        public virtual ICollection<PidfPbfClinicalCost> PidfPbfClinicalCosts { get; set; }
        public virtual ICollection<PidfPbfClinicalPilotBioFasting> PidfPbfClinicalPilotBioFastings { get; set; }
        public virtual ICollection<PidfPbfClinicalPilotBioFed> PidfPbfClinicalPilotBioFeds { get; set; }
        public virtual ICollection<PidfPbfClinicalPivotalBioFasting> PidfPbfClinicalPivotalBioFastings { get; set; }
        public virtual ICollection<PidfPbfClinicalPivotalBioFed> PidfPbfClinicalPivotalBioFeds { get; set; }
        public virtual ICollection<PidfPbfClinical> PidfPbfClinicals { get; set; }
        public virtual ICollection<PidfPbfGeneral> PidfPbfGenerals { get; set; }
    }
}
