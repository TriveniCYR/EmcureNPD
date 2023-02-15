using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Master_ProductData
    {
        public Int64 UPID { get; set; }
        public string GenericName { get; set; }
        public string BrandName { get; set; }
        public int? FormulationID { get; set; }
        public string FormName { get; set; }
        public int? StrengthID { get; set; }
        public string Strength { get; set; }
        public int? PackStyleID { get; set; }
        public string PackStyle { get; set; }
        public int? PackSizeID { get; set; }
        public string PackSize { get; set; }
        public int? PlantID { get; set; }
        public string PlantName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
