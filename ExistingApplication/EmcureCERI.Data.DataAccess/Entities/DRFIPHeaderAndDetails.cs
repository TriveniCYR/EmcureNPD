using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class DRFIPHeaderAndDetails : Tbl_DRF_IP_Details
    {
        public List<Tbl_DRF_Patent_Details> tbl_DRF_Patent_Details { get; set; }
    }
}
