using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
   public class Tbl_DRFDataMaster
    {
        public int Id { get; set; }
        public int InitializationId { get; set; }
        public int? IPDetailsId { get; set; }
        public int? ManufacturingId { get; set; }
        public int? SCMId { get; set; }
        public int? MedicalId { get; set; }
        public int? RAInfoId { get; set; }
        public int? FinanceId { get; set; }
        public int? FinalId { get; set; }
        public bool IsStatus { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
