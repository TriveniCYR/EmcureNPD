using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Tbl_DRF_PIDF_Mapping
    {
        public int ID { get; set; }
        public Nullable<int> DRFID { get; set; }
        public Nullable<int> PIDFID { get; set; }
    }
}
