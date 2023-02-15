using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Master_Country
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public string ConuntryMobileCode { get; set; }
        public Nullable<int> ContinentID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
