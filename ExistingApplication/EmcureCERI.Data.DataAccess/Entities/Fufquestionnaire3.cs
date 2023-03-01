using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Fufquestionnaire3
    {
        public Fufquestionnaire3()
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
        public bool? TreatmentD { get; set; }
        public bool? Underlying { get; set; }
        public bool? TreatmentR { get; set; }
        public bool? Dop { get; set; }
        public bool? LostFollowUp { get; set; }
        public bool? Therapy { get; set; }
        public bool? Withdrawal { get; set; }
        public string WithdrawalReason { get; set; }
        public bool? DropOut { get; set; }
        public bool? OtherOutcomes { get; set; }
        public string OtherOutcomesSpecify { get; set; }
        public DateTime? Doa { get; set; }

        public ICollection<FollowUpFormMaster> FollowUpFormMaster { get; set; }
    }
}
