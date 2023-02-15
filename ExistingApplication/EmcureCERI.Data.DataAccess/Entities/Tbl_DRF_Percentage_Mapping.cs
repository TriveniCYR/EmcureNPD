using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
   public partial class Tbl_DRF_Percentage_Mapping
    {
        public int Id { get; set; }
        public Nullable<int> DRFID { get; set; }
        public Nullable<int> PerID { get; set; }
        public Nullable<int> PerValue { get; set; }
    }
}
