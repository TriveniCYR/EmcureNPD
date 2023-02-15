using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class PatientFollowUpForm
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int FollowUpFormId { get; set; }
        public int? SrNo { get; set; }
        public DateTime? Date { get; set; }
        public string FollowUpFormName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
