using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class BaselineDataMaster
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? Quest1 { get; set; }
        public int? Quest2 { get; set; }
        public int? Quest3 { get; set; }
        public int? Quest4 { get; set; }
        public string RejectionReason { get; set; }
        public bool IsConfirmedByHcp { get; set; }
        public bool IsConfirmedByAdmin { get; set; }
        public bool? IsStatus { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }

        public PatientDetails Patient { get; set; }
        public Questionnaire1 Quest1Navigation { get; set; }
        public Questionnaire2 Quest2Navigation { get; set; }
        public Questionnaire3 Quest3Navigation { get; set; }
        public Questionnaire4 Quest4Navigation { get; set; }
    }
}
