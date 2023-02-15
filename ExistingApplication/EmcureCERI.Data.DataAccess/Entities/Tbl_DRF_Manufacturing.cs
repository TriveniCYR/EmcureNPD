using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_DRF_Manufacturing
    {
      public int Id { get; set; }
      public Nullable<int> ManufacturingSiteId { get; set; }
      public string ManufacturingSiteName { get; set; }
      public Nullable<int> APIId { get; set; }
      public string APISiteName { get; set; }
      public string Batchsize { get; set; }
      public Nullable<int>  Leadtime { get; set; }
      public Nullable<decimal> UnitEXW { get; set; }
      public Nullable<int> ArtworkTypeId { get; set; }
      public DateTime TentativeSchedule { get; set; } 
      public int Tentative_Artwork_Lead_Time { get; set; }
        public decimal? PackorShipper { get; set; }
        public decimal? GrossWeight { get; set; }
        public string Dimensions { get; set; }
        public decimal? MWidth { get; set; }        
        public decimal? MHeight { get; set; }        
        public decimal? MLength { get; set; }
        public string Remark { get; set; }
      public Nullable<int> CreatedBy { get; set; }
      public DateTime CreatedDate { get; set; }
      public Nullable<int> ModifiedBy { get; set; }
      public DateTime ModifiedDate { get; set; }
      public int InitializationId { get; set; }
      public string DPDManufacturingSiteName { get; set; }
      public string DPDAPISiteName { get; set; }
      public string DPDArtworkTypeName { get; set; }
    }
}
