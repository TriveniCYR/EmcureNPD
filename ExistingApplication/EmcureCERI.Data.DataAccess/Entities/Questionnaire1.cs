using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Questionnaire1
    {
        public Questionnaire1()
        {
            BaselineDataMaster = new HashSet<BaselineDataMaster>();
        }

        public int Id { get; set; }
        public string Heading { get; set; }
        public bool? Icg { get; set; }
        public DateTime? DateParticipant { get; set; }
        public int? CountryId { get; set; }
        public bool? SoluInfusion { get; set; }
        public bool? Informedconsent { get; set; }
        public bool? Cmv { get; set; }
        public string OtherIndication { get; set; }
        public bool? Hypersensitivity { get; set; }
        public bool? Probenecide { get; set; }
        public bool? RenalInsufficiency { get; set; }
        public bool? Concomitant { get; set; }
        public bool? DirectIntraocular { get; set; }
        public bool? InductionTreatment { get; set; }
        public bool? MaintenanceTreatment { get; set; }
        public bool? PediatricPopulation { get; set; }
        public bool? ElderlyPopulation { get; set; }
        public bool? PregnantLactating { get; set; }
        public DateTime? DateAssessment { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public bool IsFulFill { get; set; } 
        public bool? Icgb { get; set; } 
        public bool? Cglrfp { get; set; }

        public ICollection<BaselineDataMaster> BaselineDataMaster { get; set; }
    }
}
