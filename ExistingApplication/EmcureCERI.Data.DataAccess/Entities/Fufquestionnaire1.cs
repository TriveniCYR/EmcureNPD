using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Fufquestionnaire1
    {
        public Fufquestionnaire1()
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
        public bool? Pexperienced { get; set; }
        public string EventTerm1 { get; set; }
        public DateTime? StartDate1 { get; set; }
        public DateTime? StopDate1 { get; set; }
        public string SaeId1 { get; set; }
        public string ComMedId1 { get; set; }
        public int? StudyDaid1 { get; set; }
        public int? OutcomeId1 { get; set; }
        public int? RelaStudyId1 { get; set; }
        public string EventTerm2 { get; set; }
        public DateTime? StartDate2 { get; set; }
        public DateTime? StopDate2 { get; set; }
        public string SaeId2 { get; set; }
        public string ComMedId2 { get; set; }
        public int? StudyDaid2 { get; set; }
        public int? OutcomeId2 { get; set; }
        public int? RelaStudyId2 { get; set; }
        public string EventTerm3 { get; set; }
        public DateTime? StartDate3 { get; set; }
        public DateTime? StopDate3 { get; set; }
        public string SaeId3 { get; set; }
        public string ComMedId3 { get; set; }
        public int? StudyDaid3 { get; set; }
        public int? OutcomeId3 { get; set; }
        public int? RelaStudyId3 { get; set; }
        public string EventTerm4 { get; set; }
        public DateTime? StartDate4 { get; set; }
        public DateTime? StopDate4 { get; set; }
        public string SaeId4 { get; set; }
        public string ComMedId4 { get; set; }
        public int? StudyDaid4 { get; set; }
        public int? OutcomeId4 { get; set; }
        public int? RelaStudyId4 { get; set; }
        public string EventTerm5 { get; set; }
        public DateTime? StartDate5 { get; set; }
        public DateTime? StopDate5 { get; set; }
        public string SaeId5 { get; set; }
        public string ComMedId5 { get; set; }
        public int? StudyDaid5 { get; set; }
        public int? OutcomeId5 { get; set; }
        public int? RelaStudyId5 { get; set; }
        public string EventTerm6 { get; set; }
        public DateTime? StartDate6 { get; set; }
        public DateTime? StopDate6 { get; set; }
        public string SaeId6 { get; set; }
        public string ComMedId6 { get; set; }
        public int? StudyDaid6 { get; set; }
        public int? OutcomeId6 { get; set; }
        public int? RelaStudyId6 { get; set; }
        public bool? CidofovirInjection { get; set; }
        public string Specify { get; set; }
        public string Aedetails { get; set; }
        public string PrescriberName { get; set; }
        public DateTime? Date { get; set; }

        public ICollection<FollowUpFormMaster> FollowUpFormMaster { get; set; }
    }
}
