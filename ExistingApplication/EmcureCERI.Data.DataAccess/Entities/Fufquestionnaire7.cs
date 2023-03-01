using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Fufquestionnaire7
    {
        public Fufquestionnaire7()
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
        public string Statement { get; set; }
        public string PrescriberName { get; set; }
        public DateTime? Doa { get; set; }
        public bool ConfirmFuf { get; set; }

        public ICollection<FollowUpFormMaster> FollowUpFormMaster { get; set; }
    }
}
