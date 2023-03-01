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
            PidfPbfClinicalPilotBioFastings = new HashSet<PidfPbfClinicalPilotBioFasting>();
            PidfPbfClinicalPilotBioFeds = new HashSet<PidfPbfClinicalPilotBioFed>();
            PidfPbfClinicalPivotalBioFastings = new HashSet<PidfPbfClinicalPivotalBioFasting>();
            PidfPbfClinicalPivotalBioFeds = new HashSet<PidfPbfClinicalPivotalBioFed>();
            PidfPbfClinicals = new HashSet<PidfPbfClinical>();
            PidfPbfRnDExicipientExhibits = new HashSet<PidfPbfRnDExicipientExhibit>();
            PidfPbfRnDExicipientProtoypes = new HashSet<PidfPbfRnDExicipientProtoype>();
            PidfPbfRnDExicipientScaleUps = new HashSet<PidfPbfRnDExicipientScaleUp>();
            PidfPbfRnDPackagingExhibits = new HashSet<PidfPbfRnDPackagingExhibit>();
            PidfPbfRnDPackagingPrototypes = new HashSet<PidfPbfRnDPackagingPrototype>();
            PidfPbfRnDPackagingScaleUps = new HashSet<PidfPbfRnDPackagingScaleUp>();
            PidfPbfRnDPlantSupportCosts = new HashSet<PidfPbfRnDPlantSupportCost>();
            PidfPbfRnDs = new HashSet<PidfPbfRnD>();
            PidfPbfRndProjectEstimations = new HashSet<PidfPbfRndProjectEstimation>();
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
        public virtual ICollection<PidfPbfClinicalPilotBioFasting> PidfPbfClinicalPilotBioFastings { get; set; }
        public virtual ICollection<PidfPbfClinicalPilotBioFed> PidfPbfClinicalPilotBioFeds { get; set; }
        public virtual ICollection<PidfPbfClinicalPivotalBioFasting> PidfPbfClinicalPivotalBioFastings { get; set; }
        public virtual ICollection<PidfPbfClinicalPivotalBioFed> PidfPbfClinicalPivotalBioFeds { get; set; }
        public virtual ICollection<PidfPbfClinical> PidfPbfClinicals { get; set; }
        public virtual ICollection<PidfPbfRnDExicipientExhibit> PidfPbfRnDExicipientExhibits { get; set; }
        public virtual ICollection<PidfPbfRnDExicipientProtoype> PidfPbfRnDExicipientProtoypes { get; set; }
        public virtual ICollection<PidfPbfRnDExicipientScaleUp> PidfPbfRnDExicipientScaleUps { get; set; }
        public virtual ICollection<PidfPbfRnDPackagingExhibit> PidfPbfRnDPackagingExhibits { get; set; }
        public virtual ICollection<PidfPbfRnDPackagingPrototype> PidfPbfRnDPackagingPrototypes { get; set; }
        public virtual ICollection<PidfPbfRnDPackagingScaleUp> PidfPbfRnDPackagingScaleUps { get; set; }
        public virtual ICollection<PidfPbfRnDPlantSupportCost> PidfPbfRnDPlantSupportCosts { get; set; }
        public virtual ICollection<PidfPbfRnD> PidfPbfRnDs { get; set; }
        public virtual ICollection<PidfPbfRndProjectEstimation> PidfPbfRndProjectEstimations { get; set; }
    }
}
