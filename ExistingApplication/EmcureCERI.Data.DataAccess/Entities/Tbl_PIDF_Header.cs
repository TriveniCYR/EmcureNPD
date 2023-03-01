using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Tbl_PIDF_Header
    {
        [Key]
        public Int64 PidfID { get; set; }
        public string PIDFNo { get; set; }
        public string PidfDate { get; set; }
        public Nullable<int> ProjectorProductID { get; set; }
        public string ProjectorProductName { get; set; }
        public string Strengths { get; set; }
        public Nullable<int> PidfStatusID { get; set; }
        public string PidfStatus { get; set; }
        public Nullable<int> ApprovedById1 { get; set; }
        public string ApprovedByDate1 { get; set; }
        public Nullable<int> ApprovedByID1StatusID { get; set; }
        public string ApprovedByID1Remark { get; set; }
        public Nullable<int> ApprovedById2 { get; set; }
        public string ApprovedByDate2 { get; set; }
        public Nullable<int> ApprovedByID2StatusID { get; set; }
        public string ApprovedByID2Remark { get; set; }
        public Nullable<int> ApprovedById3 { get; set; }
        public string ApprovedByDate3 { get; set; }
        public Nullable<int> ApprovedByID3StatusID { get; set; }
        public string ApprovedByID3Remark { get; set; }
        public Nullable<int> ApprovedById4 { get; set; }
        public Nullable<int> ApprovedByID4StatusID { get; set; }
        public string ApprovedByID4Remark { get; set; }
        public string ApprovedByDate4 { get; set; }
        public Nullable<int> PidfLastStatusID { get; set; }
        public string PidfLastStatus { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> Createdby { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public string ModifiedDate { get; set; }
        public Nullable<int> StatusFromUser { get; set; }
        public string RemarkFromUser { get; set; }
    }
}
