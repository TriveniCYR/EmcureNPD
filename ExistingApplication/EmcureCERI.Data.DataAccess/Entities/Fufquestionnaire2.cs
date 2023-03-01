using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Fufquestionnaire2
    {
        public Fufquestionnaire2()
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
        public string BhaematologyA { get; set; }
        public string BhaematologyC { get; set; }
        public string BbiochemistryA { get; set; }
        public string BbiochemistryC { get; set; }
        public string BureaA { get; set; }
        public string BureaC { get; set; }
        public string BcreatinineA { get; set; }
        public string BcreatinineC { get; set; }
        public string BphosphateA { get; set; }
        public string BphosphateC { get; set; }
        public string BuricAcidA { get; set; }
        public string BuricAcidC { get; set; }
        public string BbicarbonateA { get; set; }
        public string BbicarbonateC { get; set; }
        public string BurineAnalysisA { get; set; }
        public string BurineAnalysisC { get; set; }
        public string BserologyA { get; set; }
        public string BserologyC { get; set; }
        public string BothersSpecify { get; set; }
        public string BothersSpecifyA { get; set; }
        public string BothersSpecifyC { get; set; }
        public string Bcomments { get; set; }
        public DateTime? Bdoa { get; set; }
        public string FhaematologyA { get; set; }
        public string FhaematologyC { get; set; }
        public string FbiochemistryA { get; set; }
        public string FbiochemistryC { get; set; }
        public string FureaA { get; set; }
        public string FureaC { get; set; }
        public string FcreatinineA { get; set; }
        public string FcreatinineC { get; set; }
        public string FphosphateA { get; set; }
        public string FphosphateC { get; set; }
        public string FuricAcidA { get; set; }
        public string FuricAcidC { get; set; }
        public string FbicarbonateA { get; set; }
        public string FbicarbonateC { get; set; }
        public string FurineAnalysisA { get; set; }
        public string FurineAnalysisC { get; set; }
        public string FserologyA { get; set; }
        public string FserologyC { get; set; }
        public string FothersSpecify { get; set; }
        public string FothersSpecifyA { get; set; }
        public string FothersSpecifyC { get; set; }
        public string Fcomments { get; set; }
        public DateTime? Fdoa { get; set; }

        public ICollection<FollowUpFormMaster> FollowUpFormMaster { get; set; }
    }
}
