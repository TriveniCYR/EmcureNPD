using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class PatientDetails
    {
        public PatientDetails()
        {
            BaselineDataMaster = new HashSet<BaselineDataMaster>();
        }

        public int Id { get; set; }
        public int? AspNetUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RfirstName { get; set; }
        public string RlastName { get; set; }
        public DateTime? Point1Date { get; set; }
        public bool? Point1 { get; set; }
        public bool? Point2 { get; set; }
        public bool? Point3 { get; set; }
        public bool? Point4 { get; set; }
        public bool? Point5 { get; set; }
        public bool? Point6 { get; set; }
        public bool? Point7 { get; set; }
        public string PdfName { get; set; }
        public DateTime? PdfUploadDate { get; set; }
        public int? PdfUploadBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public bool? IsStatus { get; set; }
        public string UniqueId { get; set; }
        public string RejectionReason { get; set; }
        public bool IsConsentFcheckByHcp { get; set; }
        public bool IsConsentFcheckByAdmin { get; set; }

        public ICollection<BaselineDataMaster> BaselineDataMaster { get; set; }
    }
}
