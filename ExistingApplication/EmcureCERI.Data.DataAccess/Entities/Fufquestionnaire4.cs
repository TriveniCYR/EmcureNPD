using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Fufquestionnaire4
    {
        public Fufquestionnaire4()
        {
            FollowUpFormMaster = new HashSet<FollowUpFormMaster>();
        }

        public int Id { get; set; }
        public string Heading { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public bool IsFulFill { get; set; }
        public string Indication { get; set; }
        public string Dose { get; set; }
        public string Frequency { get; set; }
        public string Route { get; set; }
        public string Remarks { get; set; }
        public string TotalDose { get; set; }
        public DateTime? DotstartDate { get; set; }
        public DateTime? DotstopDate { get; set; }
        public string TotalTreatment { get; set; }
        public string Tremarks { get; set; } 
        public string Comments { get; set; }
        public DateTime? Doa { get; set; }
        public ICollection<FollowUpFormMaster> FollowUpFormMaster { get; set; }
    }
}
