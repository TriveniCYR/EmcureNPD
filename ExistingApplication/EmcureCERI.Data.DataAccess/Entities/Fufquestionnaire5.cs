using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Fufquestionnaire5
    {
        public Fufquestionnaire5()
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
        public bool? MedicalHistory { get; set; }
        public string C1condition { get; set; }
        public DateTime? C1startDate { get; set; }
        public DateTime? C1stopDate { get; set; }
        public bool? C1ongoing { get; set; }
        public string C2condition { get; set; }
        public DateTime? C2startDate { get; set; }
        public DateTime? C2stopDate { get; set; }
        public bool? C2ongoing { get; set; }
        public string C3condition { get; set; }
        public DateTime? C3startDate { get; set; }
        public DateTime? C3stopDate { get; set; }
        public bool? C3ongoing { get; set; }
        public string C4condition { get; set; }
        public DateTime? C4startDate { get; set; }
        public DateTime? C4stopDate { get; set; }
        public bool? C4ongoing { get; set; }
        public string C5condition { get; set; }
        public DateTime? C5startDate { get; set; }
        public DateTime? C5stopDate { get; set; }
        public bool? C5ongoing { get; set; }
        public string C6condition { get; set; }
        public DateTime? C6startDate { get; set; }
        public DateTime? C6stopDate { get; set; }
        public bool? C6ongoing { get; set; }
        public string C7condition { get; set; }
        public DateTime? C7startDate { get; set; }
        public DateTime? C7stopDate { get; set; }
        public bool? C7ongoing { get; set; }
        public string C8condition { get; set; }
        public DateTime? C8startDate { get; set; }
        public DateTime? C8stopDate { get; set; }
        public bool? C8ongoing { get; set; }
        public string C9condition { get; set; }
        public DateTime? C9startDate { get; set; }
        public DateTime? C9stopDate { get; set; }
        public bool? C9ongoing { get; set; }
        public string C10condition { get; set; }
        public DateTime? C10startDate { get; set; }
        public DateTime? C10stopDate { get; set; }
        public bool? C10ongoing { get; set; }
        public string Comments { get; set; }
        public DateTime? Doa { get; set; }

        public ICollection<FollowUpFormMaster> FollowUpFormMaster { get; set; }
    }
}
