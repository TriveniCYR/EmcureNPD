using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Tbl_DSR_NewProductContinentMapping
    {
        public int Id { get; set; }
        public Nullable<int> NewProductID { get; set; }
        public Nullable<int> ContinentID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
