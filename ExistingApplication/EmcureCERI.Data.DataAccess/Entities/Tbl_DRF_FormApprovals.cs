using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
   public class Tbl_DRF_FormApprovals
    {
        [Key]
        public int ID { get; set; }
        public int InitializationID { get; set; }
        public Nullable<bool> DRFCreatedApproval { get; set; }
        public Nullable<int> DRFCreatedBy { get; set; }
        public Nullable<DateTime> DRFCreatedDate { get; set; }
        public Nullable<bool> CountryManagerApproval { get; set; }
        public Nullable<int> CMCreatedBy { get; set; }
        public Nullable<DateTime> CMCreatedDate { get; set; }

        public Nullable<bool> LineManagerApproval { get; set; }
        public Nullable<int> LMCreatedBy { get; set; }
        public Nullable<DateTime> LMCreatedDate { get; set; }
        public Nullable<bool> HODofDossierApproval { get; set; }
        public Nullable<int> HODCreatedBy { get; set; }
        public Nullable<DateTime> HODCreatedDate { get; set; }
        public string Comment { get; set; }
    }
}
