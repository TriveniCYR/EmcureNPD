using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class DRFManufHeaderAndDetails : Tbl_DRF_Manufacturing
    {
        public List<Tbl_DRF_Manufacturing_APISite> Tbl_DRF_Manufacturing_APISite { get; set; }
    }
}
